using Data;
using ScriptableObjects.Data;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private int score;
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private EnemiesCount currentEnemiesCount;

        public static ScoreManager Instance { get; private set; }
        public int Score { get => score; set => score = value; }
        public EnemiesCount CurrentEnemiesCount { get => currentEnemiesCount; set => currentEnemiesCount = value; }

        private void Awake()
        {
            Instance = this;
        }

        public void AddScore(int value)
        {
            score += value;
            ScreenUIManager.Instance.UpdateScoreText(score.ToString());
        }
        public void AddScore(int value, int enemyId)
        {
            score += value;
            playerStatistics.EnemiesCount.enemies[enemyId] += 1;
            playerStatistics.EnemiesCount.totalEnemies += 1;
            currentEnemiesCount.enemies[enemyId] += 1;
            currentEnemiesCount.totalEnemies += 1;
            ScreenUIManager.Instance.UpdateScoreText(score.ToString());
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}