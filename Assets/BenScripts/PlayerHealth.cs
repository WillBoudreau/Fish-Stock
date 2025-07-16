using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthSystem;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    public HealthSystem healthSystem = new HealthSystem(); 
    public int playerHP;
    public TextMeshProUGUI playerHPtext; 
    public string playerName;

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
        if(healthSystem.health != playerHP) 
        {
            playerHP = healthSystem.health;
            playerHPtext.text = playerName + " Health: " + playerHP;
        }
    }
}
