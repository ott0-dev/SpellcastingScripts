using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimatorTriggerer))]
public class AnimatorTriggererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AnimatorTriggerer triggerer = (AnimatorTriggerer)target;

        if (GUILayout.Button("Trigger 1"))
        {
            triggerer.Trigger1();
        }

        if (GUILayout.Button("Trigger 2"))
        {
            triggerer.Trigger2();
        }

        if (GUILayout.Button("Trigger 3"))
        {
            triggerer.Trigger3();
        }
    }
}
