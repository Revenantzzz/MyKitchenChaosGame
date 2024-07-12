using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyKitchenChaos
{
    public class UIDishOrdered : MonoBehaviour
    {
        public static UIDishOrdered instance { get; private set; }

        [SerializeField] UIRecipe uiRecipe;

        List<UIRecipe> uiRecipeList = new List<UIRecipe>();
        private int maxRecipeNum => DeliveryManager.Instance.maxDishNum;

        private void Awake()
        {
            instance = this;
            SpawnUIRecipe();
        }
        private void SpawnUIRecipe()
        {
            for (int i = 0; i < maxRecipeNum; i++)
            {
                UIRecipe recipe = Instantiate(uiRecipe, this.transform);
                uiRecipeList.Add(recipe);
                recipe.gameObject.SetActive(false);
            }
        }
        //Set UIReicpe match recipeSO
        public  void SetUIRecipe(RecipeSO recipeSO)
        {
            if(recipeSO != null)
            {
                foreach (UIRecipe uIRecipe in uiRecipeList.ToList<UIRecipe>())
                {
                    if (!uIRecipe.gameObject.activeSelf)
                    {
                        uIRecipe.gameObject.SetActive(true);
                        uIRecipe.SetFoodIcon(recipeSO);
                        break;
                    }
                }
            }            
        }
        //Disable uiRecipe when complete or timeout
        public void CompleteRecipe(UIRecipe uiRecipe, RecipeSO recipeSO)
        {    
            if(uiRecipe == null)
            {
                uiRecipe = this.uiRecipeList[DeliveryManager.Instance.RecipeMenu.IndexOf(recipeSO)];
            }
            if (uiRecipe == null) return;
            uiRecipe.gameObject.SetActive(false);
            uiRecipeList.Remove(uiRecipe);
            uiRecipeList.Add(uiRecipe);
            uiRecipe.transform.SetParent(null);            
            uiRecipe.transform.SetParent(this.transform);
            DeliveryManager.Instance.RemoveRecipe(recipeSO);
        }
    }
}
