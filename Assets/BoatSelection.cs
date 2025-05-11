using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BoatSelection : MonoBehaviour
{
    [Header("Boat Selection Properties")]
    [SerializeField] private GameObject[] boats; // Array of boat GameObjects
    [SerializeField] private Image player1Boat; // Player 1's selected boat
    [SerializeField] private Image player2Boat; // Player 2's selected boat
    [SerializeField] private SpriteRenderer player1BoatSpriteRenderer; // SpriteRenderer for player 1's boat
    [SerializeField] private SpriteRenderer player2BoatSpriteRenderer; // SpriteRenderer for player 2's boat
    [SerializeField] private int player1BoatIndex; // Index of player 1's selected boat
    [SerializeField] private int player2BoatIndex; // Index of player 2's selected boat

    [Header("Boat Selection UI Player1")]
    [SerializeField] private bool player1Ready; // Flag to check if player 1 is ready
    [SerializeField] private Button player1BoatButton; // Button for player 1's boat selection
    [SerializeField] private TextMeshProUGUI player1BoatText; // Text for player 1's boat selection
    [Header("Boat Selection UI Player2")]
    [SerializeField] private bool player2Ready; // Flag to check if player 2 is ready
    [SerializeField] private Button player2BoatButton; // Button for player 2's boat selection
    [SerializeField] private TextMeshProUGUI player2BoatText; // Text for player 2's boat selection
    [Header("Class References")]
    [SerializeField] private GameManager gameManager; // Reference to the GameManager
    [SerializeField] private LevelManager levelManager; // Reference to the LevelManager
    [SerializeField] private UIManager uIManager; // Reference to the UIManager


    void Start()
    {
        //player1BoatButton.interactable = false; // Disable the player 1 boat button initially
        // Initialize the boat selection UI
        UpdateBoatSelectionUI();
    }

    #region Player Boat Selection images

    /// <summary>
    /// Selects the boat for player 1
    /// </summary>
    /// <param name="boatIndex">The index of the selected boat</param>
    public void SelectPlayer1Boat(int boatIndex)
    {
        player1BoatIndex = boatIndex;
        TransferSpriteToPlayer1Boat(1f);
        UpdateBoatSelectionUI();
        player1BoatButton.interactable = true; // Enable the player 1 boat button
    }
    /// <summary>
    /// Selects the boat for player 2
    /// </summary>
    /// <param name="boatIndex">The index of the selected boat</param>
    public void SelectPlayer2Boat(int boatIndex)
    {
        player2BoatIndex = boatIndex;
        TransferSpriteToPlayer2Boat(1f);
        UpdateBoatSelectionUI();
    }

    #endregion
    #region Update Boat Selection UI

    /// <summary>
    /// Updates the boat selection UI
    /// </summary>
    private void UpdateBoatSelectionUI()
    {
        // Set the sprite for player 1's boat
        if (player1BoatIndex >= 0 && player1BoatIndex < boats.Length)
        {
            player1BoatSpriteRenderer.sprite = boats[player1BoatIndex].GetComponent<SpriteRenderer>().sprite;
        }
        // Set the sprite for player 2's boat
        if (player2BoatIndex >= 0 && player2BoatIndex < boats.Length)
        {
            player2BoatSpriteRenderer.sprite = boats[player2BoatIndex].GetComponent<SpriteRenderer>().sprite;
        }
    }

    #endregion
    #region Transfer Boat Sprite to image and set alpha

    /// <summary>
    /// Transfers the sprite from the selected boat to the player 1 boat image
    /// </summary>
    private void TransferSpriteToPlayer1Boat(float alpha)
    {
        if (player1BoatIndex >= 0 && player1BoatIndex < boats.Length)
        {
            Sprite selectedBoatSprite = boats[player1BoatIndex].GetComponent<SpriteRenderer>().sprite;
            Color color = player1Boat.color;
            color.a = alpha;
            player1Boat.sprite = selectedBoatSprite;
            player1Boat.color = color;
        }
    }
    /// <summary>
    /// Transfers the sprite from the selected boat to the player 2 boat image
    /// </summary>
    /// <param name="alpha">The alpha value for the image</param>
    private void TransferSpriteToPlayer2Boat(float alpha)
    {
        if (player2BoatIndex >= 0 && player2BoatIndex < boats.Length)
        {
            Sprite selectedBoatSprite = boats[player2BoatIndex].GetComponent<SpriteRenderer>().sprite;
            Color color = player2Boat.color;
            color.a = alpha;
            player2Boat.sprite = selectedBoatSprite;
            player2Boat.color = color;
        }
    }

    #endregion
    #region Ready Methods

    /// <summary>
    /// Display Ready button for player 1
    /// </summary>
    public void Player1Ready()
    {
        Debug.Log("Player 1 is ready"); // Debug log for player 1 readiness
        player1Ready = true;
        player1BoatText.text = "Ready"; // Update the text to show that player 1 is ready
        player1BoatButton.interactable = false; // Disable the player 1 boat button
        StartGame();
    }
    /// <summary>
    /// Display Ready button for player 2
    /// </summary>
    public void Player2Ready()
    {
        Debug.Log("Player 2 is ready"); // Debug log for player 2 readiness
        player2Ready = true;
        player2BoatText.text = "Ready"; // Update the text to show that player 2 is ready
        player2BoatButton.interactable = false; // Disable the player 2 boat button
        StartGame();
    }
    
    /// <summary>
    /// When all of the ready buttons are pressed, start the game
    /// </summary>
    public void StartGame()
    {
        if(player1Ready && player2Ready)
        {
            gameManager.SetGameState(GameManager.gameState.InGame); 
            gameManager.SetPlayerState(true); 
            levelManager.LoadScene("Level1"); 
            uIManager.SetFalse();
            uIManager.hUD.SetActive(true); 
        }
        else
        {
            Debug.Log("Not all players are ready to start the game.");
        }
    }

    #endregion
}
