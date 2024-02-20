using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : Counter
{
    public GameObject plate;
    private GameObject plateSpawned;

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        SpawnPlate();
    }
    private void SpawnPlate()
    {
        if(plateSpawned == null)
        {
            plateSpawned = Instantiate(plate, transform);
        }   
    }
}
