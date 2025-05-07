using UnityEditor;


[CustomEditor(typeof(RoboticArmController))]
public class RoboticArmControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector
        DrawDefaultInspector();

        // Reference to the target script
        RoboticArmController controller = (RoboticArmController)target;

        // Add sliders for each joint
        for (int i = 0; i < controller.joints.Length; i++)
        {
            var joint = controller.joints[i];
            if (joint.jointTransform != null)
            {
                // Add a slider for the joint's target angle
                joint.targetAngle = EditorGUILayout.Slider($"Joint {i + 1} Angle", joint.targetAngle, joint.minAngle, joint.maxAngle);
            }
            else
            {
                EditorGUILayout.LabelField($"Joint {i + 1} is not assigned!");
            }
        }
    }
}