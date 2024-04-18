using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{

    enum ItemType { Coin, Health }; //Creates an ItemType enum (enum is short for enumeration; it's basically a dropdown)
    [SerializeField] private ItemType itemType;

    //Reducing redundancy by creating reference to player
    NewPlayer newPlayer;

    // Start is called before the first frame update
    void Start()
    {
        //Make sure to put for every collectables script, since we want newPlayer to be referenced by every collectables type
        newPlayer = GameObject.Find("Player").GetComponent<NewPlayer>();
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

            newPlayer.coinsCollected++;
            newPlayer.UpdateUI();
            Destroy(gameObject, 0f);
        }
    }
}
