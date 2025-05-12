using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform Variables")]
    [SerializeField] private float platformSpeed = 2f; // Speed of the platform movement
    [SerializeField] private float platformDistance = 5f; // Distance the platform moves
    [SerializeField] private bool isMoving = false; // Is the platform moving or not
    [SerializeField] private Vector3 startPosition; // Starting position of the platform
    [SerializeField] private Vector3 endPosition; // Ending position of the platform
    [SerializeField] private Vector3 movementAxis; // Axis of movement
    [SerializeField] private bool isStayingUp = false; // Is the platform staying up or not

    void Start()
    {
        GetPlatformPositions(); // Get the starting and ending positions of the platform
    }
    /// <summary>
    /// Get the starting and ending positions of the platform
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public void GetPlatformPositions()
    {
        startPosition = transform.position; // Get the starting position of the platform
        if(movementAxis.x > 0)
        {
            endPosition = new Vector3(startPosition.x + platformDistance, startPosition.y, startPosition.z);
        }
        else if(movementAxis.y > 0)
        {
            endPosition = new Vector3(startPosition.x, startPosition.y + platformDistance, startPosition.z); 
        }
        else if(movementAxis.z > 0)
        {
            endPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z + platformDistance); 
        }
    }
    /// <summary>
    /// Moves the platform between the start and end positions
    /// </summary>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private IEnumerator MovePlatform(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, platformSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition; 
    }
    /// <summary>
    /// Start moving the platform
    /// /// </summary>
    public void StartMovingPlatform()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MovePlatform(endPosition)); // Start moving the platform to the end position
        }
    }
    /// <summary>
    /// Stop moving the platform
    /// </summary>
    public void StopMovingPlatform()
    {
        if (isMoving)
        {
            isMoving = false;
            if(isStayingUp)
            {
                transform.position = endPosition; // Set the platform to the end position
            }
            else
            {
                StartCoroutine(MovePlatform(startPosition)); // Start moving the platform back to the start position
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(transform); // Set the player as a child of the platform
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.SetParent(null); // Remove the player from the platform
        }
    }
}
