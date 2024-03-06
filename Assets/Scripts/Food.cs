using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private GameObject _foodParent;
    private Vector3 _inHandPosition = new Vector3(0, 1.4f, .85f);
    private Vector3 _inCounterPosition = new Vector3(0, 1.3f, 0);
    private Vector3 _inPlatePosition = new Vector3(0, .2f, 0);
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        _foodParent = FoodParent();
        Position();
    }
    private void Position()
    {
        if (_foodParent.layer == 3)
        {
            ChangePos(_inHandPosition);
        }
        else
        {
            if (_foodParent.layer == 6)
            {
                if (_foodParent.GetComponent<StoveCounter>() != null)
                {
                    ChangePos(_inCounterPosition + new Vector3(0, .2f, 0));
                }
                else
                {
                    if (_foodParent.GetComponent<ContainerCounter>() != null || _foodParent.GetComponent<DeliveryCounter>())
                    {
                        ChangePos(Vector3.zero);
                    }
                    else
                    {
                        ChangePos(_inCounterPosition);
                    }
                }
            }
            else
            {
                if (_foodParent.layer == 9)
                {
                    ChangePos(_inPlatePosition);
                }
            }
        }
    }
    private void ChangePos(Vector3 pos)
    {
        if (transform.localPosition != pos)
        {
            transform.localPosition = pos;
        }
    }
    private GameObject FoodParent()
    {
        return transform.parent.gameObject;
    }
}
