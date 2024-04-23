using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{

    enum ItemType { Coin, Health, InventoryItem }; //Creates an ItemType enum (enum is short for enumeration; it's basically a dropdown)
    [SerializeField] private ItemType itemType;

    //Reducing redundancy by creating reference to player
    NewPlayer newPlayer;
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;

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
            if (itemType == ItemType.Coin)
            {
                newPlayer.coinsCollected++;
            }
            else if (itemType == ItemType.Health && newPlayer.health == 2.5f)
            {
                newPlayer.health += 0.5f;
            }
            else if (itemType == ItemType.Health && newPlayer.health < 3.0f)
            {
                newPlayer.health += 1.0f;
            }
            else if (itemType == ItemType.InventoryItem)
            { 
                newPlayer.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            
            newPlayer.UpdateUI();
            Destroy(gameObject, 0f);
        }
    }
}
