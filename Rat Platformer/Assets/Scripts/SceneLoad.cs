using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public GameObject trigger;
    public GameObject player;
    public Animator transition;
    public Vector2 whereToSpawnTo;
    public VectorValue playerStorage;

    public float transitionTime = 1.0f;

    public string sceneToLoadName;
    
    void Start()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player" /*&& !collider.isTrigger*/)
        {
            Debug.Log("Player is touching trigger!");
            StartCoroutine(LoadLevel());
        }
    }

    public void LoadGame()
    {
        StartCoroutine(LoadLevel());
    }

    public IEnumerator LoadLevel()
    {
        playerStorage.pos = whereToSpawnTo;
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoadName);

    }
}

