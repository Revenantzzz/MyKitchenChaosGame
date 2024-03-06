using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : Counter
{
    private Animator _animator;

    public GameObject[] _uncutFoodList;
    public GameObject[] _cutFoodList;
    private int _cutTime = 0;
    private int _doneTime = 10;
    private float _cuttingTimer = 0f;
    private int _indexInCutList = -1;
    private bool hasCut = false;

    public override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
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
            for (int i = 0; i < _uncutFoodList.Length; i++)
            {
                if (foodInHand.gameObject.name.Contains(_uncutFoodList[i].name))
                {
                    canPut = true;
                    _indexInCutList = i;
                    break;
                }
            }
        }
    }
    private void Cut()
    {
        if (interacting)
        {
            if (_cutTime >= _doneTime)
            {
                hasCut = true;
                Destroy(foodInCounter.gameObject);
                _cuttingTimer = 0f;
                _cutTime = 0;
                Instantiate(_cutFoodList[_indexInCutList], transform);
                canPick = true;
            }
            else
            {
                if(!hasCut)
                {
                    canPick = false;
                    _cuttingTimer += Time.deltaTime;
                    if (_cuttingTimer >= 0.2 * _cutTime)
                    {
                        _animator.SetTrigger("Cut");
                        _cutTime++;
                    }
                }     
            }
        }
    }
}
