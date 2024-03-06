using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : Counter
{
    public GameObject foodContain;
    private bool hasFood = false;
    private float timer = 0;
    private float spawnDelay = 1f;
    public override void Start()
    {
        base.Start();
        canPut = false;
        canPick = true;
    }
    public override void Update()
    {
        canPut = false;
        base.Update();
        if (foodInCounter == null && !hasFood)
        {
            SpawnFood();
            hasFood = true;
        }
        if (hasFood)
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay)
            {
                hasFood = false;
                timer = 0f;
            }
        }
        if (foodInCounter != null)
        {
            hasFood = true;
        }
    }
    private void SpawnFood()
    {
        Instantiate(foodContain, transform);
    }
}


