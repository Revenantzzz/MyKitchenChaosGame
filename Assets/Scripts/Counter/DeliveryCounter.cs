using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyKitchenChaos
{
    public class DeliveryCounter : Counter
    {
        private DeliveryManager deliveryManager;
        private void Awake()
        {
            deliveryManager = GetComponent<DeliveryManager>();
        }
        private void Update()
        {
            ResetFood();
        }
        public override bool SetKitchenObject(KitchenObject kitchenObject)
        {
            if (!CheckDish(kitchenObject))
            {
                return false;
            }
            return base.SetKitchenObject(kitchenObject);
        }
        // Check if kitchenobject is dish
        private bool CheckDish(KitchenObject kitchenObject)
        {
            if (kitchenObject is Dish dish)
            {              
                return deliveryManager.CheckRecipe(dish);
            }
            return false;
        }
        
        private void ResetFood()
        {
            if(HasKitchenware())
            {
                this.GetKitchenware().ResetFoodSOList();
            }
            if(HasKitchenObject)
            {
                kitchenObject.DisableKitchenObject();
            }
        }
    }
}
