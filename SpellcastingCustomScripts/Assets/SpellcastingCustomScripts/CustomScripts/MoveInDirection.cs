using UnityEngine;

public class MoveInDirection : MonoBehaviour
{
    // Public velocity to set in the Inspector or from another script
    public Vector3 velocity = new Vector3(1, 0, 0); // Default movement in the X direction

    // Update is called once per frame
    void Update()
    {
        // Move the object in the specified direction at a constant pace
        transform.position += velocity * Time.deltaTime;
    }
}
