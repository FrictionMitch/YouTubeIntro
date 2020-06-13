using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogo : MonoBehaviour
{
    public bool bCanAppear = false;

    [SerializeField]
    private Material dissolveMaterial;
    [SerializeField]
    private float dissolveSpeed = .5f;
    [ColorUsageAttribute(true, true)]
    [SerializeField]
    private Color startDissolveColor;
    [ColorUsageAttribute(true, true)]
    [SerializeField]
    private Color stopDissolveColor;
    [SerializeField]
    float entryDelay = 8f;
    [SerializeField]
    float animationDelayOffset;
    [SerializeField]
    private float dissolveAmount = 0.5f;
    [SerializeField]
    GameObject reticle;

    private Animator animator;
    private bool bCanAnimate;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bCanAnimate = true;
        dissolveAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            bCanAppear = !bCanAppear;
        }

        if(Time.time >= entryDelay)
        {
            bCanAppear = true;
            StartCoroutine(AnimatorRoutine());
        }

        if (bCanAppear)
        {
            ReverseDissolve();
        } 
        else
        {
            StartDissolve();
        }

    }

    public void StartDissolve()
    {
        //bIsDissolving = true;
        dissolveAmount = Mathf.Clamp(dissolveAmount + dissolveSpeed * Time.deltaTime, 0.3f, 1.3f);
        dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        dissolveMaterial.SetColor("_DissolveColor", startDissolveColor);
    }

    public void ReverseDissolve()
    {
        //bIsDissolving = false;
        dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
        dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        dissolveMaterial.SetColor("_DissolveColor", stopDissolveColor);
    }

    IEnumerator AnimatorRoutine()
    {
        if (bCanAnimate)
        {
            if(reticle != null)
            {
                Destroy(reticle);
            }
            yield return new WaitForSeconds(animationDelayOffset);
            animator.SetTrigger("FlexTrigger");
            bCanAnimate = false;
        }
    }
}
