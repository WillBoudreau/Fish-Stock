using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [Header("Button Variables")]
    [SerializeField] private GameObject[] objectsToActivate; // Array of objects to activate
    [SerializeField] private string playerNameID; // Name of the player to check for

    void Start()
    {
        if (objectsToActivate.Length == 0) // Check if there are no objects to activate
        {
            Debug.LogWarning("No objects to activate assigned to the button. " + this.gameObject);
        }
        if(playerNameID == "") // Check if the player name ID is empty
        {
            Debug.LogWarning("No player name ID assigned to the button. " + this.gameObject); 
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == playerNameID) // Check if the player is touching the button
        {
            ActivateObjects(); 
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == playerNameID) // Check if the player is leaving the button
        {
            DeactivateObjects(); 
        }
    }

    /// <summary>
    /// Activates the objects assigned to the button
    /// </summary>
    /// <returns></returns>
    void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate) // Loop through each object in the array
        {
           if(obj.tag == "Door")
           {
                obj.GetComponent<DoorBehavior>().OpenTheDoor(); // Open the door
           }
        }
    }
    /// <summary>
    /// Deactivates the objects assigned to the button
    /// </summary>
    /// <returns></returns>
    void DeactivateObjects()
    {
        foreach (GameObject obj in objectsToActivate) // Loop through each object in the array
        {
            if (obj.tag == "Door")
            {
                obj.GetComponent<DoorBehavior>().CloseTheDoor(); // Close the door
            }
        }
    }
}
