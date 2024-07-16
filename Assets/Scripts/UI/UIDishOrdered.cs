using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class UIDishOrdered : MonoBehaviour
    {
        public static UIDishOrdered Instance { get; private set; }

        [SerializeField] UIRecipe uiRecipe;

        List<UIRecipe> uiRecipeList = new List<UIRecipe>();
        public event UnityAction<RecipeSO> RemoveRecipe;
        private int MaxRecipeNum => DeliveryManager.Instance.MaxDishInMenu;
        private List<RecipeSO> RecipesInMenu => DeliveryManager.Instance.RecipeMenu;

        public event UnityAction<int> OnChangeScore;
        private void Awake()
        {
            Instance = this;
            SpawnUIRecipe();
        }
        private void Start()
        {           
            DeliveryManager.Instance.OnCompleteRecipe += RecipeComplete;
        }

        private void SpawnUIRecipe()
        {
            for (int i = 0; i < MaxRecipeNum; i++)
            {
                UIRecipe recipe = Instantiate(uiRecipe, this.transform);
                uiRecipeList.Add(recipe);
                recipe.gameObject.SetActive(false);
                recipe.OnRecipeComplete += RecipeComplete;
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
        //Remove then add uiRecipe into uiRecipeList make it the last in list
        public void RecipeComplete(UIRecipe uiRecipe,RecipeSO recipeSO)
        {
            if(uiRecipe == null)
            {
                uiRecipe = this.uiRecipeList[RecipesInMenu.IndexOf(recipeSO)];
                OnChangeScore?.Invoke(recipeSO.Score);
            }            
            if (uiRecipe == null) return;
            OnChangeScore?.Invoke(-50);
            uiRecipe.DisableUIRecipe();
            uiRecipeList.Remove(uiRecipe);
            uiRecipeList.Add(uiRecipe);
            uiRecipe.transform.SetParent(null);            
            uiRecipe.transform.SetParent(this.transform);
            RemoveRecipe?.Invoke(recipeSO);
        }
    }
}
