using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class FoodInKitchenware : MonoBehaviour
    {
        [SerializeField] FoodSO foodSO;

        private void Awake()
        {
            Hide();
        }
        public FoodSO GetFoodSO()
        {
            return foodSO;  
        }
        public void SetFoodActive(bool isActive)
        {
            if (isActive)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
        private void Hide()
        {
            this.transform.gameObject.SetActive(false); 
        }
        private void Show()
        {
            this.transform.gameObject.SetActive(true);
        }
    }
}
