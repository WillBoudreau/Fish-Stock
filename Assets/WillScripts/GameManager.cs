using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum gameState { MainMenu, InGame, Paused, GameOver, Winner, Loading, Credits }//The game states;
    [Header("Game Variables")]
    [SerializeField] private gameState currentGameState = gameState.MainMenu;//The current game state
    [SerializeField] private gameState previousGameState = gameState.MainMenu;//The previous game state
    public GameObject playerPrefab;//The player prefab
    [SerializeField] private bool isPaused;//Is the game paused
    [SerializeField] private bool hasWon;//Has the player won
    [SerializeField] private bool isGameOver;//Is the game over
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
    [SerializeField] private LevelManager levelManager;//The level manager

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
                break;
            case gameState.InGame:
                uIManager.SwitchUI(uIManager.hUD);
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

    public void RestartGame()
    {
        hasWon = false;
        isPaused = false;
        isGameOver = false;
        SetGameState(gameState.MainMenu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
