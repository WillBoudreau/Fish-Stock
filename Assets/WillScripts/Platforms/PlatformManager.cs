using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Spawner Settings")]
    [SerializeField] private GameObject[] platforms; // Array of platform prefabs
    public List<GameObject> spawnedPlatforms = new List<GameObject>(); // List of spawned platforms
    [SerializeField] private Transform[] spawnPoint = new Transform[0]; // Array of spawn points for platforms
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns
    [SerializeField] private float distanceThreshold = 5f; // Distance threshold for spawning platforms
    [SerializeField] private float spawnDelay = 1f; // Delay before spawning platforms
    [SerializeField] private int maxPlatforms = 5; // Maximum number of platforms to spawn
    ///<summary>
    /// Spawns a new platform at the specified position.
    /// </summary>
    /// <returns></returns>
    public void SpawnNewPlatform()
    {
        if(spawnedPlatforms.Count >= maxPlatforms) // Check if the maximum number of platforms is reached
        {
            Debug.Log("Max platforms reached, not spawning new platform."); // Debug log for max platforms reached
            return; // Exit if the maximum number of platforms is reached
        }
        Debug.Log("SpawnNewPlatform called"); 
        Transform availibleSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)]; // Randomly select a spawn point
        GameObject platformPrefab = platforms[Random.Range(0, platforms.Length)]; 
        GameObject newPlatform = Instantiate(platformPrefab, availibleSpawnPoint.position, Quaternion.identity); 
        spawnedPlatforms.Add(newPlatform);
        StartCoroutine(CheckDistance()); // Start checking the distance after spawning a new platform
    }
    /// <summary>
    /// Shoots a ray from the spawn point to check for the distance from the previous platform.
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckDistance()
    {
        Debug.Log("CheckDistance called"); // Debug log for checking distance
        RaycastHit2D hit;

        foreach (Transform point in spawnPoint)
        {
            hit = Physics2D.Raycast(point.position, Vector2.down, distanceThreshold);
            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.transform.name); // Debug log for raycast hit
                if (hit.transform.CompareTag("Platform"))
                {
                    // If the distance is less than the threshold, wait for a while before checking again
                    yield return new WaitForSeconds(spawnInterval);
                }
                Debug.Log("Platform hit, checking distance."); // Debug log for platform hit
            }
            else
            {   
                Debug.Log("No platform hit, spawning new platform."); // Debug log for no platform hit
                spawnDelay = Random.Range(0.5f, 2f); // Randomize the spawn delay
                Debug.Log(spawnDelay);
                yield return new WaitForSeconds(spawnDelay); // Wait for the spawn delay
                // If the distance is greater than the threshold, spawn a new platform
                SpawnNewPlatform();
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Transform point in spawnPoint)
        {
            Gizmos.DrawLine(point.position, point.position + Vector3.down * distanceThreshold);
            Gizmos.DrawSphere(point.position, 5f); 
        }
    }
}
