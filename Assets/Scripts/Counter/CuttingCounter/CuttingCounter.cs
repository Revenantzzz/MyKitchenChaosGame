using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class CuttingCounter : Counter
    {
        [SerializeField] private ProgressBar progressBar;
        private int cuttingTimes;
        private int cuttingTimer = 0;
        public event UnityAction<bool> Cut;

        private void Awake()
        {
            locatePoint = new Vector3(0, 1.3f, 0);
        }
        private void Start()
        {
            PlayerObjectCarrier.Instance.Interacted += Cutting;
            progressBar.gameObject.SetActive(false);
        }
        private void Cutting(Counter counter)
        {
            if (counter != this || !HasKitchenObject)
            {
               return;
            }
            if (this.kitchenObject is CuttingFood cutting)
            {
                progressBar.gameObject.SetActive(true);
                cuttingTimes = cutting.CuttingTime;
                if (cuttingTimer >= cuttingTimes - 1)
                {
                    progressBar.gameObject.SetActive(false);
                    cutting.Cut();
                    return;
                }
                StartCoroutine(CutFood(cutting));
            }
        }
        IEnumerator CutFood(CuttingFood cutting)
        {            
            if (cutting.IsRaw)
            {
                cuttingTimer++;
                Cut?.Invoke(true);
                SetProgressbarValue();
            }
            PlayerObjectCarrier.Instance.Interacted -= Cutting;
            yield return new WaitForSeconds(.1f);
            PlayerObjectCarrier.Instance.Interacted += Cutting;
        }
        private void SetProgressbarValue()
        {
            progressBar.SetProgressValue(((float)cuttingTimer / cuttingTimes));
        }
        public override void ResetKitchenObject()
        {
            base.ResetKitchenObject(); 
            cuttingTimer = 0;
            progressBar.gameObject.SetActive(false );
        }
    }
}
