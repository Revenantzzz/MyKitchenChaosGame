using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Counter
{
    public GameObject plate;
    private bool hasPlate = false;
    private float spawnDelay = 1f;
    private float timer = 0f;
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (foodInCounter == null && !hasPlate) 
        {
            SpawnPlate();
            hasPlate = true;
        }
        if(hasPlate)
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay)
            {
                hasPlate = false;
                timer = 0f;
            }
        }
        if(foodInCounter != null)
        {
            hasPlate = true;
        }
    }
    private void SpawnPlate()
    {
        Instantiate(plate, transform);
    }
}
