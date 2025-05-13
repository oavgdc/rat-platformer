using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    // public GameObject trigger;
    // public GameObject player;
    public Animator checkpointAnimator;
    public AudioSource checkpointAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("player is entering!");
        
        if (collider.name == "Player") 
        {
            Debug.Log("setting checkpoint!");

            if (checkpointAnimator.GetBool("isEnabled") != true)
            {
                checkpointAudioSource.Play();
            }

            checkpointAnimator.SetBool("isEnabled", true);
            collider.gameObject.GetComponent<NewPlayer>().respawnPosition = this.gameObject.transform.position;
        }
    }
}
