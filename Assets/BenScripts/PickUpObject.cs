using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public enum Owner 
    {
        Blob,
        Gilbert,
        None
    }

    public Owner pickupOwner; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(string playerName)
    {
        if (pickupOwner.ToString() != "None")
        {
            if (playerName == pickupOwner.ToString())
            {
                Debug.Log("Picking up object" + gameObject.name);
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Picking up object" + gameObject.name);
            this.gameObject.SetActive(false);
        }
    }
}
