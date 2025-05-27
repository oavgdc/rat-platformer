using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{

    public AudioClip fallSoundAudioClip;
    public AudioClip landOnBookAudioClip;
    public AudioSource audioSource;
    public float transitionTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySoundEffects()); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator PlaySoundEffects()
    {
        audioSource.Play();
        yield return new WaitForSeconds(transitionTime);
        audioSource.clip = landOnBookAudioClip;
        audioSource.Play();
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(2);

    }
}
