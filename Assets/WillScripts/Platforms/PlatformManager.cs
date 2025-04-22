using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Spawner Settings")]
    [SerializeField] private GameObject[] platforms; // Array of platform prefabs
    public List<GameObject> spawnedPlatforms = new List<GameObject>(); // List of spawned platforms
    public int maxPlatformsArea1 = 5; // Maximum number of platforms to spawn
    public int maxPlatformsArea2 = 5; // Maximum number of platforms to spawn
    public int maxPlatformsArea3 = 5; // Maximum number of platforms to spawn
    public int maxPlatformsArea4 = 5; // Maximum number of platforms to spawn

    [Header("Platform Spawn area settings")]
    [SerializeField] private Rect spawnArea1; // Spawn area for the first platform
    [SerializeField] private Rect spawnArea2; // Spawn area for the second platform
    [SerializeField] private Rect spawnArea3; // Spawn area for the third platform
    [SerializeField] private Rect spawnArea4; // Spawn area for the fourth platform
    [Header("Class References")]
    [SerializeField] private GameManager gameManager; // Reference to the game manager

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }
    /// <summary>
    /// Spawn a new platform at a random position within the designated spawn area.
    /// Ensure the first platform is within 5 units of the player.
    /// </summary>
    public void SpawnNewPlatform(Vector3 playerPOS)
    {
        if (spawnedPlatforms.Count < maxPlatformsArea1 && gameManager.checkpointIndex == 0)
        {
            StartCoroutine(SpawnPlatforms(spawnArea1, playerPOS));
        }
        else if (spawnedPlatforms.Count < maxPlatformsArea2 && gameManager.checkpointIndex == 1)
        {
            StartCoroutine(SpawnPlatforms(spawnArea2, playerPOS));
        }
        else if (spawnedPlatforms.Count < maxPlatformsArea3 && gameManager.checkpointIndex == 2)
        {
            StartCoroutine(SpawnPlatforms(spawnArea3, playerPOS));
        }
        else if (spawnedPlatforms.Count < maxPlatformsArea4 && gameManager.checkpointIndex == 3)
        {
            StartCoroutine(SpawnPlatforms(spawnArea4, playerPOS));
        }
    }
    /// <summary>
    /// Spawn a platform starting close to the player and then building up words towards the next area
    /// </summary>
    /// <param name="area"></param>
    /// <param name="playerPOS"></param>
    /// returns></returns>
    private IEnumerator SpawnPlatforms(Rect area, Vector3 playerPOS)
    {
        Vector3 spawnPosition = randomPosition;
        int maxAttempts = 10; 
        int attempts = 0; 
        if(attempts <= 5)
        {
            spawnPosition = playerPOS;
        }
        // Check if the platform is too close to the player
        while (Vector3.Distance(spawnPosition, playerPOS) < 1f || attempts < maxAttempts)
        {
            spawnPosition = randomPosition;
            attempts++;

            if (attempts >= maxAttempts)
            {
                Debug.Log("Max attempts reached for spawning platform. No valid position found.");
                break;
            }
            yield return null;
        }

        GameObject newPlatform = Instantiate(platforms[0], spawnPosition, Quaternion.identity);
        spawnedPlatforms.Add(newPlatform);
    }
    /// <summary>
    /// Select the designated spawn area based on checkpoint index from game manager.
    /// </summary>
    /// <returns></returns>
    private Vector3 randomPosition
    {
        get
        {
            Rect selectedArea = spawnArea1;

            if (gameManager == null)
            {
               gameManager = FindObjectOfType<GameManager>();
            }

            switch (gameManager.checkpointIndex)
            {
                case 0:
                    selectedArea = spawnArea1;
                    break;
                case 1:
                    selectedArea = spawnArea2;
                    break;
                case 2:
                    selectedArea = spawnArea3;
                    break;
                case 3:
                    selectedArea = spawnArea4;
                    break;
                default:
                    Debug.LogError("Invalid checkpoint index. Please set a valid checkpoint index in the GameManager.");
                    return Vector3.zero; // Return a default value or handle the error as needed
            }

            // Generate a random position within the selected area
            float xPos = Random.Range(selectedArea.xMin, selectedArea.xMax);
            float yPos = Random.Range(selectedArea.yMin, selectedArea.yMax);
            return new Vector3(xPos, yPos, 0); 
        }
    }
    /// <summary>
    /// Check to make sure that platforms do not overlap with each other.
    /// </summary>
    /// <param name="newPlatform"></param>
    /// <returns></returns>
    private bool IsPlatformOverlapping(GameObject newPlatform)
    {
        foreach (GameObject platform in spawnedPlatforms)
        {
            if (platform != newPlatform && platform.GetComponent<Collider2D>().bounds.Intersects(newPlatform.GetComponent<Collider2D>().bounds))
            {
                return true; 
            }
        }
        return false; 
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnArea1.center, spawnArea1.size); // Draw the first spawn area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnArea2.center, spawnArea2.size); // Draw the second spawn area
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(spawnArea3.center, spawnArea3.size); // Draw the third spawn area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea4.center, spawnArea4.size); // Draw the fourth spawn area
    }
}
