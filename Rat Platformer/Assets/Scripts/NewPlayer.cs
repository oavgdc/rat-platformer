using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : PhysicsObject
{

    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float jumpPower = 10;

    public int coinsCollected = 0;
    public Text coinsText;

    public float health = 3f;
    public Image[] hearts;
    [SerializeField] public Sprite heartFull;
    [SerializeField] public Sprite heartHalf;
    [SerializeField] public Sprite heartEmpty;

    

    // Start is called before the first frame update
    void Start()
    {
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
        coinsText.text = coinsCollected.ToString() + " Coins";
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

}
