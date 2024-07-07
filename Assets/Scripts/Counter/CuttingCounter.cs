using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class CuttingCounter : Counter
    {
        int cuttingTimes;
        int cuttingTimer = 0;
        public event UnityAction Cut;

        private void Awake()
        {
            locatePoint = new Vector3(0, 1.3f, 0);
        }
        private void Start()
        {
            PlayerObjectCarrier.Instance.Interacted += Cutting;
        }
        private void Cutting(Counter counter)
        {
            if (counter == this && HasKitchenObject)
            {
                if (this.kitchenObject is CuttingFood cutting)
                {
                    cuttingTimes = cutting.CuttingTime;
                    StartCoroutine(CutFood(cutting));
                }
            }
        }
        IEnumerator CutFood(CuttingFood cutting)
        {            
            if (cutting.IsRaw)
            {
                if (cuttingTimer > cuttingTimes)
                {
                    cutting.Cut();
                    cuttingTimer = 0;
                }
                else
                {
                    cuttingTimer++;
                    Cut?.Invoke();
                }
            }
            PlayerObjectCarrier.Instance.Interacted -= Cutting;
            yield return new WaitForSeconds(.1f);
            PlayerObjectCarrier.Instance.Interacted += Cutting;
        }
    }
}
