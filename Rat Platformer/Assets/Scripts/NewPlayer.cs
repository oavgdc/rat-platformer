using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : PhysicsObject
{

    //Movement
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    
    //Health
    public float health = 3f;
    public Image[] hearts;
    [SerializeField] public Sprite heartFull;
    [SerializeField] public Sprite heartHalf;
    [SerializeField] public Sprite heartEmpty;
    
    //Coins
    public int coinsCollected = 0;
    public Text coinsText;

    //Inventory (Item)
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    public Image inventoryItemImage;
    public Sprite keySprite;
    public Sprite inventoryItemBlank;

    //Singleton instantiation (so we don't have to write GameObject.GetComponent
    private static NewPlayer instance;
    public static NewPlayer Instance 
    { 
        get
        {
            if(instance == null) instance = GameObject.FindObjectOfType<NewPlayer>();
            return instance;
        }
    }

    //Inventory (Weapon)

    // Start is called before the first frame update
    void Start()
    {
        health = 3f;
        UpdateUI();

        //Note: since coinsText UI element is only every gonna be used one time, we can just assign reference
        //in unity inspector by dragging and dropping onto text field (in other words, below line is unnecessary)
        //below line would be good for multiple objects referencing the same thing 

        //coinsText = GameObject.Find("Coins").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

    }

    //Update UI elements
    public void UpdateUI() 
    {
        //Update Coins
        coinsText.text = coinsCollected.ToString() + " Coins";

        //Update Health
        for (int i = 0; i < hearts.Length; i++) 
        {
            if (health >= i + 1)
            {
                hearts[i].sprite = heartFull;
            }
            else if (i < health && health < i + 1)
            {
                hearts[i].sprite = heartHalf;
            }
            else
            {
                hearts[i].sprite = heartEmpty;
            }
        
        }

        //Update Inventory
        

    } 

    public void AddInventoryItem(string inventoryItemName, Sprite image)
    {
        inventory.Add(inventoryItemName, image);
        inventoryItemImage.sprite = inventory[inventoryItemName];

    }

    public void RemoveInventoryItem(string inventoryItemName)
    {
        inventory.Remove(inventoryItemName);
        inventoryItemImage.sprite = inventoryItemBlank;

    }

}
