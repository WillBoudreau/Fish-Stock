using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLever : MonoBehaviour
{
    public GameObject lever = null;
    public KeyCode leverKey;
    public bool leverActivated = false;
    private Animator c_Animator; 

    // Start is called before the first frame update
    void Start()
    {
        c_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever != null && Input.GetKeyUp(leverKey)) 
        {
            lever.GetComponent<MechanicalSwitch>().activateSwitch();
            leverActivated = true;
            c_Animator.SetBool("Pull", true);
            Invoke("ExitPulling", 0.25f); 
        }

    }

    void OnTriggerEnter2D(Collider2D coll) 
    {
        if(coll.gameObject.CompareTag("Lever"))
        {
            lever = coll.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D coll) 
    {
        if(coll.gameObject.CompareTag("Lever"))
        {
            lever = null; 
        }
    }

    void ExitPulling() 
    {
        leverActivated = false;
        c_Animator.SetBool("Pull", false);
    }
}
