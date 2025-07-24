using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : MonoBehaviour
{

    public enum TypeOfItem 
    {
        Recovery,
        Speedy,
        Jump,
        None
    }

    public TypeOfItem _typeOfItem;

    public GameObject player = null;    

    public void ItemConsume() 
    {            
        ItemEffect();
    }

    private void ItemEffect() 
    {
        switch (_typeOfItem) 
        {
            case TypeOfItem.Recovery:
                player.GetComponent<PlayerHealth>().healthSystem.recoverLife(3);
                break;
            case TypeOfItem.Speedy:
                player.GetComponent<PlayerController>().IncreaseSpeed(2f);
                break;
            case TypeOfItem.Jump:
                player.GetComponent<PlayerController>().IncreaseJump(0.75f);
                break;
            

        }
    }

    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player")) 
        {
            player = col.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }


}
