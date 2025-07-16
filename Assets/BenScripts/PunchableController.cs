using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableController : MonoBehaviour
{
    public GameObject interactableObj = null;
    public KeyCode pushButton;
    public Animator c_Animator; // Set the variable animator
    public PlayerController playerController; 
    // Start is called before the first frame update
    void Start()
    {
        c_Animator = gameObject.GetComponent<Animator>();
        playerController = this.GetComponent<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(interactableObj != null)
        {
            if(Input.GetKeyDown(pushButton))
            {
                playerController.invincibility = true; 
                c_Animator.SetBool("Punch", true); // Set to tru because Blob is punching
                interactableObj.GetComponent<PunchableBox>().getDamage(1);  //Only is effective if the punch is againts an punchable object
                Invoke("CancelPunch", 0.25f); 
            }
                
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Punchable"))
        {
            interactableObj = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Punchable"))
        {
            interactableObj = null;
        }
    }

    void CancelPunch() 
    {
        c_Animator.SetBool("Punch", false);
        playerController.invincibility = false;
    }
}
