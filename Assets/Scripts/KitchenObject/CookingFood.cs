using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class CookingFood : Food
    {

        private bool _isBurned;
        public bool IsBurned
        {
            get { return _isBurned; }
            set { _isBurned = value; }
        }
        [SerializeField] FoodSO cookedFoodSO;
        [SerializeField] FoodSO burnedFoodSO;
        [SerializeField] float timeToCook;
        [SerializeField] float timeToBurn;
        public float cookingTime => timeToCook;
        public float burningTime => timeToBurn;
        private GameObject cookedFood;
        private GameObject burnedFood;

        protected override void Initialize()
        {
            base.Initialize();
            cookedFood = Instantiate(cookedFoodSO.Prefab, this.transform);
            burnedFood = Instantiate(burnedFoodSO.Prefab, this.transform);        
            cookedFood.SetActive(false);
            burnedFood.SetActive(false);  
            IsBurned = false;        
        }
        public void Done()
        {
            rawFood .SetActive(false);
            cookedFood.SetActive(true);
            burnedFood.SetActive(false);
            IsRaw = false;
            mainFoodSO = cookedFoodSO;
        }
        public void Burned()
        {
            rawFood.SetActive(false);
            cookedFood.SetActive(false);
            burnedFood.SetActive(true);
            IsRaw = false;
            IsBurned = true;
            mainFoodSO = burnedFoodSO;
        }
        public override void Raw()
        {
            base.Raw();
            cookedFood.SetActive(false);
            burnedFood.SetActive(false);
            IsBurned = false;          
        }
       
    }
}
