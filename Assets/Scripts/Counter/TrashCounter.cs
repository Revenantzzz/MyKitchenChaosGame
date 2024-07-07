using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class TrashCounter : Counter
    {
        private void Start()
        {
            locatePoint = Vector3.zero;
            Initiallize();
        }
        private void Update()
        {
            TakeOutTrash();
        }
        private void TakeOutTrash()
        {
            if(HasKitchenObject)
            {
                kitchenObject.DisableKitchenObject();
                ResetKitchenObject();                           
            }
        }
    }
}
