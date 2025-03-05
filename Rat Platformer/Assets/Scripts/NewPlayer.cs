using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : PhysicsObject
{

    //Animator
    public Animator playerAnimator;

    //Movement
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    
    //Health
    public float health = 3f;
    public Image[] hearts;
    public Vector3 respawnPosition;
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

    //Attacking
    [SerializeField] private GameObject attackBox;
    [SerializeField] private float cooldown;
    [SerializeField] private float attackTimer;

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

        attackBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        attackTimer -= Time.deltaTime;

        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);

        //Respawn
        if(health == 0) 
        {
            Respawn();
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower;
        }

        //Flip Player
        if(targetVelocity.x < -0.1)
        {
            transform.localScale = new Vector2(-1, 1);
        } 
        else if (targetVelocity.x > 0.1)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //Attacking
        if(Input.GetButtonDown("Fire1"))
        {
            if(attackTimer > 0)
            {
                attackBox.SetActive(false);
                return;
            }

            attackTimer = cooldown;
            playerAnimator.SetBool("isSwingingSword", true);
            attackBox.SetActive(true);
            // StartCoroutine(WaitSomeTime(1.0f));
            // attackBox.SetActive(false);
        }

        if(Input.GetButtonUp("Fire1"))
        {
            attackBox.SetActive(false);
            playerAnimator.SetBool("isSwingingSword", false);
        }

    }

    public void Respawn()
    {
        health = 3f;
        UpdateUI();
        this.gameObject.transform.position = respawnPosition;
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

    IEnumerator WaitSomeTime(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
    }

}
