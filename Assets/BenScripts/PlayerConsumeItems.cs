using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumeItems : MonoBehaviour
{
    public GameObject itemObject = null;
    public KeyCode consumingItem; 

   // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(consumingItem) && itemObject != null) 
        {
            itemObject.GetComponent<ConsumableItem>().ItemConsume(); 
            itemObject.gameObject.SetActive(false);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Item")) 
        {
            itemObject = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Item"))
        {
            itemObject = null; 
        }
    }
}
