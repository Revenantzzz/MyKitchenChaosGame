using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyKitchenChaos
{
    public class DeliveryManager : MonoBehaviour
    {
        [SerializeField] int maxDishInMenu;
        [SerializeField] List<RecipeSO> Recipes;
        List<RecipeSO> RecipeMenu = new List<RecipeSO>();
        

        private void Awake()
        {
            GenerateMenu();
        }
        private void GenerateMenu()
        {
            if(RecipeMenu.Count < maxDishInMenu)
            {
                for(int i = RecipeMenu.Count; i <= maxDishInMenu; i++)
                {
                    RecipeSO recipe = Recipes[Random.Range(0, Recipes.Count)];
                    RecipeMenu.Add(recipe);
                    Debug.Log(recipe);
                }                
            }
        }
        public bool CheckRecipe(Dish dish)
        {
            foreach (RecipeSO recipe in RecipeMenu)
            {
                if (CompareLists(recipe.foodInRecipe, dish.foodSOList))
                {
                    RecipeMenu.Remove(recipe);
                    GenerateMenu();
                    return true;
                }
            }
            return false;
        }
        private bool CompareLists(List<FoodSO> list1, List<FoodSO> list2)
        {
            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();
            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
    }
}
