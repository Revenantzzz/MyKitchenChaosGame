using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public abstract class SoundPlayer : MonoBehaviour
    {
        protected AudioSource audioSource;
        [SerializeField] protected List<AudioClip> clipList;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        protected virtual void PlaySound(bool isSoundOn)
        {
            if(isSoundOn)
            {
                audioSource.clip = clipList[Random.Range(0, clipList.Count)];
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }
    }
}
