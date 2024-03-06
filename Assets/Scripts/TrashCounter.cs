using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
    public override void Update()
    {
        base.Update();
        canPut = true;
        if(foodInCounter!= null)
        {
            Destroy(foodInCounter.gameObject);
        }
        if(plateInCounter != null)
        {
            Destroy(plateInCounter.gameObject);
        }
    }
}
