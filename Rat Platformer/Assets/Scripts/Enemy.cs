using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction; // Removed [SerializeField] since it's set automatically
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private LayerMask rayCastLayerMask;
    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength = .6f;
    private Vector3 initialScale;
    
    //Enemy Health
    public int health = 3;
    private int maxHealth = 3;

    //Attack Box Reference
    [SerializeField] private GameObject playerHitbox; 

    void Start()
    {
        initialScale = transform.localScale;
        direction = Mathf.RoundToInt(transform.localScale.x / Mathf.Abs(transform.localScale.x)); // Set initial direction
    }

    void Update()
    {

        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for Right Ledge
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if(rightLedgeRaycastHit.collider == null)
        {
            direction = -1;
        } 

        //Check for Left Ledge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.red);
        if(leftLedgeRaycastHit.collider == null)
        {
            direction = 1;
        } 

        //Check for Right Wall
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * rayCastLength, Color.yellow);
        if(rightWallRaycastHit.collider != null)
        {
            direction = -1;
        } 

        //Check for Left Wall
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * rayCastLength, Color.magenta);
        if(leftWallRaycastHit.collider != null)
        {
            direction = 1;
        } 

        //Flip sprite based on direction
        transform.localScale = new Vector2(direction * initialScale.x, initialScale.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject == NewPlayer.Instance.gameObject && NewPlayer.Instance.health > 0)
        {
            Debug.Log("yipes!");
            NewPlayer.Instance.health -= 1.0f;
            NewPlayer.Instance.UpdateUI();
        }
    }
       
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject == playerHitbox.gameObject && health > 0)
        {
            health -= 1;
            Debug.Log("YOU SON OF A !");
        }
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (health == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("enemy died");
        //Die animation

        //Destroy 
        Destroy(gameObject);
    }
}