using UnityEngine;

namespace MyKitchenChaos
{
    public class CuttingFood : Food
    {
        [SerializeField] FoodSO cutFoodSO;
        [SerializeField] int timesToCut;
        public int CuttingTime => timesToCut;
        private GameObject cuttedFood;       

        protected override void Initialize()
        {
            base.Initialize();
            cuttedFood = Instantiate(cutFoodSO.Prefab, this.transform);
            cuttedFood.SetActive(false);           
        }

        public void Cut()
        {
            rawFood.SetActive(false);
            cuttedFood.SetActive(true);
            IsRaw = false;
            mainFoodSO = cutFoodSO;
        }
        public override void Raw()
        {
            base.Raw();
            cuttedFood.SetActive(false );
        }
    }
}
