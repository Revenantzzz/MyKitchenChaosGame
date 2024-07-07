using UnityEngine;

namespace MyKitchenChaos
{
    public abstract class KitchenObject : MonoBehaviour
    {
        private Counter parentCounter;
        protected void SetLocateOnFloor()
        {
            if(this.transform.parent == null && transform.position.y > .3f)
            {
                transform.position = new Vector3(transform.position.x, .3f, transform.position.z);
            }
        }
        protected void OnEnable()
        {
            if(this.transform.parent != null)
            {
                if (this.transform.parent.TryGetComponent<Counter>(out Counter counter))
                {
                    this.parentCounter = counter;
                    return;
                }
            }
            parentCounter = null;
        }
        public virtual void DisableKitchenObject()
        {
            this.transform.gameObject.SetActive(false);
            if (this.transform.parent != null)
            {
                if (this.transform.parent.TryGetComponent<KitchenObjectCarrier>(out KitchenObjectCarrier carrier))
                {
                    carrier.ResetKitchenObject();
                }
            }
            if (parentCounter != null)
            {
                this.transform.parent = parentCounter.transform;
                this.transform.position = parentCounter.transform.TransformPoint(Vector3.zero);
                AddToStack(parentCounter);
                this.transform.gameObject.SetActive(true);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        private void AddToStack(Counter parentCounter)
        {
            if (parentCounter is IContainKitchenObject container)
            {
                container.PushIntoStack(this.transform.gameObject);
            }
        }
            
    }
}
