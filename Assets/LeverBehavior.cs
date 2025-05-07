using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehavior : MonoBehaviour
{
    [Header("Lever references")]
    [SerializeField] private GameObject[] leverObjects; // Array of lever objects to be activated
    [SerializeField] private string playerNameID; // Name of the player to check for
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player is within the trigger area
            if (Input.GetKeyDown(KeyCode.E) && other.gameObject.name == playerNameID) 
            {
                Debug.Log("Player activated the lever: " + other.gameObject.name);
                ActivateLever();
            }
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
                lever.GetComponent<DoorBehavior>().OpenTheDoor(); 
            }
            else if(lever.tag == "MovablePlatform")
            {
                Debug.Log("Activating platform: " + lever.name); 
                lever.GetComponent<MovingPlatform>().StartMovingPlatform(); 
            }
        }
    } 
}
