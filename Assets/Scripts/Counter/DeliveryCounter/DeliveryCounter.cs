using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class DeliveryCounter : Counter
    {
        public event UnityAction<bool> Done;
        private void Update()
        {
            ResetFood();
        }
        public override bool SetKitchenObject(KitchenObject kitchenObject)
        {            
            if (!CheckDish(kitchenObject))
            {
                Done?.Invoke(false);
                return false;
            }
            Done?.Invoke(true);
            return base.SetKitchenObject(kitchenObject);
        }
        // Check if kitchenobject is dish
        private bool CheckDish(KitchenObject kitchenObject)
        {
            if (kitchenObject is Dish dish)
            {              
                return DeliveryManager.Instance.CheckRecipe(dish);
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
