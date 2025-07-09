using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject pointObjA;
    public GameObject pointObjB;
    public Vector3 initialPosition;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private float dx;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = this.gameObject.transform.position;
        currentPoint = pointObjB.transform;
        rb = GetComponent<Rigidbody2D>();
        dx = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoint == pointObjB.transform)
        {
            rb.velocity = new Vector2(dx, 0);
        }
        else if (currentPoint == pointObjA.transform)
        {
            rb.velocity = new Vector2(-dx, 0);
        }


        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointObjB.transform)
        {
            currentPoint = pointObjA.transform;
            Debug.Log("ToPointA");
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointObjA.transform)
        {
            currentPoint = pointObjB.transform;
            Debug.Log("ToPointB");
        }
    }
}
