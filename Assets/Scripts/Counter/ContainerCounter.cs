using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class ContainerCounter : Counter, IContainKitchenObject
    {
        [SerializeField] GameObject foodPrefab;
        [SerializeField] List<SpriteRenderer> containerIcon;
        [SerializeField] int maxSpawnNum = 10;
        public event UnityAction OpenClose;

        Stack<GameObject> foodStack = new Stack<GameObject>();
        private void Awake()
        {
            foodStack.Clear();
        }
        private void Start()
        {
            SpawnKitchenObject(foodPrefab, maxSpawnNum);               
        }
        private void Update()
        {
            ManageKitchenObject();
            SetContainerIcon();
        }
        //if no food then Spawn new
        public void ManageKitchenObject()
        {
            if (foodStack.Count <= 0)
            {
                SpawnKitchenObject(foodPrefab, maxSpawnNum);
            }
            if (!HasKitchenObject)
            {
                kitchenObject = foodStack.Pop().GetComponent<KitchenObject>();
            }           
        }
        //Spawn new foodPrefab with amount equal maxNum
        public void SpawnKitchenObject(GameObject foodPrefab, int maxNum)
        {
            foodStack.Clear();
            for (int i = 0; i < maxNum; i++)
            {
                GameObject prefab = Instantiate(foodPrefab, transform);
                prefab.transform.position = transform.TransformPoint(Vector3.zero);
                foodStack.Push(prefab);
            }
        }
        public override void ResetKitchenObject()
        {
            base.ResetKitchenObject();
            OpenClose?.Invoke();
        }
        public void PushIntoStack(GameObject food)
        {
            foodStack.Push(food);
        }
        //Set container Icon match to food
        private void SetContainerIcon()
        {
            if(foodPrefab.TryGetComponent<Food>(out Food food))
            {
                foreach(SpriteRenderer icon in containerIcon)
                {
                    icon.sprite = food.GetFoodIcon();
                }                
            }
        }
    }
}
