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
    [SerializeField] private bool isAbleToMoveOn = false;//Is the player able to move on to the next level
    [SerializeField] private List<GameObject> requiredObjects = new List<GameObject>();//The list of required objects to move on to the next level
    [SerializeField] private int numberOfPlayers = 0;//The number of players in the trigger before moving on
    [SerializeField] private SpriteRenderer spriteRenderer;//The sprite renderer for the trigger
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
        foreach (GameObject coin in requiredObjects)
        {
            if(coin.GetComponent<PickUpObject>().isPickedUp == true)
            {
                isAbleToMoveOn = true; 
                Debug.Log("Player has the required objects to move on to the next level.");
            }
            else
            {
                isAbleToMoveOn = false; 
                Debug.Log("Player does not have the required objects to move on to the next level.");
            }
        }
        if(isAbleToMoveOn)
        {
            //Check if the players have the required objects before moving on
            if(playerPrefab != null && player2Prefab != null)
            {
                Debug.Log("Players have the required objects to move on to the next level.");
                if(nextLevelName == "Win")
                {
                    Debug.Log("Players have won the game!");
                    uIManager.UILoadingScreen(uIManager.winnerScreen); // Load the winner screen
                }
                else
                {
                    playerPrefab.GetComponent<PlayerController>().enabled = false; // Disable player controls
                    player2Prefab.GetComponent<PlayerController>().enabled = false; // Disable player 2 controls
                    levelManager.LoadScene(nextLevelName); // Load the next level
                }
            }
            else
            {
                Debug.Log("Players do not have the required objects to move on to the next level.");
            }
        }
    }
    /// <summary>
    /// Strobe effect for the sprite renderer to indicate the end level trigger for the player
    /// </summary>
    public void StrobeEffect()
    {
        if(spriteRenderer != null)
        {
            Color strobeColor = Color.blue; 
            spriteRenderer.color = new Color(strobeColor.r, strobeColor.g, strobeColor.b, Mathf.PingPong(Time.time, 1f)); // Strobe effect for the sprite renderer
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the player has entered the trigger
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the trigger to move on to the next level.");
            numberOfPlayers++;
            if(numberOfPlayers >= 2)
            {
                CheckPlayers(); // Check if the players have the required objects before moving on
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Check if the player has exited the trigger
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the trigger to move on to the next level.");
            numberOfPlayers--;
        }
    }
    void Update()
    {
        StrobeEffect();
        foreach (GameObject coin in GameObject.FindGameObjectsWithTag("PickUp"))
        {
            requiredObjects.Add(coin);
        }
    }
}
