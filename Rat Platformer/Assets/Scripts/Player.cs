using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] private bool dead;
    [SerializeField] private int health = 100;
    [SerializeField] private string playerName = "Billy";
    [SerializeField] private float recoveryCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speedMultiplier * Time.deltaTime;
    
        recoveryCounter += Time.deltaTime;

        CheckBoundaries();

    }
    void CheckBoundaries() 
    {
        if (transform.position.x > 10) {
            transform.position = new Vector3(10.0f, transform.position.y, transform.position.z);
            Hurt();
        }
        if (transform.position.x < -10) {
            transform.position = new Vector3(-10.0f, transform.position.y, transform.position.z);
            Hurt();
        }
        if (transform.position.y > 4.5) {
            transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
            Hurt();
        }
        if (transform.position.y < -4.5) {
            transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
            Hurt();
        }
    }

    void Hurt() 
    {
        if (recoveryCounter > 2) 
        {
            health -= 1;
            recoveryCounter = 0;
        }
    }
}
