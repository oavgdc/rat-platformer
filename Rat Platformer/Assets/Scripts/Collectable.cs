using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{

    enum ItemType { Coin, Health }; //Creates an ItemType enum (enum is short for enumeration; it's basically a dropdown)
    [SerializeField] private ItemType itemType;

    // Start is called before the first frame update
    void Start()
    {
        if (itemType == ItemType.Coin) 
        {
            Debug.Log("I'm a coin!");
        }
        if (itemType == ItemType.Health) 
        {
            Debug.Log("I'm a coin!");
        }   
        else 
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player is touching the gameObject that has this script, deactivate that gameObject 
        if (collision.gameObject.name == "Player") 
        {
            /* GameObject.Find("Player") tells Unity to search for a game object called Player
                .GetComponent<NewPlayer>() finds a component called NewPlayer which is the script in this case 
                .coinsCollected accesses coinsCollected variable
            */
            GameObject.Find("Player").GetComponent<NewPlayer>().coinsCollected++;
            Destroy(gameObject, 0f);
        }
    }
}
