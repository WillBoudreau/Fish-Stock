using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D pushBody;
    public bool isPush;
    public PlayerController playerController;
    public GameObject playerOne;      


    // Start is called before the first frame update
    void Start()
    {
        pushBody = GetComponent<Rigidbody2D>();             
        //pushBody.bodyType = RigidbodyType2D.Static;       
    }    

    // Update is called once per frame
    void Update()
    {
        if(playerOne == null) 
        {
            playerOne = GameObject.Find("Blob");

            if (playerOne != null)
            {
                playerController = playerOne.GetComponent<PlayerController>();

                if (playerController != null)
                {
                    Debug.Log("Script Found");
                }
                else
                {
                    Debug.Log("There is no player Controller");
                }
            }
            else
            {
                Debug.Log("Player not found");
            }
        }



        if (isPush && !playerController.isJumping)
        {
            pushBody.velocity = new Vector2(playerController.body.velocity.x , pushBody.velocity.y);
        }        

    }

    public void BePushable()
    {
        if (!isPush)
        {            
            isPush = true;
            playerController.isPushing = true;
            //pushBody.isKinematic = false;
            Debug.Log("Push"); 
        }
        else
        {
            isPush = false;
            pushBody.velocity = new Vector2(0, 0);
            playerController.isPushing = false; 
            //pushBody.bodyType = RigidbodyType2D.Static;
            Debug.Log("Stop pushing"); 
        }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPush = false;
            //pushBody.bodyType = RigidbodyType2D.Static;
            Debug.Log("Exit collider");
        }
        else if (col.gameObject.CompareTag("Mechanical"))
        {
            //pushBody.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.CompareTag("Mechanical"))
        {
            //pushBody.isKinematic = false;
        }
    }


}
