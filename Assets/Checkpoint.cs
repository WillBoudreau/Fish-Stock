using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Checkpoint Variables")]
    public GameObject[] players; // Array of players
    [SerializeField] private bool isTriggered;

    [Header("Class calls")]
    [SerializeField] private GameManager gameManager; // Reference to the game manager
    [SerializeField] private PlatformManager platformManager; // Reference to the platform manager

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); // Find all players in the scene
        gameManager = FindObjectOfType<GameManager>(); // Find the game manager in the scene
        platformManager = FindObjectOfType<PlatformManager>(); // Find the platform manager in the scene
    }
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player"); // Find all players in the scene
    }
    /// <summary>
    /// When a player enters the checkpoint, set it as the current checkpoint and update the game manager's checkpoint index.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !isTriggered)
        {
            Debug.Log("Checkpoint reached by: " + other.gameObject.name); // Log the player who reached the checkpoint
            platformManager.SpawnNewPlatform(other.gameObject.transform.position);
            //gameManager.checkpointIndex = gameManager.checkpointIndex + 1; // Increment the checkpoint index
            isTriggered = true;
        }
    }
}
