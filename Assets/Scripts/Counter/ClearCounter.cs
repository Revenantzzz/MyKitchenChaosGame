using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyKitchenChaos
{
    public class ClearCounter : Counter
    {
        private void Start()
        {
            locatePoint = new Vector3(0, 1.3f, 0);
            Initiallize();
        }        
    }
}
