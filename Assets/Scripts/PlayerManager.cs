using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    private GameObject food;
    private bool isCounter = false;

    private float speed = 5f;
    private Vector3 move = Vector3.zero;
    private Vector2 moveInput = Vector2.zero;
    private Vector2 direction = Vector2.zero;

    private bool _isPicking;
    public bool isPicking
    {
        get
        {
            return _isPicking;
        }
        private set
        {
            _isPicking = value;
        }
    }
    private bool _isPutting = false;
    public bool isPutting
    {
        get 
        { 
            return _isPutting; 
        }
        private set 
        { 
            _isPutting= value;
        }
    }
    private bool _isInteracting = false;
    public bool isInteracting
    {
        get 
        { 
            return _isInteracting; 
        }
        private set
        {
            _isInteracting= value;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        characterController.Move(move * Time.deltaTime * speed);
    }
    private void RotatePlayer()
    {
        var targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        move = new Vector3(moveInput.x, 0, moveInput.y);
        if(moveInput != Vector2.zero)
        {
            direction = moveInput;
        }
        if (context.started)
        {
            animator.SetBool("IsWalking", true);
        }
        if (context.canceled)
        {
            animator.SetBool("IsWalking", false);
        }
    }
    public void OnPickPut(InputAction.CallbackContext context)
    {
        if (context.started && isCounter)
        {
            if (FoodInHand() != null)
            {
                isPicking = false;
                isPutting = true;
            }
            else
            {
                isPicking = true;
                isPutting = false;
            }
        }
        if (context.canceled)
        {
            isPicking = false;
            isPutting = false;
        }
    }
    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.started && isCounter)
        {
            isInteracting = true;
        }
        if (context.canceled)
        {
            isInteracting = false;
        }
    }
    public GameObject FoodInHand()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer >= 7 && child.gameObject.layer <= 9)
            {
                return (child.gameObject);
            }
        }
        return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            isCounter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isCounter = false;
    }
}
