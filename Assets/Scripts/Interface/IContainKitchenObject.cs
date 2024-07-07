using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public interface IContainKitchenObject 
    {
        public void ManageKitchenObject();
        public void PushIntoStack(GameObject food);
        public void SpawnKitchenObject(GameObject prefab, int maxNumber);
    }
}
