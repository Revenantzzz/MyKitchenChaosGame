using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MyKitchenChaos
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }
        [SerializeField] int maxDeliveryNum;
        [SerializeField] int maxDishInMenu;
        public int maxDishNum { get { return maxDishInMenu; } private set { } }
        [SerializeField] List<RecipeSO> InputRecipes;
        [SerializeField] float spawnRecipeTime;
        public List<RecipeSO> RecipeMenu =  new List<RecipeSO>();

        private int deliveredNum = 0;
        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }           
        }
        private void Start()
        {
            GenerateMenu();
        }

        //Create a menu include recipe
        private void GenerateMenu()
        {
            RecipeMenu.Capacity = maxDishInMenu;
            if (RecipeMenu.Count <= 0 )
            {
                StartCoroutine(AddRecipeToMenu());
            }           
        }
        //Add random recipe in recipe menu
        IEnumerator AddRecipeToMenu()
        {
            for (int i = RecipeMenu.Count; i < maxDishInMenu; i++)               
            {
                if (deliveredNum < maxDeliveryNum)
                {
                    deliveredNum++;
                    yield return new WaitForSeconds(spawnRecipeTime);                   
                    RecipeSO recipe = InputRecipes[Random.Range(0, InputRecipes.Count)];
                    UIDishOrdered.instance.SetUIRecipe(recipe);
                    RecipeMenu.Add(recipe);                    
                    
                }
            }           
        }
        //Compare new dish with InputRecipes in menu
        public bool CheckRecipe(Dish dish)
        {
            foreach (RecipeSO recipe in RecipeMenu.ToList<RecipeSO>())
            {
                if (CompareLists(recipe.foodsInRecipe, dish.foodSOList))
                {
                    UIDishOrdered.instance.CompleteRecipe(null, recipe); 
                    RecipeMenu.Remove(recipe);
                    StartCoroutine(AddRecipeToMenu());
                    return true;
                }
            }
            return false;
        }
        //Comapare 2 list method
        private bool CompareLists(List<FoodSO> list1, List<FoodSO> list2)
        {
            var firstNotSecond = list1.Except(list2).ToList();
            var secondNotFirst = list2.Except(list1).ToList();
            return !firstNotSecond.Any() && !secondNotFirst.Any();
        }
        public void RemoveRecipe(RecipeSO recipe)
        {
            if(RecipeMenu.Count > 0)
            {
                RecipeMenu.Remove(recipe);
                StartCoroutine(AddRecipeToMenu());
            }          
        }
    }
}
