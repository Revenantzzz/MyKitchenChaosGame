using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class DeliveryCounterSound : SoundPlayer
    {
        [SerializeField] private List<AudioClip> deliveryFailSoundList;
        private DeliveryCounter deliveryCounter;
        private void Start()
        {
            if(this.transform.parent.TryGetComponent<DeliveryCounter>(out deliveryCounter))
            {
                deliveryCounter.Done += PlaySound;
            }
        }
        protected override void PlaySound(bool isSoundOn)
        {
            AudioClip clip;
            if (isSoundOn)
            {
                 clip = clipList[Random.Range(0, clipList.Count)];
            }
            else
            {
                clip = deliveryFailSoundList[Random.Range(0, deliveryFailSoundList.Count)];
            }
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
