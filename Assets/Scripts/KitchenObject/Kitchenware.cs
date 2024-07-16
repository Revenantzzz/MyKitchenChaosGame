using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace MyKitchenChaos
{
    public class Kitchenware : KitchenObject
    {
        [SerializeField] KitchenwareFoodListSO foodListSO;
        private List<FoodSO> CanContainFoodSO => foodListSO.ContainFoodList;
        private Transform FoodInKitchenwareParent => foodListSO.foodInKitchenwareParent;
        private Transform foodParent;

        private List<FoodInKitchenware> foodList = new List<FoodInKitchenware>();
        public List<FoodSO> foodSOList = new List<FoodSO>();

        private void Start()
        {
            Initialize();
        }
        private void Initialize()
        {
            foodParent = Instantiate(FoodInKitchenwareParent,transform);
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
                foreach (FoodSO foodSO in CanContainFoodSO)
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
        public override void DisableKitchenObject()
        {
            base.DisableKitchenObject();
            foreach(FoodInKitchenware food in foodList)
            {
                food.gameObject.SetActive(false);
            }
        }
    }
}
