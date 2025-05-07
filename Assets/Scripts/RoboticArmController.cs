using UnityEngine;

public class RoboticArmController : MonoBehaviour
{
    [System.Serializable]
    public class Joint
    {
        public Transform jointTransform; // The Transform of the joint
        public float minAngle = -90f;    // Minimum rotation angle
        public float maxAngle = 90f;     // Maximum rotation angle
        public float targetAngle = 0f;  // Desired angle for the joint
        public Axis rotationAxis = Axis.X; // Axis of rotation
        [HideInInspector] public Vector3 initialRotation;
    }

    public enum Axis { X, Y, Z } // Enum for rotation axes

    public Joint[] joints = new Joint[6]; // Array to hold all 6 joints
    public float rotationStep = 1f; // Step size for rotation when using the keyboard
    public float rotationSpeed = 50f; // Speed of rotation
    void Start()
    {
        // Store the initial rotation of each joint
        foreach (var joint in joints)
        {
            if (joint.jointTransform != null)
            {
                joint.initialRotation = joint.jointTransform.localRotation.eulerAngles;
            }
        }
    }
    void RotateJoint(Joint joint, float angle)
    {
        if (joint.jointTransform != null)
        {
            // Calculate the new rotation based on the initial rotation and target angle
            Vector3 newRotation = joint.initialRotation;
            switch (joint.rotationAxis)
            {
                case Axis.X:
                    newRotation.x = joint.initialRotation.x + angle;
                    break;
                case Axis.Y:
                    newRotation.y = joint.initialRotation.y + angle;
                    break;
                case Axis.Z:
                    newRotation.z = joint.initialRotation.z + angle;
                    break;
            }
            // Apply the new rotation
            joint.jointTransform.localRotation = Quaternion.Euler(newRotation);
            
            // // Apply rotation based on the selected axis
            // switch (joint.rotationAxis)
            // {
            //     case Axis.X:
            //         joint.jointTransform.rotation = Quaternion.Euler(angle, 0, 0);
            //         break;
            //     case Axis.Y:
            //         joint.jointTransform.rotation = Quaternion.Euler(0, angle, 0);
            //         break;
            //     case Axis.Z:
            //         joint.jointTransform.rotation = Quaternion.Euler(0, 0, angle);
            //         break;
            // }
        }
    }
    void Update()
    {
        // Iterate through each joint and apply the target angle
        foreach (var joint in joints)
        {
          RotateJoint(joint, joint.targetAngle);
        }
        
        HandleKeyboardInput();
    }

       void HandleKeyboardInput()
    {
        // Example key mappings for controlling joints
        if (Input.GetKey(KeyCode.Alpha1)) // Joint 1 positive rotation
        {
            AdjustJointAngle(0, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q)) // Joint 1 negative rotation
        {
            AdjustJointAngle(0, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha2)) // Joint 2 positive rotation
        {
            AdjustJointAngle(1, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W)) // Joint 2 negative rotation
        {
            AdjustJointAngle(1, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha3)) // Joint 3 positive rotation
        {
            AdjustJointAngle(2, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E)) // Joint 3 negative rotation
        {
            AdjustJointAngle(2, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha4)) // Joint 3 positive rotation
        {
            AdjustJointAngle(3, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R)) // Joint 3 negative rotation
        {
            AdjustJointAngle(3, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha5)) // Joint 3 positive rotation
        {
            AdjustJointAngle(4, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.T)) // Joint 3 negative rotation
        {
            AdjustJointAngle(4, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Alpha6)) // Joint 3 positive rotation
        {
            AdjustJointAngle(5, rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.G)) // Joint 3 negative rotation
        {
            AdjustJointAngle(5, -rotationStep * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space)) // Reset all joints
        {
            ResetAllJoints();
        }
        if (Input.GetKey(KeyCode.Escape)) // Exit the application
        {
            Application.Quit();
        }
        // Add more key mappings for other joints as needed
    }

    void AdjustJointAngle(int jointIndex, float angleDelta)
    {
        if (jointIndex >= 0 && jointIndex < joints.Length)
        {
            var joint = joints[jointIndex];
            joint.targetAngle = Mathf.Clamp(joint.targetAngle + angleDelta, joint.minAngle, joint.maxAngle);
        }
    }

    void ResetAllJoints()
    {
        foreach (var joint in joints)
        {
            if (joint.jointTransform != null)
            {
                joint.targetAngle = 0f; // Reset the target angle to 0
            }
        }
    }
}