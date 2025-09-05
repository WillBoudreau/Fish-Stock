using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayerX : MonoBehaviour
{
    public GameObject eyes;
    public GameObject blob = null;
    public GameObject gilbert = null;

    [SerializeField] private float width;
    [SerializeField] private float initialWidth;
    [SerializeField] private bool isLimit = false; //This is a bool that checks if there is a limit or not
    [SerializeField] private float maxWidth; //This tell the limit of the X position in the camera


    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject named Blob
        blob = GameObject.Find("Blob");

        // Find the GameObject named Gilbert
        gilbert = GameObject.Find("Gilbert");

        if (eyes != null)
            initialWidth = eyes.transform.position.x;

        isLimit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (blob != null && gilbert != null)
        {
            width = Mathf.Max(blob.transform.position.x, gilbert.transform.position.x);

            // This checks if the position of Blob or Gilbert is more than the current one, in case it is will also check if is less the the maximum of width
            if (width > initialWidth)
            {
                if ((isLimit && maxWidth > width) || !isLimit)
                {
                    eyes.transform.position = new Vector3(width, eyes.transform.position.y, eyes.transform.position.z);
                    
                }
            }
            else
            {
                eyes.transform.position = new Vector3(initialWidth, eyes.transform.position.y, eyes.transform.position.z);
            }
        }
    }
    ///<summary>
    /// Check if a player is outside of the camera view
    ///</summary>
    public bool IsPlayerOutOfView()
    {
        if (blob != null && gilbert != null)
        {
            // Get the position of the players
            float blobX = blob.transform.position.x;
            float gilbertX = gilbert.transform.position.x;

            // Get the position of the camera
            float cameraX = eyes.transform.position.x;

            // Check if either player is outside of the camera view
            if (blobX < cameraX - 10f || gilbertX < cameraX - 10f)
            {
                return true; // At least one player is out of view
            }
        }
        return false; // Both players are within view
    }
}
