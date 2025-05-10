using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupMechanic : MonoBehaviour
{
    public GameObject pickupObject = null;
    public KeyCode pickupKey; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickupKey)) 
        {
            if (pickupObject != null)
            {
                pickupObject.GetComponent<PickUpObject>().PickUp(gameObject.name); 
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PickUp"))
        {
            pickupObject = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("PickUp"))
        {
            pickupObject = null;
        }
    }
}
