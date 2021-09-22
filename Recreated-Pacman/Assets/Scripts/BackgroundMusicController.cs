using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip intro_Music;
    [SerializeField] AudioClip ghost_Normal;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(StartIntroMusic());
        
    }
    
    void Update()
    {
        
    }
    
    IEnumerator StartIntroMusic() {
        audioSource.clip = intro_Music;
        audioSource.Play();
        yield return new WaitForSeconds((float)intro_Music.samples / intro_Music.frequency);
        StartCoroutine(StartGhostNormal());
    }

    IEnumerator StartGhostNormal() {
        audioSource.clip = ghost_Normal;
        audioSource.loop = true;
        audioSource.Play();
        yield return null;
    }
}
