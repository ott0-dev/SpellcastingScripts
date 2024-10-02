using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    // Public variables to assign particle systems in the Unity Inspector
    public ParticleSystem particleSystem1;
    public ParticleSystem particleSystem2;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure both particle systems are stopped at the start
        if (particleSystem1 != null)
        {
            particleSystem1.Stop();
        }
        if (particleSystem2 != null)
        {
            particleSystem2.Stop();
        }

        GetComponent<AnimatorTriggerer>().Trigger2Event += () => ActivateParticles();

        GetComponent<AnimatorTriggerer>().Trigger3Event += () => ActivateParticles();
    }

    // Function to turn on (play) the particle systems
    public void ActivateParticles()
    {
        if (particleSystem1 != null)
        {
            // Stop the particle system to restart it
            particleSystem1.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem1.Play();
        }
        if (particleSystem2 != null)
        {
            // Stop the particle system to restart it
            particleSystem2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particleSystem2.Play();
        }
    }
}
