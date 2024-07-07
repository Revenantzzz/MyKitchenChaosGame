using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public abstract class KitchenObjectCarrier : MonoBehaviour
    {
        protected KitchenObject kitchenObject;
        protected Vector3 locatePoint = Vector3.zero;
        public bool HasKitchenObject => kitchenObject != null;
        public void Initiallize()
        {
            foreach(Transform child in transform)
            {
                if(child.TryGetComponent<KitchenObject>(out KitchenObject obj))
                {
                    SetKitchenObject(obj);
                }
            }
        }
        public KitchenObject GetKitchenObject()
        {
            return kitchenObject;
        }
        public virtual bool SetKitchenObject(KitchenObject kitchenObject)
        {
            kitchenObject.transform.parent = this.transform;
            this.kitchenObject = kitchenObject;           
            LocateKitchenObject();
            return true;
        }
        public virtual void ResetKitchenObject()
        {
            this.kitchenObject = null;
        }
        public void LocateKitchenObject()
        {
            if (HasKitchenObject)
            {
                kitchenObject.transform.position = transform.TransformPoint(locatePoint);
            }
        }
        public Kitchenware GetKitchenware()
        {
            if (kitchenObject.TryGetComponent<Kitchenware>(out Kitchenware kitchenware))
            {
                return kitchenware;
            }
            return null;
        }
        public bool HasKitchenware()
        {
            if(!HasKitchenObject)
            {
                return false;
            }
            if (kitchenObject.TryGetComponent<Kitchenware>(out Kitchenware kitchenware))
            {
                return true;
            }
            return false;
        }
        protected void SetNewKitchenObjectCarrier(KitchenObjectCarrier oldCarrier, KitchenObjectCarrier newCarrier, KitchenObject kitchenObj)
        {
            if (newCarrier.SetKitchenObject(kitchenObj))
            {
                oldCarrier.ResetKitchenObject();
            }
        }
    }
}
