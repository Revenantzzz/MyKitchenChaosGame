using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

namespace MyKitchenChaos
{
    public class ContainCounterVisual : MonoBehaviour
    {
        
        ContainerCounter Counter;
        Animator Animator;
        const string animationString = "OpenClose";
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Counter = transform.parent.gameObject.GetComponent<ContainerCounter>();
        }
        private void Start()
        {
            Counter.OpenClose += Counter_OpenClose;          
        }

        private void Counter_OpenClose()
        {
            StartCoroutine(SetAnimationTrigger());
        }
        IEnumerator SetAnimationTrigger()
        {
            Animator.SetTrigger(animationString);
            Counter.OpenClose += Counter_OpenClose;
            yield return new WaitForSeconds(.1f);
            Counter.OpenClose -= Counter_OpenClose;
        }

    }
}
