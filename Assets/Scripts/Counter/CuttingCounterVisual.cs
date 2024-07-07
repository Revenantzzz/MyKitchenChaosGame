using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace MyKitchenChaos
{
    public class CuttingCounterVisual : MonoBehaviour
    {
        CuttingCounter Counter;
        Animator Animator;
        const string animationString = "Cut";
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Counter = transform.parent.gameObject.GetComponent<CuttingCounter>();
        }
        private void Start()
        {
            Counter.Cut += Counter_Cut;
        }

        private void Counter_Cut()
        {
            StartCoroutine(SetAnimationTrigger());
        }
        IEnumerator SetAnimationTrigger()
        {
            Animator.SetTrigger(animationString);
            Counter.Cut -= Counter_Cut;
            yield return new WaitForSeconds(.2f);
            Counter.Cut += Counter_Cut;
        }
    }
}
