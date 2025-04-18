using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float normalSpeed = 5f; // Speed for normal platform
    [SerializeField] private float spikeSpeed = 3f; // Speed for spike platform
    [SerializeField] private GameObject[] players; // Reference to the player object
    [SerializeField] private float collapsingSpeed = 2f; // Speed for collapsing platform
    [SerializeField] private Camera camera; // Reference to the camera
    [SerializeField] private PlatformManager platformManager; // Reference to the platform manager
    public int platformIndex; // Index of the platform
    public enum PlatformType { Normal, Collapsing, Spike,Extending } // Types of platforms
    public PlatformType platformType; // Type of the platform
    public List<GameObject> prefab = new List<GameObject>(); // Prefab of the platform
    public float speed; // Speed of the platform
    public bool isMoving; // Is the platform moving
    public bool canMove; // Can the platform move
    [SerializeField] private float distanceThreshold = 5f; // Distance threshold for destroying the platform
    PlatformCollapsingBehavior platformCollapsingBehavior;     
    private void Start()
    {
        platformCollapsingBehavior = GetComponent<PlatformCollapsingBehavior>(); // Initialize the collapsing behavior
        players = GameObject.FindGameObjectsWithTag("Player"); // Find all players in the scene
        platformManager = FindObjectOfType<PlatformManager>(); // Find the platform manager in the scene
        canMove = true; // Allow the platform to move
        InitializePlatform();
    }
    private void Update()
    {
        camera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>(); // Find the camera in the scene
        MovePlatform();
    }
    /// <summary>
    /// Initializes the platform based on its type and speed.
    /// </summary>
    /// <returns></returns>
    public void InitializePlatform()
    {
        switch (platformType)
        {
            case PlatformType.Normal:
                platformIndex = 0;
                speed = normalSpeed;
                break;
            case PlatformType.Collapsing:
                platformIndex = 1;
                speed = collapsingSpeed;
                break;
            case PlatformType.Spike:
                platformIndex = 2;
                speed = spikeSpeed;
                break;
            case PlatformType.Extending:
                platformIndex = 3;
                speed = normalSpeed; // Set a default speed for extending platforms
                break;
        }
        SetPlatformActive(platformIndex); 
    }
    /// <summary>
    /// Moves the platform based on its type and speed.
    /// </summary>
    /// <returns></returns>
    private void MovePlatform()
    {
        if(canMove == false)
        {
            return; // Exit if the platform cannot move
        }
        else
        {
            // Move the platform based on its speed and type
            switch (platformType)
            {
                case PlatformType.Normal:
                    transform.Translate(Vector2.down* speed * Time.deltaTime); 
                    DestroyPlatform();
                break;
                case PlatformType.Collapsing:
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
                case PlatformType.Spike:
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;
            }
        }
    }
    /// <summary>
    /// Set the platform to active when index mnatches
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public void SetPlatformActive(int index)
    {
        if (index == platformIndex)
        {
            if (platformIndex >= 0 && platformIndex < prefab.Count)
            {
                prefab[platformIndex].SetActive(true);
                isMoving = true;
            }
            else
            {
                Debug.LogWarning("Invalid platformIndex: " + platformIndex);
                isMoving = false;
            }
            isMoving = true;
        }
        else
        {
            prefab[platformIndex].SetActive(false);
            isMoving = false;
        }
    }
    /// <summary>
    /// When out of view of the camera's bottom view, destroy the platform
    /// </summary>
    void DestroyPlatform()
    {
        if (transform.position.y < camera.transform.position.y - distanceThreshold)
        {
            Destroy(gameObject);
            platformManager.spawnedPlatforms.Remove(gameObject); // Remove the platform from the spawned platforms list
        }
    }
    /// <summary>
    /// When the player is on the platform, set the platforms effect
    /// </summary>
    void SetEffect(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal:
                //No effect for normal platform
                break;
            case PlatformType.Collapsing:
                StartCoroutine(platformCollapsingBehavior.CollapsePlatform(0.5f));
                break;
            case PlatformType.Spike:
                // Set spike platform effect
                break;
            case PlatformType.Extending:
                // Set extending platform effect
                break;
        }
    }
    /// <summary>
    /// Trigger to check if the player is on the platform
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMove = false;
            SetEffect(platformType); 
        }
    }
    /// <summary>
    /// Trigger to check if the player is off the platform
    /// </summary>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMove = true; // Allow the platform to move again
        }
    }
}
public class PlatformCollapsingBehavior : MonoBehaviour
{
    [Header("Collasping Platform Settings")]
    [SerializeField] private float collapsingSpeed = 2f; // Speed for collapsing platform
    [SerializeField] private BoxCollider2D boxCollider; // Reference to the BoxCollider2D component
    public bool isCollapsing; // Is the platform collapsing
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); // Get the BoxCollider2D component
    }
    /// <summary>
    /// Set the platform to collapse when the player is on it after a delay
    /// </summary>
    /// param name="delay"></param>
    /// <returns></returns>
    public IEnumerator CollapsePlatform(float delay)
    {
        yield return new WaitForSeconds(delay);
        isCollapsing = true;
        collapsingSpeed = 15f;
        boxCollider.enabled = false; // Disable the BoxCollider2D component
        // Add collapsing logic here
    }

}
public class PlatformExtendingBehavior : MonoBehaviour
{
    [Header("Extending Platform Settings")]
    [SerializeField] private float extendingSpeed = 2f; // Speed for extending platform
    Scale scale; // Scale of the platform
    public bool isExtending; // Is the platform extending
    void Start()
    {
        // Initialize extending platform settings here
    }
    /// <summary>
    /// Set the platform to extend when the player is on it after a delay
    /// </summary>
    /// param name="delay"></param>
    /// <returns></returns>
    public IEnumerator ExtendPlatform(float delay)
    {
        yield return new WaitForSeconds(delay);
        isExtending = true; 
        extendingSpeed = 15f;
        this.gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // Set the scale of the platform
    }
    /// <summary>
    /// Set the platform to retract when the player is off it after a delay
    /// </summary>
    /// param name="delay"></param>
    /// <returns></returns>
    public IEnumerator RetractPlatform(float delay)
    {
        yield return new WaitForSeconds(delay);
        isExtending = false; 
        extendingSpeed = 15f;
        this.gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // Set the scale of the platform
    }
    public struct Scale
    {
        public float x;
        public float y;
        public float z;
        public Scale(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
