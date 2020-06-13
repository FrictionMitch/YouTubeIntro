using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fret : MonoBehaviour
{
    [SerializeField]
    float fretDelay;

    Animator animator;
    float timeSinceLastStrum;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        timeSinceLastStrum = 0f;
    }

    private void OnEnable()
    {
        Player.OnShoot += FretAnimation;
    }

    void FretAnimation()
    {
        timeSinceLastStrum += Time.deltaTime;
        if (timeSinceLastStrum >= fretDelay)
        {
            animator.SetTrigger("FretTrigger");
            timeSinceLastStrum = 0f;
        }
    }

    private void OnDisable()
    {
        Player.OnShoot -= FretAnimation;
    }
}
