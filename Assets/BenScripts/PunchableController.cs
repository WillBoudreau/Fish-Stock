using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableController : MonoBehaviour
{
    public GameObject interactableObj = null;
    public KeyCode pushButton;
    public Animator c_Animator; // Set the variable animator
    // Start is called before the first frame update
    void Start()
    {
        c_Animator = gameObject.GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pushButton))
        {
            Debug.Log("Punch"); 
            c_Animator.SetBool("Punch", true); // Set to tru because Blob is punching

            if(interactableObj != null)
                interactableObj.GetComponent<PunchableBox>().getDamage(1);  //Only is effective if the punch is againts an punchable object

            
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
}
