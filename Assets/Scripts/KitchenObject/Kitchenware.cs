using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace MyKitchenChaos
{
    public class Kitchenware : KitchenObject
    {
        [SerializeField] KitchenwareFoodListSO foodListSO;
        private List<FoodSO> canContainFoodSO => foodListSO.ContainFoodList;
        private Transform foodInKitchenwareParent => foodListSO.foodInKitchenwareParent;
        private Transform foodParent;

        private List<FoodInKitchenware> foodList = new List<FoodInKitchenware>();
        public List<FoodSO> foodSOList = new List<FoodSO>();

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            foodParent = Instantiate(foodInKitchenwareParent,transform);
            foreach(Transform child in foodParent)
            {   
                if(child.TryGetComponent<FoodInKitchenware>(out FoodInKitchenware food))
                {  
                    foodList.Add(food as FoodInKitchenware);
                }
            }
        }
        private bool CheckFood(Food food)
        {
            if(food != null)
            {
                foreach (FoodSO foodSO in canContainFoodSO)
                {
                    if (food.GetMainFoodSO() == foodSO)
                    {
                        return true;
                    }
                }
            }           
            return false;
        }
        public bool SetFood(Food food)
        {
            if(CheckFood(food))
            {
                foreach (FoodInKitchenware foodInKitchenware in foodList)
                {
                    if (food.GetMainFoodSO() == foodInKitchenware.GetFoodSO())
                    {
                        foodSOList.Add(foodInKitchenware.GetFoodSO());
                        foodInKitchenware.SetFoodActive(true);
                        food.DisableKitchenObject();
                        return true;
                    }
                }
            }
            return false;
        }
        public void ResetFoodSOList()
        {
            foodSOList.Clear();
            foodSOList = new List<FoodSO>();
        }
    }
}
