#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CurveRangeAttribute))]
public class CurveRangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        CurveRangeAttribute range = (CurveRangeAttribute)attribute;

        Rect curveRect = new Rect(range.minX, range.minY, range.maxX - range.minX, range.maxY - range.minY);

        EditorGUI.CurveField(position, property, Color.green, curveRect, label);
    }
}
#endif
