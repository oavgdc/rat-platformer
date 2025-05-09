using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private Collider2D collider;
    private float waitTime = 0.5f; // Cooldown after falling through
    private float currentWaitTime;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
        collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Countdown the cooldown timer
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }

        // Check for down input (S key by default)
        if (Input.GetKeyDown(KeyCode.S) && currentWaitTime <= 0)
        {
            currentWaitTime = waitTime;
            StartCoroutine(DisableCollision());
        }
    }

    IEnumerator DisableCollision()
    {
        // Rotate the effector to allow falling through
        collider.enabled = false;
        yield return new WaitForSeconds(0.25f); // Short delay
        collider.enabled = true; // Reset
    }
}