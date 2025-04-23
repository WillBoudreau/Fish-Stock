using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D pushBody;
    public bool isPush;
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        pushBody = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindObjectOfType<PlayerController>();        
        pushBody.bodyType = RigidbodyType2D.Static; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isPush)
        {
            pushBody.velocity = new Vector2(playerController.body.velocity.x , pushBody.velocity.y);
        }
        
    }

    public void BePushable()
    {
        if (!isPush)
        {
            isPush = true;
            pushBody.isKinematic = false;
            Debug.Log("Push"); 
        }
        else
        {
            isPush = false;
            pushBody.bodyType = RigidbodyType2D.Static;
            Debug.Log("Stop pushing"); 
        }
    }
    
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isPush = false;
            pushBody.bodyType = RigidbodyType2D.Static;
            Debug.Log("Exit collider"); 
        }
    }  

}
