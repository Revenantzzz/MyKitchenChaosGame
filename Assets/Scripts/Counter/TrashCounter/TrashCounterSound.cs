using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class TrashCounterSound : SoundPlayer 
    {
        private TrashCounter trashCounter;
        private void Start()
        {
            if (this.transform.parent.TryGetComponent<TrashCounter>(out trashCounter))
            {
                trashCounter.Trash += PlaySound;
            }
        }
    }
}
