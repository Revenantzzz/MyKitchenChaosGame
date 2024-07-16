using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class AudioManager : MonoBehaviour
    {       
        public static AudioManager Instance;
        private AudioSource audioSource;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            audioSource = GetComponent<AudioSource>();
        }
    }
}