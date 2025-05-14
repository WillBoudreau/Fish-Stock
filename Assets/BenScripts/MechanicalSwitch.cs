using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalSwitch : MonoBehaviour
{

    public MechanicalPlatform mPlatform;
    [SerializeField] private Animator c_Animator; 

    [SerializeField] private bool activateMovement;
    [SerializeField] private bool inMotion;
    private bool inGilbertHand = false; 



    // Start is called before the first frame update
    void Start()
    {
        resetSwitch();
        c_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activateMovement && !inMotion) 
        {
            Debug.Log("Switch is activated");
            if (!mPlatform.isRight && !mPlatform.moving)
            {
                inMotion = true;
                Debug.Log("Going Right");
                mPlatform.GoRight();
                desactivateSwitch();
            }
            else if (mPlatform.isRight && !mPlatform.moving)
            {
                inMotion = true; 
                Debug.Log("Going Left"); 
                mPlatform.GoLeft();
                desactivateSwitch();
            }

            
        }

        if (Input.GetKeyDown(KeyCode.K) && !activateMovement && inGilbertHand)
            activateSwitch(); 


        if(inMotion && !mPlatform.moving) 
        {
            inMotion = false;
            c_Animator.SetBool("Used", false);
        }

    }

    private void resetSwitch() 
    {
        desactivateSwitch(); 
        inMotion = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.name == "Gilbert")
        {
            Debug.Log("Colliding To Player");
            inGilbertHand = true; 
        }
    }

    void OnTriggerExit2D(Collider2D coll) 
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.name == "Gilbert")
        {            
            inGilbertHand = false;
        }
    }

    void desactivateSwitch() 
    {
        activateMovement = false;        
        //c_Animator.SetBool("Used", false); 
    }

    void activateSwitch() 
    {
        activateMovement = true;
        c_Animator.SetBool("Used", true);
    }

}
