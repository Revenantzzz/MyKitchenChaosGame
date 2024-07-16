using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MyKitchenChaos
{
    public class UIRecipe : MonoBehaviour
    {
        [SerializeField] private Transform foodIconPanel;
        [SerializeField] private Image foodIcon;
        [SerializeField] private Slider progress;

        RecipeSO recipe;      
        List<Image> foodIconList = new List<Image>();
        int MaxRecipeNumber => DeliveryManager.Instance.MaxDishInMenu;
        float MaxProgressTime => DeliveryManager.Instance.MaxDeliveryWaitTime;
        float timer = 0;
        public event UnityAction<UIRecipe,RecipeSO> OnRecipeComplete;

        //Spawn icon prefab when enable
        private void OnEnable()
        {
            SpawnFoodIcon();
        }
        private void Start()
        {
            timer = MaxProgressTime;
        }
        private void Update()
        {
            Progressing();           
        }
        //Spawn food Icon prefab
        private void SpawnFoodIcon()
        {
            foodIconList.Clear();
            for(int i = 0; i < MaxRecipeNumber; i++)
            {
                Image image =  Instantiate(foodIcon, foodIconPanel);
                foodIconList.Add(image);
                image.gameObject.SetActive(false);
            }
        }

        // set Icon in FoodIcon prefab match with list of Food in recipeSO
        public void SetFoodIcon(RecipeSO recipeSO)
        {
            recipe = recipeSO;
            List<FoodSO> foodList = new List<FoodSO>();
            if(recipeSO != null)
            {
                foodList = recipeSO.foodsInRecipe;
            }
            for(int i = 0; i < foodIconList.Count; i++) 
            {
                foodIconList[i].gameObject.SetActive(false);
                if (i < foodList.Count)
                {
                    foodIconList[i].sprite = foodList[i].Icon;
                    foodIconList[i].gameObject.SetActive(true);
                }                
            }
        }
        //Calculate how long this UIrecipe enable
        //If timeout disable this and reset 
        private void Progressing()
        {
            if(recipe == null)
            {
                return;
            }
            if(timer <= 0)
            {                              
                OnRecipeComplete?.Invoke(this,recipe);
                recipe = null;
                SetFoodIcon(null);
                timer = MaxProgressTime;
                this.gameObject.SetActive(false);
                return;
            }
            timer -= Time.deltaTime;
            progress.value = timer/MaxProgressTime;
        }
        public void DisableUIRecipe()
        {
            this.gameObject.SetActive(false);
            SetFoodIcon(null );
            timer = MaxProgressTime;
        }
    }
}
