using UnityEngine;
using Items.Fruits;
using ScriptableObjects.Data;
using Data;

namespace Managers
{
    public class FruitsManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerStatistics playerStatistics;

        [Header("Status")]
        [SerializeField] private FruitsCount fruitsCount;

        public static FruitsManager Instance { get; private set; }
        public FruitsCount FruitsCount { get => fruitsCount; set => fruitsCount = value; }

        private void Awake() => Instance = this;

        public void AddFruitCount(Fruit.FruitsTypes fruit, int value)
        {
            fruitsCount.fruits[(int)fruit] += value;
            fruitsCount.totalFruits += value;
            playerStatistics.FruitsCount.totalFruits += value;
            playerStatistics.FruitsCount.fruits[(int)fruit] += value;
            ScoreManager.Instance.AddScore(5);
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}