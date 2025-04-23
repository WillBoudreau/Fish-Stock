using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableBox : MonoBehaviour
{
    public int maxHP;
    public int hp;
    public int currentHp; 

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP; 
        currentHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHp != hp) 
        {
            currentHp = hp;

            if(currentHp <= 0)
                this.gameObject.SetActive(false);
        }

        if(hp > maxHP)
            hp = maxHP;
    }

    public void getDamage(int damage) 
    {
        hp = hp - damage;
        if(hp < 0)
            hp = 0;
    }
}
