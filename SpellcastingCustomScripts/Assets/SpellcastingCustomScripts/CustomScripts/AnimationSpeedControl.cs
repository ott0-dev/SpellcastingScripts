using UnityEngine;
using System.Collections;

public class AnimationSpeedControl : MonoBehaviour
{
    public Animator animator;
    public float slowSpeed = 0.1f;  // Slow down to half speed
    public float normalSpeed = 1.0f;  // Normal speed
    public float transitionDuration = 2f;  // Time in seconds to transition back to normal speed

    void Start()

    {
        
        GetComponent<AnimatorTriggerer>().Trigger3Event += Slowdownanimation;

        
        
    }

    public void Slowdownanimation()

    {
        // Slow down the animation to the specified speed
        animator.speed = slowSpeed;
        StartCoroutine(SpeedUpOverTime(transitionDuration));
    }

        
    // Coroutine to gradually restore the speed to normal
    IEnumerator SpeedUpOverTime(float duration)
    {
        float elapsedTime = 0f;
        float initialSpeed = animator.speed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            // Lerp between the initial slow speed and the normal speed
            animator.speed = Mathf.Lerp(initialSpeed, normalSpeed, elapsedTime / duration);

            yield return null; // Wait for the next frame
        }

        // Ensure the speed is exactly 1.0 at the end
        animator.speed = normalSpeed;
    }
}
