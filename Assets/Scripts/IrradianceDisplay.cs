using TMPro;
using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class IrradianceDisplay : MonoBehaviour
{
    public TMP_Text irradianceText;
    public LightDetector lightDetector; 

    // Update is called once per frame
    void Update()
    {
        if (irradianceText != null && lightDetector !=null)
        {
            float irradiance = lightDetector.detectedIrradiance;
            irradianceText.text = $"Irradiance: {irradiance:F6}";
        }
        
    }
}
