using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Update()
    {
        base.Update();
        canPut = true;
        if(FoodInCounter()!= null)
        {
            Destroy(FoodInCounter().gameObject);
        }
        if(plateInCounter != null)
        {
            Destroy(plateInCounter.gameObject);
        }
    }
    public override GameObject FoodInCounter()
    {
        return base.FoodInCounter();
    }
    public override GameObject FoodInHand()
    {
        return base.FoodInHand();
    }
}
