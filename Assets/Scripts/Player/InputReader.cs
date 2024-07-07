using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputAction;

namespace MyKitchenChaos
{
    [CreateAssetMenu(fileName = "Input Reader")]
    public class InputReader : ScriptableObject,IPlayerActions
    {
        public event UnityAction<Vector2> Move;
        public event UnityAction PickAndPut;
        public event UnityAction Interact;
        PlayerInputAction inputActions;

        public Vector2 Direction => (Vector3)inputActions.Player.Move.ReadValue<Vector2>();
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerInputAction();
                inputActions.Player.SetCallbacks(this);
            }
            inputActions.Enable();
        }
        public void EnablePlayerAction()
        {
            inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Move?.Invoke(context.ReadValue<Vector2>()); 
        }

        public void OnPickAndPut(InputAction.CallbackContext context)
        {
            PickAndPut?.Invoke();
        }

        public void OnInteracte(InputAction.CallbackContext context)
        {
            Interact?.Invoke();
        }
    }
}
