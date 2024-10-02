using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FresnelParameterLerp))]
public class FresnelParameterLerpEditor : Editor
{
    private SerializedProperty lowIntensityParameters;
    private SerializedProperty midIntensityParameters;
    private SerializedProperty highIntensityParameters;

    private bool showLowParameters = true;
    private bool showMidParameters = true;
    private bool showHighParameters = true;

    private bool showLowExtraCurves = false;
    private bool showMidExtraCurves = false;
    private bool showHighExtraCurves = false;

    private void OnEnable()
    {
        // Cache serialized properties for each intensity level
        lowIntensityParameters = serializedObject.FindProperty("lowIntensityParameters");
        midIntensityParameters = serializedObject.FindProperty("midIntensityParameters");
        highIntensityParameters = serializedObject.FindProperty("highIntensityParameters");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        FresnelParameterLerp script = (FresnelParameterLerp)target;

        // Display the Material field
        script.material = (Material)EditorGUILayout.ObjectField("Material", script.material, typeof(Material), true);

        // Display the Renderer field
        script.targetRenderer = (Renderer)EditorGUILayout.ObjectField("Target Renderer", script.targetRenderer, typeof(Renderer), true);

        // Low Intensity Parameters Foldout
        showLowParameters = EditorGUILayout.Foldout(showLowParameters, "Low Intensity Parameters");
        if (showLowParameters)
        {
            DrawLerpParameters(lowIntensityParameters, ref showLowExtraCurves);
        }

        // Mid Intensity Parameters Foldout
        showMidParameters = EditorGUILayout.Foldout(showMidParameters, "Mid Intensity Parameters");
        if (showMidParameters)
        {
            DrawLerpParameters(midIntensityParameters, ref showMidExtraCurves);
        }

        // High Intensity Parameters Foldout
        showHighParameters = EditorGUILayout.Foldout(showHighParameters, "High Intensity Parameters");
        if (showHighParameters)
        {
            DrawLerpParameters(highIntensityParameters, ref showHighExtraCurves);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawLerpParameters(SerializedProperty parameters, ref bool showExtraCurves)
    {
        EditorGUI.indentLevel++;

        showExtraCurves = EditorGUILayout.Foldout(showExtraCurves, "Extra Curves");
        if (showExtraCurves)
        {
            EditorGUILayout.PropertyField(parameters.FindPropertyRelative("useDistanceLerp"), new GUIContent("Use Distance Lerp"));
            EditorGUILayout.PropertyField(parameters.FindPropertyRelative("showMaskedLineY"), new GUIContent("Show Bottom Y"));
        }

        EditorGUI.indentLevel--;

        EditorGUILayout.PropertyField(parameters.FindPropertyRelative("totalLerpTime"), new GUIContent("Total Lerp Time"));
        EditorGUILayout.PropertyField(parameters.FindPropertyRelative("fresnelIntensityCurve"), new GUIContent("Fresnel Intensity Curve"));
        EditorGUILayout.PropertyField(parameters.FindPropertyRelative("fresnelSizeCurve"), new GUIContent("Fresnel Size Curve"));

        if (parameters.FindPropertyRelative("useDistanceLerp").boolValue)
        {
            EditorGUILayout.PropertyField(parameters.FindPropertyRelative("distanceCurve"), new GUIContent("Distance Curve"));
        }

        if (parameters.FindPropertyRelative("showMaskedLineY").boolValue)
        {
            EditorGUILayout.PropertyField(parameters.FindPropertyRelative("MaskedLineYCurve"), new GUIContent("Bottom Y Curve"));
        }
    }
}
