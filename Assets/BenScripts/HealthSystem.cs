using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class HealthSystem
{
    // variables
    private int Health;

    public int health
    {
        get { return Health; }
        set { Health = Math.Max(0, Math.Min(MaxHealth, value)); }
    }

    private int MaxHealth = 100;   //The max hp
    public int maxHealth
    {
        get { return MaxHealth; }
        set { MaxHealth = Math.Max(1, Math.Min(100, value)); }
    }

    private int Life;
    public int life
    {
        get { return Life; }
        set { Life = Math.Max(0, value); }
    }

    public void TakeDamage(int damage) 
    {
        if(damage < health) 
        {
            health -= damage;
        }
        else 
        {
            health = 0;
            Revive(); 
        }
    }

    public void Revive()
    {
        // If the character has lives, loses one and restore his max hp
        if (life > 1)
        {
            life--;
            resetStats();
        }
        else
            life = 0;
    }

    public void resetStats() 
    {
        health = maxHealth; 
    }

    public void setMaxHP(int _maxhp)
    {
        // Set the max HP
        maxHealth = _maxhp;
    }

    public void recoverLife(int healing) 
    {
        // The hp recovers based on the input
        health += healing;
    }

}
