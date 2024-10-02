using UnityEngine;
using UnityEngine.UI;
using System;

public class ParticleController : MonoBehaviour
{
    // Public fields for the particle system prefabs
    public ParticleSystem particlePrefab1;
    public ParticleSystem particlePrefab2;
    public ParticleSystem particlePrefab3;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure all particle systems are turned off at the start
        particlePrefab1.Stop();

        particlePrefab2.Stop();

        particlePrefab3.Stop();

        GetComponent<AnimatorTriggerer>().Trigger1Event += ToggleParticle1;

        GetComponent<AnimatorTriggerer>().Trigger2Event += ToggleParticle2;

        GetComponent<AnimatorTriggerer>().Trigger3Event += ToggleParticle3;
    }

    // Function to toggle Particle 1 on, and the others off
    public void ToggleParticle1()
    {
        // Turn off the other particles
        particlePrefab2.Stop();
        particlePrefab3.Stop();

        // Toggle particlePrefab1
        if (particlePrefab1.isPlaying)
        {
            particlePrefab1.Stop();
        }
        else
        {
            particlePrefab1.Play();
        }
    }

    // Function to toggle Particle 2 on, and the others off
    public void ToggleParticle2()
    {
        // Turn off the other particles
        particlePrefab1.Stop();
        particlePrefab3.Stop();

        // Toggle particlePrefab2
        if (particlePrefab2.isPlaying)
        {
            particlePrefab2.Stop();
        }
        else
        {
            particlePrefab2.Play();
        }
    }

    // Function to toggle Particle 3 on, and the others off
    public void ToggleParticle3()
    {
        // Turn off the other particles
        particlePrefab1.Stop();
        particlePrefab2.Stop();

        // Toggle particlePrefab3
        if (particlePrefab3.isPlaying)
        {
            particlePrefab3.Stop();
        }
        else
        {
            particlePrefab3.Play();
        }
    }
}
