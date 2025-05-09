using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [Header("Door Variables")]
    [SerializeField] private float doorSpeed = 2f; // Speed of the door movement
    [SerializeField] private float doorDistance = 5f; // Distance the door moves
    [SerializeField] private bool isOpen = false; // Is the door open or closed
    [SerializeField] private bool isStayOpen = false; // Is the door stay open or not
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
        }
    }
    /// <summary>
    /// Closes the door
    /// </summary>
    public void CloseTheDoor()
    {
        if (isOpen && !isStayOpen)
        {
            StartCoroutine(MoveDoor(closedPosition));
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
        if(isOpen)
        {
            isOpen = false; 
        }
        else
        {
            isOpen = true; 
        }
        transform.position = targetPosition; // Ensure the door reaches the exact position
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if(isOpen)
            {
                MoveDoor(openPosition);
            }
        }
    }
}
