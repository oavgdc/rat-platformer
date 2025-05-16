using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : PhysicsObject
{
    //Visual (Animation & Sprite)
    public Animator playerAnimator;
    public SpriteRenderer playerSprite;

    //Movement
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;
    private Vector3 initialScale;

    //Audio
    public AudioSource footstepAudioSource;
    public AudioSource playerAudioSource;
    public AudioClip[] audioClips;

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
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
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

    // Start is called before the first frame update
    void Start()
    {
        // Initialize initialScale in Start to ensure Instance is set
        initialScale = transform.localScale;

        health = 3f;
        UpdateUI();

        //attackBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer -= Time.deltaTime;

        //Movement

        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0);
        if(Mathf.Abs(targetVelocity.x) > 0.5)
        {
            playerAnimator.SetBool("isSwordWalking", true); 
        } else 
        {
            playerAnimator.SetBool("isSwordWalking", false); 
        }

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
            transform.localScale = new Vector3(-1 * initialScale.x, initialScale.y, initialScale.z);
        } 
        else if (targetVelocity.x > 0.1)
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
        
        //Footstep Sounds
        if((Mathf.Abs(targetVelocity.x) > 0.1) /* && grounded */)
        {
            footstepAudioSource.enabled = true;
            footstepAudioSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
        } else
        {
            footstepAudioSource.enabled = false;
        }

        //Attacking
        if(Input.GetButtonDown("Fire1"))
        {
            if(attackTimer > 0)
            {
                return;
            }

            attackTimer = cooldown;
            PlaySound(1);
            playerAnimator.SetTrigger("SwordAttack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit" + enemy.name);
                PlaySound(0);
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }

    }

    //audio clips
    public void PlaySound(int soundKey) 
    {
        
        if (playerAudioSource != null) 
        {
            playerAudioSource.PlayOneShot(audioClips[soundKey]);
            playerAudioSource.pitch = UnityEngine.Random.Range(1.0f, 1.5f);
        } else 
        {
            Debug.LogWarning("Sound not found: " + soundKey);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

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