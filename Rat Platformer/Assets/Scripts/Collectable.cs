using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{

    enum ItemType { Coin, Health, InventoryItem }; //Creates an ItemType enum (enum is short for enumeration; it's basically a dropdown)
    [SerializeField] private ItemType itemType;

    //Reducing redundancy by creating reference to player
    [SerializeField] private string inventoryStringName;
    [SerializeField] private Sprite inventorySprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the player is touching the gameObject that has this script, deactivate that gameObject 
        if (collision.gameObject == NewPlayer.Instance.gameObject) 
        {
            if (itemType == ItemType.Coin)
            {
                NewPlayer.Instance.coinsCollected++;
            }
            else if (itemType == ItemType.Health && NewPlayer.Instance.health == 2.5f)
            {
                NewPlayer.Instance.health += 0.5f;
            }
            else if (itemType == ItemType.Health && NewPlayer.Instance.health < 3.0f)
            {
                NewPlayer.Instance.health += 1.0f;
            }
            else if (itemType == ItemType.InventoryItem)
            { 
                NewPlayer.Instance.AddInventoryItem(inventoryStringName, inventorySprite);
            }
            
            NewPlayer.Instance.UpdateUI();
            Destroy(gameObject, 0f);
        }
    }
}
