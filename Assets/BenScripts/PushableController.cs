using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableController : MonoBehaviour
{
    public GameObject interactableObj = null;
    public KeyCode pushButton;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pushButton) && interactableObj != null)
        {
            interactableObj.GetComponent<PushableObject>().BePushable();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Pushable"))
        {
            interactableObj = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Pushable"))
        {
            interactableObj = null;
        }
    }

    
}
