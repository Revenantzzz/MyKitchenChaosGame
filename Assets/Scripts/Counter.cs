using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private GameObject _player;
    private PlayerManager _playerManager;

    private bool _picking = false;
    private bool _putting = false;

    private GameObject _plateInCounter; 
    public GameObject plateInCounter
    {
        get { return _plateInCounter; }
        set { _plateInCounter = value; }
    }

    private bool _canPick = true;
    public bool canPick
    {
        get { return _canPick; }
        set 
        { 
            _canPick = value; 
        }
    }
    private bool _canPut = true;
    public bool canPut
    {
        get { return _canPut; }
        set 
        { 
            _canPut = value;
            if(FoodInCounter() != null)
            {
                _canPut = false;
            }
        }
    }
    private bool _interacting = false;
    public bool interacting
    {
        get
        {
            return _interacting;
        }
        set
        {
            _interacting = value;
        }
    }
    private bool _isPicking = false;
    public bool isPicking
    {
        get { return _isPicking; }
        set { _isPicking = value; }
    }
    private bool _isPuting = false;
    public bool isPuting
    {
        get { return _isPuting; }
        set { _isPuting = value; }
    }
    public virtual void Start()
    {
        canPick = true;
        canPut = false;
        interacting = false;
    }
    public virtual void Update()
    {
        if (_player != null)
        {
            CheckCanPut();
            _playerManager = _player.GetComponent<PlayerManager>();
            if (FoodInCounter() != null && _playerManager.isPicking && canPick)
            {
                Pick();
            }
            if (plateInCounter != null && _playerManager.isPicking && canPick)
            {
                Pick();
            }
            if (FoodInHand() != null && _playerManager.isPutting && canPut)
            {
                Put();
            }
            if (_playerManager.isInteracting)
            {
                interacting = true;
            }
            else
            {
                interacting = false;
            }
        }
    }
    private void Pick()
    {
        isPicking = true;
        if(FoodInCounter() != null)
        {
            FoodInCounter().gameObject.transform.parent = _player.transform;
        }
        if (plateInCounter != null)
        {
            plateInCounter.gameObject.transform.parent = _player.transform;
        }
    }
    private void Put()
    {
        isPuting = true;
        if(plateInCounter == null)
        {
            FoodInHand().gameObject.transform.parent = transform;
        }
        else
        {
            FoodInHand().gameObject.transform.parent = plateInCounter.transform;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            _player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _player = null;
    }
    public virtual GameObject FoodInCounter()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 9)
            {
                plateInCounter = child.gameObject;
            }
            else
            {
                if (child.gameObject.layer >= 7 && child.gameObject.layer <= 8)
                {
                    isPuting = false;
                    return child.gameObject;
                }
            }       
        }
        return null;
    }
    public virtual GameObject FoodInHand()
    {
        if (_player != null)
        {
            foreach (Transform child in _player.transform)
            {
                if (child.gameObject.layer >= 7 && child.gameObject.layer <= 9)
                {
                    isPicking = false;
                    return child.gameObject;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    public virtual void CheckCanPut()
    {
        if (FoodInHand() != null)
        {
            if(plateInCounter != null)
            {
                if(FoodInHand().gameObject.layer >= 7 && FoodInHand().gameObject.layer <= 8)
                {
                    canPut = true;
                }
            }
            else
            {
                if (FoodInHand().gameObject.layer == 7 || FoodInHand().gameObject.layer == 9)
                {
                    canPut = true;
                }
            }
        }
    }
}
