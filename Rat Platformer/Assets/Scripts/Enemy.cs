using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    [SerializeField] private LayerMask rayCastLayerMask;
    [SerializeField] private Vector2 rayCastOffset;
    [SerializeField] private float rayCastLength = 2.0f;
    
    //Enemy Health
    private int health = 100;
    private int maxHealth = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

        //flip for facing directions
        

        if(targetVelocity.x < -0.1)
        {
            transform.localScale = new Vector2(-1, 1);
        } 
        else if (targetVelocity.x > 0.1)
        {
            transform.localScale = new Vector2(1, 1);
        }

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
}
