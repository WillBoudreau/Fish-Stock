using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [Header("End Level Trigger")]
    [Header("Level variables")]
    [SerializeField] private GameObject playerPrefab;//The player prefab
    [SerializeField] private GameObject player2Prefab;//The player prefab for player 2
    [SerializeField] private string nextLevelName;//The name of the next level to load
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
    [SerializeField] private LevelManager levelManager;//The level manager
    [SerializeField] private GameManager gameManager;//The game manager

    void Start()
    {
        //If the UIManager is null, find the UIManager
        if(uIManager == null)
        {
            uIManager = FindObjectOfType<UIManager>();
        }
        //If the GameManager is null, find the GameManager
        if(gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        //If the LevelManager is null, find the LevelManager
        if(levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
        playerPrefab = gameManager.playerPrefab;
        player2Prefab = gameManager.player2Prefab;
    }
    /// <summary>
    /// Check if the players have the required objects before moving on
    /// </summary>
    void CheckPlayers()
    {
        //Check if the players have the required objects before moving on
        if(playerPrefab != null && player2Prefab != null)
        {
            Debug.Log("Players have the required objects to move on to the next level.");
            levelManager.LoadScene(nextLevelName); // Load the next level
        }
        else
        {
            Debug.Log("Players do not have the required objects to move on to the next level.");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player has entered the trigger
        if(other.CompareTag("Player"))
        {
            CheckPlayers();
        }
    }
}
