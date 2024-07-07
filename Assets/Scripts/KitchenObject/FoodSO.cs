using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    [CreateAssetMenu(fileName = "FoodSO")]    
    public class FoodSO : ScriptableObject
    {
        public string Name;
        public GameObject Prefab;
        public Sprite Icon;
    }
}
