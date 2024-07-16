using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class DishCounter : Counter, IContainKitchenObject
    {
        [SerializeField] private GameObject Dish;
        private Stack<GameObject> dishStack = new Stack<GameObject>();
        private int dishOntopCount = 0;
        private int maxDishOnTopNum = 4;
        private List<Vector3> locateDishOnTop = new List<Vector3>();

        private void Awake()
        {
            SpawnKitchenObject(Dish, 10);
            SetLocatePoint();
        }
        private void Start()
        {
            SetTopDish();
        }
        private void Update()
        {
            ManageKitchenObject();
        }
        public void SpawnKitchenObject(GameObject dish, int maxNum)
        {
            //Spawn Dish 
            for (int i = 0; i < maxNum; i++)
            {
                GameObject prefab = Instantiate(dish, transform);
                dishStack.Push(prefab);
                prefab.transform.position = transform.TransformPoint(Vector3.zero);
            }
        }
        //if this has no dish then spawn 
        public void ManageKitchenObject()
        {
            if (dishStack.Count <= 0)
            {
                SpawnKitchenObject(Dish, 10);
            }
        }
        public override void ResetKitchenObject()
        {
            dishOntopCount--;
            base.ResetKitchenObject();
        }
        // Set location of dish layers
        private void SetLocatePoint()
        {
            locatePoint = new Vector3(0, 1.3f, 0);
            locateDishOnTop.Add(locatePoint);
            for (int i = 1; i <= maxDishOnTopNum; i++)
            {
                Vector3 locate = new Vector3(0, locateDishOnTop[i - 1].y + .1f, 0);
                locateDishOnTop.Add(locate);
            }
        }
        //Set dish layer after time
        IEnumerator SetDishOnTop()
        {
            yield return new WaitForSeconds(2f);
            if (dishOntopCount < locateDishOnTop.Count)
            {
                GameObject prefab = dishStack.Pop();
                prefab.transform.position = this.transform.TransformPoint(locateDishOnTop[dishOntopCount]);
                kitchenObject = prefab.GetComponent<KitchenObject>();
                dishOntopCount++;                
            }       
            SetTopDish();
        }
        private void SetTopDish()
        {
            StartCoroutine(SetDishOnTop());
        }
        public void PushIntoStack(GameObject dish)
        {
            dishStack.Push(dish);
        }
    }
}
