using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningBoxes : MonoBehaviour
{
    public GameObject boxPrefab;
    public Vector3 spawiningPosition;
    private bool spawn = true; 

    // Start is called before the first frame update
    void Start()
    {
        SpawnBox(); 
    }    

    private void SpawnBox() 
    {
        if (spawn)
        {
            GameObject tempBox = Instantiate(boxPrefab, spawiningPosition, this.transform.rotation) as GameObject;
            Rigidbody2D tempRigidBodyBox = tempBox.GetComponent<Rigidbody2D>();
            tempRigidBodyBox.mass = 1000f;
            spawn = false;
            Invoke("AbleToSpawn", 0.5f); 
        }

    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        
        if (col.gameObject.name.Contains("PushableBoxType2"))
        {
            Debug.Log("Trigger" + col.name);
            Destroy(col.gameObject);
            SpawnBox();
            
        }
    }

    void AbleToSpawn() 
    {
        spawn = true; 
    }
}
