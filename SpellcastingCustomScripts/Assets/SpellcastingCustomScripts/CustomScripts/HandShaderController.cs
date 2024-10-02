using UnityEngine;

[ExecuteInEditMode]
public class HandShaderController : MonoBehaviour
{
    public Transform handBone;
    public Renderer characterRenderer;  // Reference to the character's Renderer component
    private MaterialPropertyBlock materialPropertyBlock;

    void OnEnable()
    {
        // Initialize the MaterialPropertyBlock
        if (materialPropertyBlock == null)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
        }

        // Ensure the shader properties are updated when the object is enabled in the editor
        UpdateShaderProperties();
    }

    void OnValidate()
    {
        // Called when a value is changed in the Inspector
        if (materialPropertyBlock == null)
        {
            materialPropertyBlock = new MaterialPropertyBlock();
        }

        // Update the shader properties in edit mode when values are modified
        UpdateShaderProperties();
    }

    void Update()
    {
        // Ensure the shader properties are updated every frame
        UpdateShaderProperties();
    }

    void UpdateShaderProperties()
    {
        if (handBone == null || characterRenderer == null)
            return; // Exit if the necessary references are missing

        // Get the positions of the hand and torso
        var handPosition = handBone.position;
        

        // Fetch the current MaterialPropertyBlock of the renderer
        characterRenderer.GetPropertyBlock(materialPropertyBlock);

        // Set the hand and torso positions in the MaterialPropertyBlock
        materialPropertyBlock.SetVector("_HandPosition", handPosition);
        
        // Apply the modified MaterialPropertyBlock to the renderer
        characterRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
