using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class PlayerObjectCarrier : KitchenObjectCarrier
    {

        [Header("Interact Setting")]
        [SerializeField] float interactDistance = 3f;
        [SerializeField] LayerMask kitchenObjectLayer;
        [SerializeField] LayerMask counterLayer;

        public static PlayerObjectCarrier Instance { get; private set; }
        public event UnityAction<Counter> OnSelectedCounter;
        public event UnityAction<Counter> Interacted;

        private float interactCounterDistance;
        private PlayerController playerController;
        private Vector3 interactSize;
        private RaycastHit raycastHit;
        private RaycastHit boxcastHit;

        private Counter counter;
        private KitchenObject selectedKitchenObject;
        private void Awake()
        {
            Instance = this;
            locatePoint = new Vector3(0, 1.5f, 1f);
            playerController = GetComponent<PlayerController>();
            interactCounterDistance = interactDistance - 2;
            interactSize = new Vector3(.5f, 1f, .5f);
        }
        private void Start()
        {
            Initiallize();
            playerController.PickAndPut += PickAndPut;
            playerController.Interacted += InteractWithCounter;
        }
        void Update()
        {
            CheckInteractedObject();
        }
        private void CheckInteractedObject()
        {
            //Check what object player is interacting
            bool checkCounter = CheckCounter();
            if (!checkCounter && !HasKitchenObject)
            {
                CheckKitchenObject();
            }
        }
        private void CheckKitchenObject()
        {
            //Check if there are any kitchenObject on floor in front of Player using Boxcast
            if (Physics.BoxCast(
                this.transform.position, 
                interactSize, 
                this.transform.forward, 
                out boxcastHit, 
                Quaternion.identity, 
                interactDistance, 
                kitchenObjectLayer))
            {
                if (boxcastHit.transform.TryGetComponent<KitchenObject>(out KitchenObject kitchenObj))
                {
                    this.selectedKitchenObject = kitchenObj;
                    return;
                }
            }
            this.selectedKitchenObject = null;
            return;
        }
        private bool CheckCounter()
        {
            //Check there are any containerCounter in range which player can interact 
            if (Physics.Raycast(transform.position, transform.forward, out raycastHit, interactCounterDistance, counterLayer))
            {
                if (raycastHit.transform.TryGetComponent<Counter>(out Counter counter))
                {
                    this.OnSelectedCounter?.Invoke(counter);
                    this.counter = counter;
                    return true;
                }
            }
            this.OnSelectedCounter?.Invoke(null);
            this.counter = null;
            return false;
        }
        private void PickAndPut()
        {
            StartCoroutine(PickAndPutCooldown());
        }
        private void Put()
        {
            //if have containerCounter then put kitchenObject on containerCounter else put on the floor
            if (counter != null)
            {
                if(!counter.HasKitchenObject)
                {
                    SetNewKitchenObjectCarrier(this,counter, kitchenObject);
                    return;
                }
                if(counter.HasKitchenware())
                {
                    counter.GetKitchenware().SetFood(kitchenObject as Food);
                    ResetKitchenObject();
                    return;
                }
                return;
            }
            else
            {
                this.kitchenObject.transform.parent = null;
                ResetKitchenObject();
            }            
        }
        private void Pick()
        {
            //if have containerCounter then pick kitchenObject on containerCounter else pick on the floor
            if (counter != null)
            {
                if (counter.HasKitchenObject)
                {
                    SetNewKitchenObjectCarrier(counter ,this , counter.GetKitchenObject());
                    return;
                }
            }
            if (selectedKitchenObject != null)
            {
                SetKitchenObject(selectedKitchenObject);
            }
        }
        private void PickAndPutWithKitchenware(Kitchenware kitchenware)
        {
            //Pick and put with kitchenware
            if(counter == null)
            {
                if (selectedKitchenObject != null)
                {
                    kitchenware.SetFood(selectedKitchenObject as Food);
                    return;
                }
                Put();
                return;
            }
            if(counter.HasKitchenObject)
            {
                if (kitchenware.SetFood(counter.GetKitchenObject() as Food))
                {
                    Debug.Log("SetFood");
                    counter.ResetKitchenObject();
                    return;
                }
            }
            Put();
            return;           
        }
        private void InteractWithCounter()
        {
            //Advance Interact like cutting food in cutting containerCounter
            if (this.counter != null)
            {             
                Interacted?.Invoke(this.counter);
            }
        }
        IEnumerator PickAndPutCooldown()
        {
            // Pick or Put depend on what player hold
            if (!HasKitchenObject)
            {
                Pick();
            } 
            else
            {
                if (HasKitchenware())
                {
                    PickAndPutWithKitchenware(GetKitchenware());
                }
                else
                {
                    Put();
                }
            }
            playerController.PickAndPut -= PickAndPut;
            yield return new WaitForSeconds(.1f);
            playerController.PickAndPut += PickAndPut;
        }
       
    }
}
