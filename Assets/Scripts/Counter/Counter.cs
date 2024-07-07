using UnityEngine;

namespace MyKitchenChaos
{
    public class Counter : KitchenObjectCarrier
    {
        private bool canPick = true;
        public bool CanPick 
        { 
            get { return canPick; } 
            set { if (value != canPick) canPick = value; }
        }
        [SerializeField] GameObject selectedCounter;

        public void SetSelectedHighlight(bool isSelected)
        {
            if(selectedCounter != null)
            {
                selectedCounter.SetActive(isSelected);
            }
        }
    }
}
