using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    [Header("Lever references")]
    [SerializeField] private GameObject[] leverObjects; // Array of lever objects to be activated
    [SerializeField] private string playerNameID; // Name of the player to check for
    [SerializeField] private Animator leverAnimator; // Animator for the lever
    private bool playerInTrigger = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in the trigger area: " + other.gameObject.name);
            Debug.Log("Player name ID: " + playerNameID);
            playerInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the trigger area: " + other.gameObject.name);
            playerInTrigger = false;
        }
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player activated the lever.");
            ActivateLever();
        }
    }
    /// <summary>
    /// Activates the lever and performs the desired action.
    /// </summary>
    void ActivateLever()
    {
        Debug.Log("Lever activated!");
        foreach (GameObject lever in leverObjects)
        {
            if(lever.tag == "Door")
            {
                Debug.Log("Activating door: " + lever.name); 
                //leverAnimator.SetBool("Used", true); 
                lever.GetComponent<DoorBehavior>().OpenTheDoor(); 
            }
            else if(lever.tag == "MovablePlatform")
            {
                Debug.Log("Activating platform: " + lever.name); 
                if(lever.GetComponent<MovingPlatform>().isMoving)
                {
                    lever.GetComponent<MovingPlatform>().StopMovingPlatform();
                    //leverAnimator.SetBool("Used", false);
                }
                else
                {
                    leverAnimator.SetBool("Used", true);
                    //lever.GetComponent<MovingPlatform>().StartMovingPlatform(); 
                }
            }
        }
    } 
}
