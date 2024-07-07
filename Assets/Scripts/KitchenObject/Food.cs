using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class Food : KitchenObject
    {
        private bool _isRaw;
        public bool IsRaw 
        { 
            get { return _isRaw; } 
            set { _isRaw = value; }
        } 
        [SerializeField] protected FoodSO foodSO;
        protected GameObject rawFood;
        protected FoodSO mainFoodSO;

        protected void Awake()
        {
            Initialize();
        }
        protected virtual void Initialize()
        {
            rawFood = Instantiate(foodSO.Prefab, this.transform);
            rawFood.SetActive(true);
            IsRaw = true;
            mainFoodSO = foodSO;
        }
        protected void Update()
        {
            SetLocateOnFloor();
        }
        public virtual void Raw()
        {
            rawFood.SetActive(true);
            IsRaw = true;
            mainFoodSO = foodSO;
        }
        public override void DisableKitchenObject()
        {
            this.Raw();
            base.DisableKitchenObject();
        }
        public FoodSO GetMainFoodSO()
        {
            return mainFoodSO;
        }
        public Sprite GetFoodIcon()
        {
            return foodSO.Icon;
        }
    }
}
