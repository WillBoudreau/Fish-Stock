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


    // Start is called before the first frame update
    void Start()
    {
        // Find the GameObject named Blob
        blob = GameObject.Find("Blob");

        // Find the GameObject named Gilbert
        gilbert = GameObject.Find("Gilbert");

        if(eyes != null)
            initialWidth = eyes.transform.position.x; 
    }

    // Update is called once per frame
    void Update()
    {
        if (blob != null && gilbert != null)
        {
            width = Mathf.Max(blob.transform.position.x, gilbert.transform.position.x);

            if (width> initialWidth)
            {
                eyes.transform.position = new Vector3(width, eyes.transform.position.y, eyes.transform.position.z);
            }
            else 
            {
                eyes.transform.position = new Vector3(initialWidth, eyes.transform.position.y, eyes.transform.position.z);
            }
        }
    }
}
