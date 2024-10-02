using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class AnimateInEditor : MonoBehaviour
{
    public Animator animator;
    private double lastEditorTime;

    void OnEnable()
    {
        EditorApplication.update += UpdateAnimatorInEditor;
        lastEditorTime = EditorApplication.timeSinceStartup;
    }

    void OnDisable()
    {
        EditorApplication.update -= UpdateAnimatorInEditor;
    }

    void UpdateAnimatorInEditor()
    {
        if (animator != null && !Application.isPlaying)
        {
            float deltaTime = (float)(EditorApplication.timeSinceStartup - lastEditorTime);
            lastEditorTime = EditorApplication.timeSinceStartup;
            animator.Update(deltaTime);
        }
    }
}
