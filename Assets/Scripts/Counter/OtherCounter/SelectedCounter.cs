
using UnityEngine;

namespace MyKitchenChaos
{
    public class SelectedCounter : MonoBehaviour
    {
        private Counter interactedCounter;

        private void Awake()
        {
            interactedCounter = GetComponent<Counter>();
        }
        private void Start()
        {
            PlayerObjectCarrier.Instance.OnSelectedCounter += Interacted;
        }
        private void Interacted(Counter counter)
        {
            if (counter == interactedCounter)
            {
                interactedCounter.SetSelectedHighlight(true);
            }
            else
            {
                interactedCounter.SetSelectedHighlight(false);
            }
        }
    }
}
