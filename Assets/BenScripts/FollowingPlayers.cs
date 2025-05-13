using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayers : MonoBehaviour
{
    
    public GameObject eyes;
    public GameObject blob = null;
    public GameObject gilbert = null;

    private float height; 

    // Start is called before the first frame update
    void Start()
    {

        // Find the GameObject named Blob
        blob = GameObject.Find("Blob");

        // Find the GameObject named Gilbert
        gilbert = GameObject.Find("Gilbert");

    }

    // Update is called once per frame
    void Update()
    {
        if (blob != null && gilbert != null)
        {
            height = Mathf.Max(blob.transform.position.y, gilbert.transform.position.y);
            eyes.transform.position = new Vector3(eyes.transform.position.x, height, eyes.transform.position.z);
        }
              
        
    }

    
}
