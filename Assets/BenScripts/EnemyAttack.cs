using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject playerObj = null;
    private bool readyToAttack = true;
    private float cooldownAtk = 2f;
    private PlayerHealth playerHealth = null; 
    private PlayerController playerController = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerObj != null && readyToAttack && !playerController.invincibility) 
        {
            Debug.Log("The enemy attacks!!");
            playerHealth.healthSystem.TakeDamage(2);
            playerController.DamageJump(); 
            readyToAttack = false;
            Invoke("GettingReadyToAttack", cooldownAtk); 
        }

    }


    void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.gameObject.CompareTag("Player") && playerObj == null) 
        {
            playerObj = coll.gameObject;
            playerHealth = playerObj.GetComponent<PlayerHealth>();
            playerController = playerObj.GetComponent<PlayerController>(); 
        }
    }

    void OnTriggerExit2D(Collider2D coll) 
    {
        if (coll.gameObject.CompareTag("Player") && playerObj != null) 
        {
            playerObj = null; 
        }
    }

    void GettingReadyToAttack() 
    {
        readyToAttack = true;
    }
}
