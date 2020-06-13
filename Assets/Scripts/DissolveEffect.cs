//using Packages.Rider.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    public bool bStartDissolved = false;
    public bool bIsDissolving;

    [SerializeField]
    private Material dissolveMaterial;
    [SerializeField]
    private float dissolveSpeed = 1f;
    [ColorUsageAttribute(true, true)]
    [SerializeField]
    private Color startDissolveColor;
    [ColorUsageAttribute(true, true)]
    [SerializeField]
    private Color stopDissolveColor;

    float extinctionDelay = 6.8f;

    private Animator animator;
    private Enemy enemy;
    private float dissolveAmount;

    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponentInParent<Enemy>();

        if(bStartDissolved == true)
        {
            //Enemy.OnDeath -= StartDissolve;
            //dissolveMaterial.SetFloat("_DissolveAmount", 1);
            dissolveAmount = 1;
            bIsDissolving = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsDissolving)
        {
            //dissolveAmount = Mathf.Clamp(dissolveAmount + dissolveSpeed * Time.deltaTime,-0.3f, 1.3f);
            //dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
            StartDissolve();
        }
        else
        {
            StopDissolve();
            //dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            //dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bIsDissolving = !bIsDissolving;
        }

        if(Time.time >= extinctionDelay)
        {
            bIsDissolving = true;
        }


    }

    public void StartDissolve()
    {
        if (this.gameObject.CompareTag("Enemy") && bStartDissolved == false)
        {
            //if (this.gameObject == enemyObject)
            //{
                bIsDissolving = true;
                //Enemy.OnDeath -= StartDissolve;
            //}

        }
        dissolveAmount = Mathf.Clamp(dissolveAmount + dissolveSpeed * Time.deltaTime, 0.3f, 1.3f);
        dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        //this.dissolveSpeed = dissolveSpeed;
        dissolveMaterial.SetColor("_DissolveColor", startDissolveColor);
    }

    public void StopDissolve()
    {
        //bIsDissolving = false;
        dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
        dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);
        //this.dissolveSpeed = dissolveSpeed;
        dissolveMaterial.SetColor("_DissolveColor", stopDissolveColor);
    }

    private void OnEnable()
    {
        //Enemy.OnDeath += StartDissolve;
    }

    private void OnDisable()
    {
        //Enemy.OnDeath -= StartDissolve;
    }


}
