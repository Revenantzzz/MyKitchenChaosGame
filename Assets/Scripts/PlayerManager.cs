using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;

    private GameObject _food;
    private bool _isCounter = false;

    private float _speed = 5f;
    private Vector3 _move = Vector3.zero;
    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _direction = Vector2.zero;

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
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        _characterController.Move(_move * Time.deltaTime * _speed);
    }
    private void RotatePlayer()
    {
        var targetAngle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        _move = new Vector3(_moveInput.x, 0, _moveInput.y);
        if(_moveInput != Vector2.zero)
        {
            _direction = _moveInput;
        }
        if (context.started)
        {
            _animator.SetBool("IsWalking", true);
        }
        if (context.canceled)
        {
            _animator.SetBool("IsWalking", false);
        }
    }
    public void OnPickPut(InputAction.CallbackContext context)
    {
        if (context.started && _isCounter)
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
        if (context.started && _isCounter)
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
            _isCounter = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _isCounter = false;
    }
}
