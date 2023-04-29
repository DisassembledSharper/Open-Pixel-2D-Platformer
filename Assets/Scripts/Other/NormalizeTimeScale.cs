using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    public class NormalizeTimeScale : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1;
        }
    }
}