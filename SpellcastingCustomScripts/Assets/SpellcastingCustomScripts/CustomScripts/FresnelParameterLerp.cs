using UnityEngine;
using System;

public class FresnelParameterLerp : MonoBehaviour
{
    [System.Serializable]
    public class LerpParameters
    {
        public float totalLerpTime = 1f;

        [CurveRange(0, 1, 0, 100)]
        public AnimationCurve fresnelIntensityCurve;

        [CurveRange(0, 1, 0, 100)]
        public AnimationCurve fresnelSizeCurve;

        [CurveRange(0, 1, 0, 1000)]
        public AnimationCurve distanceCurve;

        [CurveRange(0, 1, -2, 2)]
        public AnimationCurve MaskedLineYCurve;

        public bool useDistanceLerp = false; // Toggle for using _Distance instead of Fresnel size
        public bool showMaskedLineY = false;
    }

    public LerpParameters lowIntensityParameters;
    public LerpParameters midIntensityParameters;
    public LerpParameters highIntensityParameters;

    // OnValidate to refresh the inspector when values change
    private void OnValidate()
    {
        // This just forces Unity to refresh the inspector when values change
        UnityEditor.EditorUtility.SetDirty(this);
    }
    public enum IntensityLevel
    {
        Low,
        Mid,
        High
    }

    // The material to modify
    public Material material;

    // The renderer to which the material is applied
    public Renderer targetRenderer;

    // Internal variables to track the lerp progress
    private float currentLerpTime = 0f;
    private Material instanceMaterial;
    private bool isLerping = false;
    private LerpParameters currentParameters;

    void Start()
    {
        if (material == null)
        {
            Debug.LogError("Material not assigned. Please assign a material in the Inspector.");
            return;
        }

        if (targetRenderer == null)
        {
            Debug.LogError("Target Renderer not assigned. Please assign the mesh's Renderer in the Inspector.");
            return;
        }

        // Create an instance of the material to avoid modifying the original asset
        instanceMaterial = new Material(material);

        // Assign the instance material to the target renderer
        targetRenderer.material = instanceMaterial;

        GetComponent<AnimatorTriggerer>().Trigger1Event += () => StartLerping(IntensityLevel.Low);
        GetComponent<AnimatorTriggerer>().Trigger2Event += () => StartLerping(IntensityLevel.Mid);
        GetComponent<AnimatorTriggerer>().Trigger3Event += () => StartLerping(IntensityLevel.High);
    }

    void Update()
    {
        if (!isLerping || currentParameters == null) return;

        currentLerpTime += Time.deltaTime;
        float t = Mathf.Clamp01(currentLerpTime / currentParameters.totalLerpTime);

        // Evaluate the fresnel intensity curve
        float fresnelIntensity = currentParameters.fresnelIntensityCurve.Evaluate(t);
        instanceMaterial.SetFloat("_Fresnel_Intensity", fresnelIntensity);

        // Always evaluate and use the fresnel size curve
        float fresnelSize = currentParameters.fresnelSizeCurve.Evaluate(t);
        instanceMaterial.SetFloat("_FresnelSize", fresnelSize);

        // Only evaluate and use the distance curve if useDistanceLerp is true
        if (currentParameters.useDistanceLerp)
        {
            float distance = currentParameters.distanceCurve.Evaluate(t); // Evaluate distance curve
            instanceMaterial.SetFloat("_DistanceFromHand", distance);
            instanceMaterial.SetFloat("_DistanceActive", 1);  // Set _DistanceActive to true
            instanceMaterial.SetFloat("_FresnelActive", 0);   // Set _FresnelActive to false
        }
        else
        {
            instanceMaterial.SetFloat("_DistanceActive", 0);  // Set _DistanceActive to false
            instanceMaterial.SetFloat("_FresnelActive", 1);   // Set _FresnelActive to true
        }

       
        if (currentParameters.showMaskedLineY)
        {
            float maskedLineY = currentParameters.MaskedLineYCurve.Evaluate(t); 
            instanceMaterial.SetFloat("_MaskedLineY", maskedLineY);
        }

        // Check if lerping has finished
        if (t >= 1f)
        {
            isLerping = false;
            currentParameters = null;
            instanceMaterial.SetFloat("_Active", 0);  // Deactivate when lerping finishes
        }
    }


    void OnDestroy()
    {
        // Clean up the instance material when the script is destroyed
        if (instanceMaterial != null)
        {
            Destroy(instanceMaterial);
        }
    }

    // Public method to start the lerp with a specific intensity level
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
        }

        if (currentParameters != null)
        {
            currentLerpTime = 0f;
            isLerping = true;
            instanceMaterial.SetFloat("_Active", 1);  // Activate when lerping starts
        }
        else
        {
            Debug.LogWarning("Lerp parameters not set for " + intensity + " intensity.");
        }
    }
}
