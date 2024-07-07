using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    [CreateAssetMenu()]
    public class KitchenwareFoodListSO : ScriptableObject
    {
        public List<FoodSO> ContainFoodList;
        public Transform foodInKitchenwareParent;
    }
}
