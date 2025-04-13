using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Spawner Settings")]
    public bool canSpawnPlatform = false; // Flag to control platform spawning
    [SerializeField] private GameObject platform; // Array of platform prefabs
    [SerializeField] private Transform[] spawnPoint; // Array of spawn points for platforms
    [SerializeField] private float spawnInterval = 2f; // Time interval between spawns
    [SerializeField] private float distanceThreshold = 5f; // Distance threshold for spawning platforms


    /// <summary>
    /// Coroutine to spawn platforms at regular intervals.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnPlatforms()
    {
        Debug.Log("Spawning platforms...");
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject newPlatform = Instantiate(platform, spawnPoint[i].position, Quaternion.identity);
            newPlatform.transform.SetParent(spawnPoint[i]);
            Debug.Log(spawnInterval);
            yield return new WaitForSeconds(spawnInterval); 
            Debug.Log("Platform spawned: " + newPlatform.name);
            Debug.Log("Platform spawned at: " + spawnPoint[i].position);
        }
        StartCoroutine(CheckDistance());
    }
    /// <summary>
    /// Shoots a ray from the spawn point to check for the distance from the previous platform.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckDistance()
    {
        RaycastHit hit;
        Vector3 direction = spawnPoint[0].position - spawnPoint[1].position;
        if (Physics.Raycast(spawnPoint[0].position, direction, out hit))
        {
            float distance = hit.distance;
            if (distance > distanceThreshold) 
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnPlatforms();
            }
        }
        else
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnPlatforms();
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
