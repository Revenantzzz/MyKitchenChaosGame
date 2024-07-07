using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class StoveCounter : Counter
    {
        float doneTime;
        float burnTime;

        bool isFlameOn = false;
        public event UnityAction<bool> FlameOn;
        private void Awake()
        {
            locatePoint = new Vector3(0, 1.4f, 0);
        }
        private void Update()
        {
            Cooking();
        }
        public override void ResetKitchenObject()
        {
            base.ResetKitchenObject();
            isFlameOn = false;
            FlameOn?.Invoke(false);
        }
        private void Cooking()
        {
            if (HasKitchenObject && !isFlameOn)
            {
                if (kitchenObject is CookingFood food && food.IsRaw)
                {
                    isFlameOn = true;
                    FlameOn?.Invoke(true);
                    food = kitchenObject as CookingFood;
                    doneTime = food.cookingTime;
                    burnTime = food.burningTime;
                    StartCoroutine(CookingFood(food));
                }
            }
        }
        IEnumerator CookingFood(CookingFood food)
        {
            yield return new WaitForSeconds(doneTime);
            Debug.Log("Done");
            food.Done();
            StartCoroutine(BurningFood(food));
        }
        IEnumerator BurningFood(CookingFood food)
        {
            yield return new WaitForSeconds(burnTime);
            food.Burned();
        }
    }
}
