using Unity.Collections;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    // how to mark it readonly in the inspector?
    [SerializeField, ReadOnly] public float detectedIrradiance = 0f;
    [SerializeField, ReadOnly] public float surfaceArea = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        surfaceArea = CalculateSurfaceArea();
        if (surfaceArea < 0.000001f)
        {
            Debug.LogWarning("Surface area is zero. Ensure the object has a valid mesh.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        detectedIrradiance = CalculateIrradiance();
    }

    private float CalculateSurfaceArea()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        float surfaceArea = 0f;

        // Get the triangles and vertices of the mesh
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        // Loop through each triangle
        for (int i = 0; i < triangles.Length; i += 3)
        {
            Vector3 v0 = vertices[triangles[i]];
            Vector3 v1 = vertices[triangles[i + 1]];
            Vector3 v2 = vertices[triangles[i + 2]];

            // Calculate the area of the triangle
            float triangleArea = Vector3.Cross(v1 - v0, v2 - v0).magnitude * 0.5f;
            surfaceArea += triangleArea;
        }

        return surfaceArea;
    }
    //Luminance refers to the amount of light emitted or reflected by a surface in a specific direction.
    //Irradiance (or illuminance) (natezenie promieniowania) 
    // refers to the amount of light energy received per unit area on a surface. 
    // It depends on the angle between the light direction and the surface normal.
    /**
        Odpowiada to mocy jaką, przenosi promieniowanie przez płaszczyznę jednostkową. 
        Jednostką irradiancji w układzie SI jest wat na metr kwadratowy [W/m²].
    */
    private float CalculateIrradiance()
    {
        float totalIrradiance = 0f;
        Light[] lights = Object.FindObjectsByType<Light>(FindObjectsSortMode.None);
        foreach (Light light in lights)
        {
            
            if (light.enabled)
            {
                Vector3 lightDirection = (light.transform.position - transform.position).normalized;
                Vector3 surfaceNormal = transform.up;
                
                float angleFactor = Mathf.Max(0, Vector3.Dot(surfaceNormal, lightDirection));
                float distance = Vector3.Distance(transform.position, light.transform.position);
                
                float irradi3anc = (light.intensity*angleFactor) / (distance * distance);
                Debug.Log($"Light: {light.name}, Intensity: {irradianc}, Angle Factor: {angleFactor}, Distance: {distance}");

                totalIrradiance += irradianc;
            }
        }
        return totalIrradiance / surfaceArea;
    }
    private void OnDrawGizmos()
    {
        // Visualize the light detector in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
       
    }
}
