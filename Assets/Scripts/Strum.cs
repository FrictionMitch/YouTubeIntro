using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strum : MonoBehaviour
{
    [SerializeField]
    AudioClip[] notes;
    [SerializeField]
    float soundDelay;
    [SerializeField]
    float strumDelay;

    Animator animator;
    AudioSource audioSource;
    int strumCount;
    float timeSinceLastSound;
    float timeSinceLastStrum;

    private void OnEnable()
    {
        Player.OnShoot += StrumAnimation;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timeSinceLastStrum = 0f;
        timeSinceLastSound = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //StrumAnimation();
    }

    void StrumAnimation() {
        //if (Input.GetAxisRaw("Right Stick Horizontal") != 0 || Input.GetAxisRaw("Right Stick Vertical") != 0) 
        //if(Input.GetMouseButton(0))
        {
            timeSinceLastStrum += Time.deltaTime;
            timeSinceLastSound += Time.deltaTime;
            if (timeSinceLastStrum >= strumDelay) {
                animator.SetTrigger("StrumTrigger");
                timeSinceLastStrum = 0f;
                if(timeSinceLastSound >= soundDelay) {
                    //StrumSound(); TODO: Enable After YouTube Intro
                    timeSinceLastSound = 0f;
                }
            }
        }
    }

    void StrumSound() {
        audioSource.PlayOneShot(notes[strumCount]);
        strumCount++;
        if (strumCount > notes.Length - 1) {
            strumCount = 0;
        }
    }

    private void OnDisable()
    {
        Player.OnShoot -= StrumAnimation;
    }
}
