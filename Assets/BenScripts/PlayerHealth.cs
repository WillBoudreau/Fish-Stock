using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthSystem;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem();
    [SerializeField] private GameManager gameManager;// TODO: FIND A BETTER WAY TO MANAGE GAME OVER
    public int playerHP;
    public TextMeshProUGUI playerHPtext;
    public string playerName;
    [Header("Health Test Settings")]
    [SerializeField] private int damageTest = 1;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.setMaxHP(10);
        healthSystem.health = healthSystem.maxHealth;
        playerHP = healthSystem.health;
        playerHPtext.text = playerName + " Health: " + playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthSystem.health != playerHP)
        {
            playerHP = healthSystem.health;
            playerHPtext.text = playerName + " Health: " + playerHP;

            if (playerHP <= 0)
            {
                gameManager.CheckGameOver(healthSystem.health, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(damageTest);
        }
    }
    /// <summary>
    /// Method to apply damage to the player.
    /// </summary>
    /// <param name="damageAmount">The amount of damage to apply.</param>
    public void TakeDamage( int damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
    }
}
