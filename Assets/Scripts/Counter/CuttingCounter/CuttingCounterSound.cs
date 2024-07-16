using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MyKitchenChaos
{
    public class CuttingCounterSound : SoundPlayer
    {
        private CuttingCounter cuttingCounter;
        private void Start()
        {
            this.transform.parent.TryGetComponent<CuttingCounter>(out cuttingCounter);
            if (cuttingCounter != null)
            {
                cuttingCounter.Cut += PlaySound;
            }
        }
    }
}
