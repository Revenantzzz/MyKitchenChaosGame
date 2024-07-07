using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MyKitchenChaos
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] InputReader inputReader;
        [SerializeField] Animator animator;
        CharacterController characterController;  

        [Header("Settings")]
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float rotateSpeed = 10f;

        private Vector3 moveVector = Vector3.zero;
        const string moveAnim = "IsWalking";

        public event UnityAction PickAndPut;
        public event UnityAction Interacted;
    
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }
        private void Start()
        {
            inputReader.EnablePlayerAction();
            inputReader.PickAndPut += HandlePickAndPut;
            inputReader.Interact += HandleInteract;
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleCharacterController();
        }

        private void HandleMovement()
        {
            moveVector = new Vector3(inputReader.Direction.x, 0, inputReader.Direction.y);
            animator.SetBool(moveAnim, moveVector.magnitude > 0);
        }
        private void HandleRotation()
        {
            transform.forward = Vector3.Slerp(transform.forward, moveVector, rotateSpeed * Time.deltaTime);
        }
        private void HandleCharacterController()
        {
            characterController.SimpleMove(moveVector * moveSpeed);
        }
        private void HandlePickAndPut()
        {
            PickAndPut?.Invoke();
        }
        private void HandleInteract()
        {
            Interacted?.Invoke();
        }

    }
}
