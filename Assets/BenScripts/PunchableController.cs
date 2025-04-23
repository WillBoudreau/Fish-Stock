using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchableController : MonoBehaviour
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
            interactableObj.GetComponent<PunchableBox>().getDamage(1);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Punchable"))
        {
            interactableObj = col.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Punchable"))
        {
            interactableObj = null;
        }
    }
}
