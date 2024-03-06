using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : Counter
{
    private Animator animator;

    public GameObject[] uncutFoodList;
    public GameObject[] cutFoodList;
    private int cutTime = 0;
    private int doneTime = 10;
    private float cuttingTimer = 0f;
    private int indexInCutList = -1;
    private bool hasCut = false;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void Update()
    {
        CheckCanPut();
        base.Update(); 
        if(foodInCounter != null)
        {
            Cut();
        }  
        else
        {
            hasCut = false;
        }
    }
    public override void CheckCanPut()
    {
        if (foodInHand != null)
        {
            canPut = false;
            for (int i = 0; i < uncutFoodList.Length; i++)
            {
                if (foodInHand.gameObject.name.Contains(uncutFoodList[i].name))
                {
                    canPut = true;
                    indexInCutList = i;
                    break;
                }
            }
        }
    }
    private void Cut()
    {
        if (interacting)
        {
            if (cutTime >= doneTime)
            {
                hasCut = true;
                Destroy(foodInCounter.gameObject);
                cuttingTimer = 0f;
                cutTime = 0;
                Instantiate(cutFoodList[indexInCutList], transform);
                canPick = true;
            }
            else
            {
                if(!hasCut)
                {
                    canPick = false;
                    cuttingTimer += Time.deltaTime;
                    if (cuttingTimer >= 0.2 * cutTime)
                    {
                        animator.SetTrigger("Cut");
                        cutTime++;
                    }
                }     
            }
        }
    }
}
