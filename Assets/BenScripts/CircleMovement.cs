using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    private float x;
    private float y;
    private float z;
    private float ix;  // Initial position in x
    private float iy; // Initial position in y
    private float iz; // Initial position in z
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _frequency = 1;


    // Start is called before the first frame update
    void Start()
    {
        // Getting the initial position of the enemy
        ix = transform.position.x;
        iy = transform.position.y;
        iz = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        x = Mathf.Cos(Time.time * _frequency) * _amplitude + ix;
        y = Mathf.Sin(Time.time * _frequency) * _amplitude + iy;
        z = transform.position.z + iz;

        transform.position = new Vector3(x, y, z);

    }
}
