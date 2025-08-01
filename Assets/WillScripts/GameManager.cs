using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum gameState { MainMenu, InGame, Paused, GameOver, Winner, Loading, Credits }//The game states;
    [Header("Game Variables")]
    public gameState currentGameState = gameState.MainMenu;//The current game state
    public gameState previousGameState = gameState.MainMenu;//The previous game state
    public GameObject playerPrefab;//The player prefab
    public GameObject player2Prefab;//The player prefab for player 2
    [SerializeField] private bool isPaused;//Is the game paused
    [SerializeField] private bool hasWon;//Has the player won
    [SerializeField] private bool isGameOver;//Is the game over
    public Camera mainCamera;//The main camera

    [Header("Debug Controls")]
    [SerializeField] private bool debugControls = false; // Enable debug controls for testing purposes
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
    [SerializeField] private LevelManager levelManager;//The level manager
    [SerializeField] private MusicManager musicManager;//The music manager
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        levelManager = FindObjectOfType<LevelManager>();
        musicManager = FindObjectOfType<MusicManager>();

        mainCamera = Camera.main;
        SetGameState(currentGameState);//Set the game state to the current game state
        
        if (levelManager.sceneName == "GamePlayScene")
        {
            SetGameState(gameState.InGame);
        }
        else if (levelManager.sceneName == "Level1")
        {
            SetGameState(gameState.InGame);
        }
    }
    void Update()
    {
        if (debugControls)
        {
            DebugControls();
        }
        
    }
    void DebugControls()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            levelManager.LoadScene("Level1");
            SetGameState(gameState.InGame);
            uIManager.SwitchUI(uIManager.hUD);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            levelManager.LoadScene("Level3");
            SetGameState(gameState.InGame);
            uIManager.SwitchUI(uIManager.hUD);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            levelManager.LoadScene("Level2");
            SetGameState(gameState.InGame);
            uIManager.SwitchUI(uIManager.hUD);
        }
    }
    /// <summary>
    /// Set the game state to the specified state
    /// </summary>
    /// <param name="state">The state to set the game to</param>
    public void SetGameState(gameState state)
    {
        previousGameState = currentGameState;
        currentGameState = state;
        switch (currentGameState)
        {
            case gameState.MainMenu:
                uIManager.SwitchUI(uIManager.mainMenu);
                SetPlayerState(false);
                break;
            case gameState.InGame:
                uIManager.SwitchUI(uIManager.hUD);
                SetPlayerState(true);
                break;
            case gameState.Paused:
                uIManager.SwitchUI(uIManager.pauseMenu);
                break;
            case gameState.GameOver:
                uIManager.SwitchUI(uIManager.gameOverScreen);
                break;
            case gameState.Winner:
                uIManager.SwitchUI(uIManager.winnerScreen);
                break;
            case gameState.Loading:
                uIManager.SwitchUI(uIManager.loadingScreen);
                break;
            case gameState.Credits:
                uIManager.SwitchUI(uIManager.creditsScreen);
                break;
        }
    }
    /// <summary>
    /// Sers the state of the player to the specified state
    /// </summary>
    /// <param name="state">The state to set the player to</param>
    public void SetPlayerState(bool state)
    {
        playerPrefab.SetActive(state);
        player2Prefab.SetActive(state);
    }

    /// <summary>
    /// Checks the health of both of the players and sets the game state accordingly
    /// </summary>
    /// <param name="playerHealth">The health of player 1</param>
    /// <param name="player2Health">The health of player 2</param>
    /// <returns>True if the game is over, false otherwise</returns>
    public bool CheckGameOver(int playerHealth, int player2Health)
    {
        if (playerHealth <= 0 && player2Health <= 0)
        {
            isGameOver = true;
            SetGameState(gameState.GameOver);
            return true;
        }
        else if (playerHealth <= 0)
        {
            isGameOver = true;
            SetGameState(gameState.GameOver);
            return true;
        }
        else if (player2Health <= 0)
        {
            isGameOver = true;
            SetGameState(gameState.GameOver);
            return true;
        }
        return false;
    }

    public void RestartGame()
    {
        levelManager.LoadScene("MainMenuScene");
        hasWon = false;
        isPaused = false;
        isGameOver = false;
        playerPrefab.GetComponent<PlayerHealth>().healthSystem.resetStats();
        player2Prefab.GetComponent<PlayerHealth>().healthSystem.resetStats();
        SetGameState(gameState.MainMenu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
