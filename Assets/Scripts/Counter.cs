using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private GameObject player;
    private PlayerManager playerManager;

    private GameObject _plateInCounter;
    public GameObject plateInCounter
    {
        get { return _plateInCounter; }
        set { _plateInCounter = value; }
    }
    private GameObject _foodInCounter;
    public GameObject foodInCounter
    {
        get { return FoodInCounter(); }
    }
    private GameObject _foodInHand;
    public GameObject foodInHand
    {
        get { return FoodInHand(); }
    }
    private bool _canPick = true;
    public bool canPick
    {
        get { return _canPick; }
        set
        {
            canPick = value;
        }
    }
    private bool _canPut = true;
    public bool canPut
    {
        get { return _canPut; }
        set
        {
            canPut = value;
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
            interacting = value;
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
        if (player != null)
        {  
            playerManager = player.GetComponent<PlayerManager>();
            if (FoodInCounter() != null && playerManager.isPicking && canPick)
            {
                Pick();
            }
            CheckCanPut();
            if (FoodInHand() != null && playerManager.isPutting && canPut)
            {
                Put();
            }
            if (playerManager.isInteracting)
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
        if (FoodInCounter() != null)
        {
            FoodInCounter().gameObject.transform.parent = player.transform;
        }
        if (plateInCounter != null)
        {
            plateInCounter.gameObject.transform.parent = player.transform;
        }
    }
    private void Put()
    {
        isPuting = true;
        if (plateInCounter == null)
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
            player = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        player = null;
    }
    private GameObject FoodInCounter()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 9)
            {
                plateInCounter = child.gameObject;
                isPuting = false;
                return child.gameObject;
            }
            else
            {
                if (child.gameObject.layer >= 7 && child.gameObject.layer <= 8)
                {
                    isPuting = false;
                    plateInCounter = null;
                    return child.gameObject;
                }
            }
        }
        plateInCounter = null;
        return null;
    }
    private GameObject FoodInHand()
    {
        if (player != null)
        {
            foreach (Transform child in player.transform)
            {
                if (child.gameObject.layer >= 7 && child.gameObject.layer <= 9)
                {
                    isPicking = false;
                    return child.gameObject;
                }
            }
            return null;
        }
        return null;
    }
    public virtual void CheckCanPut()
    {
        if (FoodInHand() != null)
        {
            if (plateInCounter != null)
            {
                if (FoodInHand().gameObject.layer == 8)
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
                if (FoodInHand().gameObject.layer == 7 || FoodInHand().gameObject.layer == 9)
                {
                    canPut = true;
                }
                else
                {
                    canPut = false;
                }
            }
        }
    }
}
