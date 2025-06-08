using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction; // Removed [SerializeField] since it's set automatically

    //Ledge Detection 
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private LayerMask rayCastLayerMask; //The objects of whatever layers are selected for the layermask are the that Enemies will detect and bounce off of
    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength = .6f;
    private Vector3 initialScale;

    //Enemy Sprite
    public SpriteRenderer sprite;

    //Enemy Health
    public int health = 3;
    private int maxHealth = 3;

    void Start()
    {
        initialScale = transform.localScale;
        //direction = Mathf.RoundToInt(transform.localScale.x / Mathf.Abs(transform.localScale.x)); // Set initial direction
    }

    void Update()
    {

        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge!
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x + rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.blue);
        if (rightLedgeRaycastHit.collider == null) direction = -1;

        //Check for left ledge!
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down, rayCastLength);
        Debug.DrawRay(new Vector2(transform.position.x - rayCastOffset.x, transform.position.y + rayCastOffset.y), Vector2.down * rayCastLength, Color.green);
        if (leftLedgeRaycastHit.collider == null) direction = 1;

        //Check for right wall!
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.right, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.right * rayCastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall!
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y +rayCastOffset.y), Vector2.left, rayCastLength, rayCastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + rayCastOffset.y), Vector2.left * rayCastLength, Color.magenta);
        if (leftWallRaycastHit.collider != null) direction = 1;

        //Flip sprite based on direction
        transform.localScale = new Vector2(direction * initialScale.x, initialScale.y);
    }


    //Decreases health of Enemy by damage value passed in
    //Makes Enemy flash red as well
    public void TakeDamage(int damage) 
    {
        health -= damage;

        StartCoroutine(FlashRed(sprite)); 

        if (health == 0)
        {
            Die();
        }
    }

    public IEnumerator FlashRed(SpriteRenderer subject)
    {
        subject.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        subject.color = Color.white;
    }

    void Die()
    {
        Debug.Log("enemy died");
        //Die animation

        //Destroy 
        Destroy(gameObject);
    }
}