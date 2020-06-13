using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField]
    float randomTimeMin, randomTimeMax;

    [SerializeField]
    AudioClip music;

    AudioSource audioSource;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentTime = 0f;
        audioSource.PlayOneShot(music);
    }

    // Update is called once per frame
    void Update()
    {
        PlayMusic();
    }

    void PlayMusic() {
        float playDelay = Random.Range(randomTimeMin, randomTimeMax);
        if (currentTime >= playDelay) {
            audioSource.PlayOneShot(music);
            currentTime = 0;
        } else {
            currentTime += Time.deltaTime;
        }
    }
}
