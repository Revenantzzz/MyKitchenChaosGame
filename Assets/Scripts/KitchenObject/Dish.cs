using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace MyKitchenChaos
{
    public class Dish : Kitchenware
    {
        private void Update()
        {
            SetLocateOnFloor();
        }
    }
}
