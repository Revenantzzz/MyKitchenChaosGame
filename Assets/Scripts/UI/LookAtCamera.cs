using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class LookAtCamera : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted
        }
        [SerializeField] private Mode mode;

        private void Update()
        {
            switch (mode)
            {
                case Mode.LookAt:
                    {
                        transform.LookAt(Camera.main.transform.position);
                        break;
                    }
                case Mode.LookAtInverted:
                    {
                        Vector3 toCameraDir = transform.position - Camera.main.transform.position;
                        transform.LookAt(transform.position + toCameraDir);
                        break;
                    }
                case Mode.CameraForward:
                    {
                        transform.forward = Camera.main.transform.forward;
                        break;
                    }
                case Mode.CameraForwardInverted:
                    {
                        transform.forward = -Camera.main.transform.forward;
                        break;
                    }
            }
        }
    }
}
