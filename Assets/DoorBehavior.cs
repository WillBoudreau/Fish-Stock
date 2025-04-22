using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [Header("Door Variables")]
    [SerializeField] private float doorSpeed = 2f; // Speed of the door movement
    [SerializeField] private float doorDistance = 5f; // Distance the door moves
    [SerializeField] private bool isOpen = false; // Is the door open or closed
    [SerializeField] private Vector3 closedPosition; // Closed position of the door
    [SerializeField] private Vector3 openPosition; // Open position of the door

    void Start()
    {
        GetDoorPositions(); 
    }
    /// <summary>
    /// Get the closed and open positions of the door
    /// </summary>
    /// <param name="closedPos"></param>
    /// <param name="openPos"></param>
    /// <returns></returns>
    public void GetDoorPositions()
    {
        closedPosition = transform.position;
        openPosition = new Vector3(closedPosition.x, closedPosition.y + doorDistance, closedPosition.z);
    }
    /// <summary>
    /// Opens the door
    /// </summary>
    public void OpenTheDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoor(openPosition));
            isOpen = true;
        }
    }
    /// <summary>
    /// Closes the door
    /// </summary>
    public void CloseTheDoor()
    {
        if (isOpen)
        {
            StartCoroutine(MoveDoor(closedPosition));
            isOpen = false;
        }
    }
    /// <summary>
    /// Moves the door to the specified position
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private IEnumerator MoveDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, doorSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; // Ensure the door reaches the exact position
    }
}
