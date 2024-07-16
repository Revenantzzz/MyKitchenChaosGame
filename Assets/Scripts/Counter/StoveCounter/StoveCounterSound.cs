using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class StoveCounterSound : SoundPlayer
    {
        private StoveCounter stoveCounter;
        private void Start()
        {
            if(this.transform.parent.TryGetComponent<StoveCounter>(out stoveCounter))
            {
                stoveCounter.FlameSwitch += PlaySound;
            }
        }
    }
}
