using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounter : Counter
{
    public GameObject _unCookedMeat;
    public GameObject _cookedMeat;
    public GameObject _overCookedMeat;

    private float _turnOnStoveTimer = 0;
    private float _turnOnStoveTime = .5f;
    private float _cookingTime = 40f;
    private float _doneTime = 15f;
    private float _burnedTime = 25f;
    private float _cookTimer = 0;

    private bool _hasMeat = false;
    private bool _isBurning = false;
    private bool _done = false;
    private bool _burned = false;

    public Slider slider;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        CheckIsMeat();
        SetValue();
        slider.gameObject.SetActive(_isBurning);
        Cook();
        if (_isBurning)
        {     
            slider.value = _cookTimer / _cookingTime;
        }
        Debug.Log(_hasMeat);
    }
    public void Cook()
    {
        if (!_isBurning || !_hasMeat)
        {
            _cookTimer = 0;
            _done = false;
            _burned = false;

        }
        if (_isBurning && foodInCounter != null)
        {
            _cookTimer += Time.deltaTime;
            if (_cookTimer > _doneTime && !_done )
            {
                _done = true;
                Instantiate(_cookedMeat, transform);
                Destroy(foodInCounter.gameObject);
                
            }
            if(_cookTimer > _burnedTime && !_burned)
            {
                _burned = true;
                Instantiate(_overCookedMeat, transform);
                Destroy(foodInCounter.gameObject);
                
            }
            if(_cookTimer >= _cookingTime)
            {
                _cookTimer = _cookingTime;
            }
        }
    }
    public float CookProgress()
    {
        return (_cookTimer/_burnedTime);
    }
    private void CheckIsMeat()
    {
        if (foodInHand != null)
        {
            if (foodInHand.gameObject.name.Contains(_unCookedMeat.name))
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
    }
    private void SetValue()
    {
        if (foodInCounter != null)
        {
            _hasMeat = true;
        }
        else
        {
            _hasMeat = false;
        }
        if (isPicking)
        {
            _hasMeat = false;
        }
        if (!_hasMeat)
        {
            _isBurning = false;
            _cookTimer = 0f;
        }
        if (interacting && _turnOnStoveTimer >= _turnOnStoveTime)
        {
            _isBurning = !_isBurning;
            _turnOnStoveTimer = 0f;
        }
        else
        {
            _turnOnStoveTimer += Time.deltaTime;
            if (_turnOnStoveTimer >= 1)
            {
                _turnOnStoveTimer = 1f;
            }
        }
    }
}
