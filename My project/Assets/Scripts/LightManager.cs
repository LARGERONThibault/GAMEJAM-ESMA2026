using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second.")]
    public float rotationSpeed = 360f;

    [Tooltip("Axis of rotation. For 2D, usually (0, 0, 1).")]
    public Vector3 rotationAxis = new Vector3(0f, 0f, 1f);

    public bool smooth = true;

    private Quaternion targetRotation;

    
    void Start()
    {
        if (rotationAxis == Vector3.zero)
        {
            Debug.LogWarning("Rotation axis is zero. No rotation will occur.");
            enabled = false;
            return;


        }
        
        targetRotation = transform.rotation;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            SetTargetAngle(0f);
        else if (Input.GetKey(KeyCode.A))
            SetTargetAngle(90f);
        else if (Input.GetKey(KeyCode.S))
            SetTargetAngle(180f);
        else if (Input.GetKey(KeyCode.D))
            SetTargetAngle(270f);

        // Apply rotation (instant or smooth)
        if (smooth)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 3);
        }
        else
        {
            transform.rotation = targetRotation;
        }
        
    }
    private void SetTargetAngle(float angleDegrees)
    {
        // Create a rotation of angleDegrees around the configured axis
        targetRotation = Quaternion.AngleAxis(angleDegrees, rotationAxis.normalized);
    }

    private void Luminosity(float value)
    {
        // Adjust the light's intensity based on the value (0 to 1)
        Light light = GetComponent<Light>();
        if (light != null)
        {
            light.intensity = Mathf.Clamp01(value);
        }


    }
    
}