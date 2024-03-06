using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    private GameObject foodParent;
    private Vector3 inHandPosition = new Vector3(0, 1.4f, .85f);
    private Vector3 inCounterPosition = new Vector3(0, 1.3f, 0);
    private Vector3 inPlatePosition = new Vector3(0, .2f, 0);
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        foodParent = FoodParent();
        Position();
    }
    private void Position()
    {
        if (foodParent.layer == 3)
        {
            ChangePos(inHandPosition);
        }
        else
        {
            if (foodParent.layer == 6)
            {
                if (foodParent.GetComponent<StoveCounter>() != null)
                {
                    ChangePos(inCounterPosition + new Vector3(0, .2f, 0));
                }
                else
                {
                    if (foodParent.GetComponent<ContainerCounter>() != null || foodParent.GetComponent<DeliveryCounter>())
                    {
                        ChangePos(Vector3.zero);
                    }
                    else
                    {
                        ChangePos(inCounterPosition);
                    }
                }
            }
            else
            {
                if (foodParent.layer == 9)
                {
                    ChangePos(inPlatePosition);
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
