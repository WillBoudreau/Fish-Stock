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
    public bool isPickedUp = false; // Flag to check if the object is picked up
    public SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                isPickedUp = true; 
                spriteRenderer.enabled = false; // Hide the object
            }
        }
        else
        {
            Debug.Log("Picking up object" + gameObject.name);
            this.gameObject.SetActive(false);
        }
    }
}
