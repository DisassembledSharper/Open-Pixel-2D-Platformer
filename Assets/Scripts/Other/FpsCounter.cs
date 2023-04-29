using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Other
{
    public class FpsCounter : MonoBehaviour
    {
        [SerializeField] private float refreshRate;
        [SerializeField] private TMP_Text fpsText;
        [SerializeField] private float refreshCounter;

        private void Start()
        {
            refreshCounter = refreshRate;
        }

        private void Update()
        {
            refreshCounter += Time.unscaledDeltaTime;

            if (refreshCounter >= refreshRate)
            {
                refreshCounter = 0;
                fpsText.text = "FPS: " + Mathf.RoundToInt(1f / Time.unscaledDeltaTime);
            }
        }
    }
}