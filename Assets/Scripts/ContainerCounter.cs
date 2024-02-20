using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : Counter
{
    public GameObject foodContain;
    private GameObject foodSpawned;
    public override void Start()
    {
        base.Start();
        canPut = false;
        canPick = true;
    }
    public override void Update()
    {
        base.Update();
        canPut = false;
        canPick = true ;
        SpawnFood();
    }
    private void SpawnFood()
    {
        if(foodContain != null && foodSpawned == null)
        {
            foodSpawned = Instantiate(foodContain, transform);
        }    
    }
}
    

