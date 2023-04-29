using ScriptableObjects.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Data
{
    public class StatisticsGetter : MonoBehaviour
    {
        [SerializeField] private PlayerStatistics statistics;
        [SerializeField] private TextMeshProUGUI[] fruitsTexts;

        private void OnEnable()
        {
            int i = 0;
            foreach (int fruitValue in statistics.FruitsCount.fruits)
            {
                fruitsTexts[i].text = fruitValue.ToString();
                i++;
            }
        }
    }
}