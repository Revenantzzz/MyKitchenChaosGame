using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class TrashCounter : Counter
    {
        public event UnityAction<bool> Trash;
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
                Trash?.Invoke(true);
                kitchenObject.DisableKitchenObject();
                ResetKitchenObject();                           
            }
        }
    }
}
