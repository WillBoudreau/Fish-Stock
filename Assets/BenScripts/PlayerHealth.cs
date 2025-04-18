using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthSystem;
using TMPro; 

public class PlayerHealth : MonoBehaviour
{
    private HealthSystem healthSystem = new HealthSystem(); 
    public int playerHP;
    public TextMeshProUGUI playerHPtext; 

    // Start is called before the first frame update
    void Start()
    {
        healthSystem.setMaxHP(10); 
        healthSystem.health = healthSystem.maxHealth;
        playerHP = healthSystem.health;
        playerHPtext.text = "Health: " + playerHP; 
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.health != playerHP) 
        {
            playerHP = healthSystem.health;
            playerHPtext.text = "Health: " + playerHP;
        }
    }
}
