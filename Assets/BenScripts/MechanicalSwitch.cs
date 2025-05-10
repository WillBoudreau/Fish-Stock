using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalSwitch : MonoBehaviour
{

    public MechanicalPlatform mPlatform;

    [SerializeField] private bool activateMovement;
    private bool inMotion; 


    // Start is called before the first frame update
    void Start()
    {
        resetSwitch(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (activateMovement && !inMotion) 
        {
            if (!mPlatform.isRight && !mPlatform.moving)
            {
                Debug.Log("Going Right");
                mPlatform.GoRight();
                activateMovement = false;
            }
            else if (mPlatform.isRight && !mPlatform.moving)
            {
                Debug.Log("Going Left"); 
                mPlatform.GoLeft();
                activateMovement = false; 
            }
        }
    }

    private void resetSwitch() 
    {
        activateMovement = false;
        inMotion = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Colliding To Player"); 
            activateMovement = true; 
        }
    }


}
