using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour
{
    [Header("End Level Trigger")]
    [Header("Level variables")]
    [SerializeField] private GameObject endLevelTrigger;//The end level trigger
    [SerializeField] private GameObject playerPrefab;//The player prefab
    [SerializeField] private GameObject player2Prefab;//The player prefab for player 2
    [Header("Class References")]
    [SerializeField] private UIManager uIManager;//The UI Manager
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
        playerPrefab = gameManager.playerPrefab;
        player2Prefab = gameManager.player2Prefab;
    }
}
