using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parenting : MonoBehaviour
{

    [SerializeField] private Singleton singleton;

    void Start() 
    {
        singleton = GameObject.Find("Singleton").GetComponent<Singleton>();
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
            if (coll.gameObject.CompareTag("Player"))
            {
                coll.gameObject.transform.SetParent(singleton.transform);
            }
            else
            {
                coll.gameObject.transform.SetParent(null);
            }
        }
    }
}
