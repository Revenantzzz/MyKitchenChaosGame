using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {
        public List<FoodSO> foodsInRecipe;
        public int Score;
    }
}
