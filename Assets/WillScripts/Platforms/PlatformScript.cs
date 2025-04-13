using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [Header("Platform Settings")]
    [SerializeField] private float normalSpeed = 5f; // Speed for normal platform
    [SerializeField] private float collapsingSpeed = 2f; // Speed for collapsing platform
    [SerializeField] private float spikeSpeed = 3f; // Speed for spike platform
    public int platformIndex; // Index of the platform
    public enum PlatformType { Normal, Collapsing, Spike } // Types of platforms
    public PlatformType platformType; // Type of the platform
    public List<GameObject> prefab = new List<GameObject>(); // Prefab of the platform
    public float speed; // Speed of the platform
    public bool isMoving; // Is the platform moving
    public bool isCollapsing; // Is the platform collapsing


    private void Start()
    {
        InitializePlatform();
    }
    private void Update()
    {
        MovePlatform();
    }
    /// <summary>
    /// Initializes the platform based on its type and speed.
    /// </summary>
    /// <returns></returns>
    private void InitializePlatform()
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
        }
        SetPlatformActive(platformIndex); 
    }
    /// <summary>
    /// Moves the platform based on its type and speed.
    /// </summary>
    /// <returns></returns>
    private void MovePlatform()
    {
        // Move the platform based on its speed and type
        switch (platformType)
        {
            case PlatformType.Normal:
                transform.Translate(Vector2.down* speed * Time.deltaTime); 
                break;
            case PlatformType.Collapsing:
                // Move collapsing platform
                break;
            case PlatformType.Spike:
                // Move spike platform
                break;
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
            prefab[platformIndex].SetActive(true);
            isMoving = true;
        }
        else
        {
            prefab[platformIndex].SetActive(false);
            isMoving = false;
        }
    }
}
