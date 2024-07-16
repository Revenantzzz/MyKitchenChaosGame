using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class DeliveryManager : MonoBehaviour
    {
        public static DeliveryManager Instance { get; private set; }
        [Header("Reference")]
        [SerializeField] List<RecipeSO> InputRecipes;
        public List<RecipeSO> RecipeMenu { get; private set; }

        [Header("Settings")]
        [SerializeField] private float spawnRecipeTime;
        [SerializeField] private int maxDishInMenu;
        public int MaxDishInMenu { get { return maxDishInMenu; } private set { } }
        [SerializeField] private int maxDeliveryNum;
        public int MaxDeliveryNum { get { return maxDeliveryNum; } private set { } } // max delivery num, if this = -1 mean no limit in delivery number
        [SerializeField] private float maxDeliveryWaitTime;
        public float MaxDeliveryWaitTime { get { return maxDeliveryWaitTime; } private set { } }

        public event UnityAction<UIRecipe, RecipeSO> OnCompleteRecipe;
        

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
            RecipeMenu = new List<RecipeSO>();
            MaxDeliveryNum = -1;
            deliveredNum = MaxDeliveryNum;
        }
        private void Start()
        {
            GenerateMenu();
            UIDishOrdered.Instance.RemoveRecipe += RemoveRecipe;
        }

        //Create a menu include recipe
        private void GenerateMenu()
        {
            RecipeMenu.Capacity = maxDishInMenu;
            AddRecipeToMenu();   
            StartCoroutine(GenerateDeliveryMenu());
        }
        //Add random recipe in recipe menu
        IEnumerator GenerateDeliveryMenu()
        {
            for (int i = RecipeMenu.Count; i < maxDishInMenu; i++)               
            {               
                yield return new WaitForSeconds(spawnRecipeTime);
                AddRecipeToMenu();
            }           
        }
        private void AddRecipeToMenu()
        {
            if (deliveredNum > 0 || deliveredNum <= -1)
            {
                deliveredNum--;
                RecipeSO recipe = InputRecipes[Random.Range(0, InputRecipes.Count)];
                RecipeMenu.Add(recipe);
                UIDishOrdered.Instance.SetUIRecipe(recipe);
            }
        }
        //Compare new dish with InputRecipes in menu
        public bool CheckRecipe(Dish dish)
        {
            foreach (RecipeSO recipe in RecipeMenu.ToList<RecipeSO>())
            {
                if (CompareLists(recipe.foodsInRecipe, dish.foodSOList))
                {
                    OnCompleteRecipe?.Invoke(null, recipe); 
                    RecipeMenu.Remove(recipe);
                    //StartCoroutine(GenerateDeliveryMenu());
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
                StartCoroutine(GenerateDeliveryMenu());
            }          
        }
    }
}
