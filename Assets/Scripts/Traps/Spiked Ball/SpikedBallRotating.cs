using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class SpikedBallRotating : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private void Update()
        {
            transform.eulerAngles += new Vector3(0, 0, rotationSpeed * Time.deltaTime);
        }
    }
}