using System.Collections;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
    // Struct to store settings for each trigger
    [System.Serializable]
    public struct SpawnSettings
    {
        public GameObject prefabToSpawn;  // Prefab to spawn
        public float spawnDelay;          // Delay before spawning
        public Vector3 spawnVelocity;     // Velocity for the spawned prefab
        public GameObject spawnLocation;  // Spawn location reference
    }

    // Settings for Trigger 1, 2, and 3
    public SpawnSettings trigger1Settings;
    public SpawnSettings trigger2Settings;
    public SpawnSettings trigger3Settings;

    public void Start()
    {
        GetComponent<AnimatorTriggerer>().Trigger1Event += Trigger1;

        GetComponent<AnimatorTriggerer>().Trigger2Event += Trigger2;

        GetComponent<AnimatorTriggerer>().Trigger3Event += Trigger3;
    }

    // Method to fire Trigger 1
    public void Trigger1()
    {
        StartCoroutine(SpawnPrefab(trigger1Settings));
    }

    // Method to fire Trigger 2
    public void Trigger2()
    {
        StartCoroutine(SpawnPrefab(trigger2Settings));
    }

    // Method to fire Trigger 3
    public void Trigger3()
    {
        StartCoroutine(SpawnPrefab(trigger3Settings));
    }

    // Coroutine to spawn a prefab based on the provided settings
    private IEnumerator SpawnPrefab(SpawnSettings settings)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(settings.spawnDelay);

        // Determine the spawn position based on the provided spawn location GameObject
        Vector3 spawnPosition = (settings.spawnLocation != null) ? settings.spawnLocation.transform.position : transform.position;

        // Instantiate the prefab at the spawn location's position and rotation
        GameObject spawnedObject = Instantiate(settings.prefabToSpawn, spawnPosition, transform.rotation);

        // Set the velocity in MoveInDirection script if it exists on the spawned prefab
        MoveInDirection moveScript = spawnedObject.GetComponent<MoveInDirection>();
        if (moveScript != null)
        {
            moveScript.velocity = settings.spawnVelocity;
        }
    }
}
