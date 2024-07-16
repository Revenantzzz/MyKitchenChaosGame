using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class StoveCounterVisual : MonoBehaviour
    {
        StoveCounter counter;
        [SerializeField] GameObject sizzlingParticles;
        [SerializeField] GameObject stoveOnVisual;
        private void Awake()
        {
            counter = this.transform.parent.GetComponent<StoveCounter>();
        }
        private void Start()
        {
            counter.FlameSwitch += Counter_FlameOn;
        }

        private void Counter_FlameOn(bool isFlameOn)
        {
            sizzlingParticles.gameObject.SetActive(isFlameOn);
            stoveOnVisual.gameObject.SetActive(isFlameOn);
        }
    }
}
