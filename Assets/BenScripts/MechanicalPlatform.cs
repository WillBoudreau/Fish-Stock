using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicalPlatform : MonoBehaviour
{

    public Vector2 Point1;
    public Vector2 Point2;

    private Vector2 tempPoint1;
    private Vector2 tempPoint2;

    private float ElapseDuration;
    private float TimeElapse = 0;

    private float MvX;
    private float MvY;

    public bool moving = false;
    public bool isRight = true;

    // Start is called before the first frame update
    void Start()
    {
        Point1 = this.gameObject.transform.position;
        ElapseDuration = 5f;
        tempPoint1 = Point1;
        tempPoint2 = Point2;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (TimeElapse < ElapseDuration)
            {
                float t = TimeElapse / ElapseDuration;
                MvX = Mathf.Lerp(tempPoint1.x, tempPoint2.x, t);
                MvY = Mathf.Lerp(tempPoint1.y, tempPoint2.y, t);
                TimeElapse += Time.deltaTime;
            }
            else
            {
                MvX = tempPoint2.x;
                MvY = tempPoint2.y;
                moving = false;
                TimeElapse = 0;
            }

            this.gameObject.transform.position = new Vector3(MvX, MvY, 0);
        }
    }

    public void GoRight()
    {
        tempPoint1 = Point2;
        tempPoint2 = Point1;
        moving = true;
        isRight = true;
    }

    public void GoLeft()
    {
        tempPoint1 = Point1;
        tempPoint2 = Point2;
        moving = true;
        isRight = false;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Pushable"))
        {
            coll.gameObject.transform.SetParent(transform); 
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") || coll.gameObject.CompareTag("Pushable"))
        {
            coll.gameObject.transform.SetParent(null); 
        }
    }

}
