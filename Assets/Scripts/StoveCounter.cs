using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : Counter
{
    public GameObject _unCookedMeat;
    public GameObject _cookedMeat;
    public GameObject _overCookedMeat;

    private float _turnOnStoveTimer = 0;
    private float _turnOnStoveTime = .5f;
    private float _cookingTime = 8f;
    private float _doneTime = 5f;
    private float _cookTimer = 0;
    private bool _hasMeat = false;
    private bool _isBurning = false;
    private bool _done = false;
    private bool _burned = false;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if(FoodInHand() != null)
        {
            if (FoodInHand().gameObject.name.Contains(_unCookedMeat.name))
            {
                canPut = true;
            }
            else
            {
                canPut = false;
            }
        }
        else
        {
            canPut = false;
        }
        if(FoodInCounter()!= null)
        {
            _hasMeat=true;
        }
        if (isPicking)
        {
            _hasMeat = false;
        }
        {
            
        }
        if (interacting &&_turnOnStoveTimer >= _turnOnStoveTime)
        {
            _isBurning = !_isBurning;
            _turnOnStoveTimer = 0f;
        }
        else
        {
            _turnOnStoveTimer += Time.deltaTime;
            if( _turnOnStoveTimer >= 1 )
            {
                _turnOnStoveTimer = 1f;
            }
        }
        Cook();
    }
    private void Cook()
    {
        if (!_isBurning || !_hasMeat)
        {
            _cookTimer = 0;
            _done = false;
            _burned = false;
        }
        if (_isBurning && FoodInCounter() != null)
        {
            _cookTimer += Time.deltaTime;
            if (_cookTimer > _doneTime && !_done )
            {
                _done = true;
                Destroy(FoodInCounter().gameObject);
                Instantiate(_cookedMeat, transform);
            }
            if(_cookTimer > _cookingTime && !_burned)
            {
                _burned = true;
                Destroy(FoodInCounter().gameObject);
                Instantiate(_overCookedMeat, transform);
                _cookTimer = _cookingTime;
            }
        }
    }
}
