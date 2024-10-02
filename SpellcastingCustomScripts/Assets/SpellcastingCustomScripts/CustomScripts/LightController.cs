using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour
{
    
    [System.Serializable]
    public class LightLerpParameters
    {
        public float totalLerpTime = 1f; // Total duration for the lerp

        [CurveRange(0, 1, 0, 10)]
        public AnimationCurve intensityCurve; // Curve for light intensity

        [CurveRange(0, 1, 0, 5)]
        public AnimationCurve rangeCurve; // Curve for light range
    }

    // Enum to define different intensity levels
    public enum IntensityLevel
    {
        Low,
        Mid,
        High
    }

    // The target light to control
    public Light targetLight;

    // Lerp parameters for different intensity levels
    public LightLerpParameters lowIntensityParameters;
    public LightLerpParameters midIntensityParameters;
    public LightLerpParameters highIntensityParameters;

    // Internal variables to track lerp progress
    private float currentLerpTime = 0f;
    private bool isLerping = false;
    private LightLerpParameters currentParameters;

    void Start()
    {
        targetLight.enabled = false;
        if (targetLight == null)
        {
            Debug.LogError("Target light not assigned. Please assign a light in the Inspector.");
            return;
        }

        GetComponent<AnimatorTriggerer>().Trigger1Event += () => StartLerping(IntensityLevel.Low);

        GetComponent<AnimatorTriggerer>().Trigger2Event += () => StartLerping(IntensityLevel.Mid);

        GetComponent<AnimatorTriggerer>().Trigger3Event += () => StartLerping(IntensityLevel.High);
    }

    void Update()
    {
        if (!isLerping || currentParameters == null) return;

        currentLerpTime += Time.deltaTime;
        float t = Mathf.Clamp01(currentLerpTime / currentParameters.totalLerpTime);

        // Evaluate intensity and range based on the curves
        float intensity = currentParameters.intensityCurve.Evaluate(t);
        float range = currentParameters.rangeCurve.Evaluate(t);

        // Set the light properties
        targetLight.intensity = intensity;
        targetLight.range = range;

        // Check if lerping has finished
        if (t >= 1f)
        {
            isLerping = false;
            targetLight.enabled = false; // Turn off the light when lerping ends
        }
    }

    // Public method to start lerping the light with a specific intensity level
    public void StartLerping(IntensityLevel intensity)
    {
        switch (intensity)
        {
            case IntensityLevel.Low:
                currentParameters = lowIntensityParameters;
                break;
            case IntensityLevel.Mid:
                currentParameters = midIntensityParameters;
                break;
            case IntensityLevel.High:
                currentParameters = highIntensityParameters;
                break;
            default:
                Debug.LogWarning("Invalid intensity level.");
                return;
        }

        if (currentParameters != null)
        {
            currentLerpTime = 0f;
            isLerping = true;
            targetLight.enabled = true; // Ensure the light is on when lerping starts
        }
        else
        {
            Debug.LogWarning("Lerp parameters not set for the selected intensity level.");
        }
    }
}
