using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class StoveCounter : Counter
    {
        [SerializeField] ProgressBar cookingProgressBar;
        [SerializeField] ProgressBar burningProgressBar;
        CookingFood cookingFood;
        float doneTime;
        float burnTime;
        float cookTimer = 0f;

        bool isFlameOn = false;
        bool isBurned = false;
        public event UnityAction<bool> FlameOn;
        private void Awake()
        {
            locatePoint = new Vector3(0, 1.4f, 0);
            isFlameOn = false;
        }
        private void Start()
        {
            cookingProgressBar.gameObject.SetActive(false);
            burningProgressBar.gameObject.SetActive(false);
        }
        private void Update()
        {
            StoveState();
        }
        // override SetKichenObject to check kitchenObjet is cooking food;
        public override bool SetKitchenObject(KitchenObject kitchenObject)
        {
            kitchenObject.transform.parent = this.transform;
            this.kitchenObject = kitchenObject;
            LocateKitchenObject();
            if (kitchenObject is CookingFood food && food.IsRaw)
            {
                isFlameOn = true;
                FlameOn?.Invoke(true);
                cookingFood = kitchenObject as CookingFood;
                doneTime = cookingFood.cookingTime;
                burnTime = cookingFood.burningTime;
            }
            return true;
        }
        //turn off flame 
        public override void ResetKitchenObject()
        {
            base.ResetKitchenObject();
            isFlameOn = false;
            FlameOn?.Invoke(false);
            isBurned = false;
            cookingProgressBar.gameObject.SetActive(false);
            burningProgressBar.gameObject.SetActive(false);
        }
        //if Stove is on start cooking
        private void StoveState()
        {
            if(isFlameOn && !isBurned)
            {
                Cooking();
            }
        }
        //Cooking timer
        private void Cooking()
        {
            cookingProgressBar.gameObject.SetActive(true);
            if (cookTimer >= doneTime)
            {
                cookingFood.Done();
                cookingProgressBar.gameObject.SetActive(false);
                Burning();
                return;
            }
            cookTimer += Time.deltaTime;
            cookingProgressBar.SetProgressValue(cookTimer/doneTime);
        }
        private void Burning()
        {
            burningProgressBar.gameObject.SetActive(true);
            if (cookTimer >= burnTime)
            {
                isBurned = true;
                cookTimer = 0;
                cookingFood.Burned();
                burningProgressBar.gameObject.SetActive(false);
                return;
            }
            cookTimer += Time.deltaTime;
            burningProgressBar.SetProgressValue(((cookTimer - doneTime)/(burnTime - doneTime)));
        }
    }
}
