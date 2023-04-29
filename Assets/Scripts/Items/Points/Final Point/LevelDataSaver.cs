using Data;
using Managers;
using ScriptableObjects.Data;
using UnityEngine;

namespace Items.Points
{
    public class LevelDataSaver : MonoBehaviour
    {
        [SerializeField] private PlayerStatistics statistics;
        private LevelDataObject levelDataObject;

        public void OnFinishLevel()
        {
            levelDataObject = LevelDataManager.Instance.CurrentLevelData;
            levelDataObject.LevelData.isCompleted = true;

            ScoreCheck();
            FruitsCheck();
            EnemiesCheck();
            
            GameManager.Instance.SaveGameData();
        }

        private void ScoreCheck()
        {
            int previousScore = levelDataObject.LevelData.highScore;
            if (previousScore < ScoreManager.Instance.Score)
            {
                levelDataObject.LevelData.highScore = ScoreManager.Instance.Score;
            }

            if (ScoreManager.Instance.Score >= levelDataObject.MinScoreToTwoStars)
            {
                if (levelDataObject.LevelData.starsCount < 3)
                {
                    if (ScoreManager.Instance.Score >= levelDataObject.MinScoreToThreeStars)
                    {
                        levelDataObject.LevelData.starsCount = 3;
                    }
                    else
                    {
                        levelDataObject.LevelData.starsCount = 2;
                    }
                }
            }
            else
            {
                if (levelDataObject.LevelData.starsCount < 2)
                {
                    levelDataObject.LevelData.starsCount = 1;
                }
            }
        }

        private void FruitsCheck()
        {
            FruitsCount objectFruitsCount = levelDataObject.LevelData.pickedFruitsCount;
            FruitsCount currentFruitsCount = FruitsManager.Instance.FruitsCount;

            for (int i = 0; i < currentFruitsCount.fruits.Length; i++)
            {
                statistics.FruitsBalance.fruits[i] += currentFruitsCount.fruits[i]; 
            }

            for (int i = 0; i < currentFruitsCount.fruits.Length; i++)
            {
                if (objectFruitsCount.fruits[i] < currentFruitsCount.fruits[i])
                {
                    objectFruitsCount.fruits[i] = currentFruitsCount.fruits[i];
                }
            }
            if (objectFruitsCount.totalFruits < currentFruitsCount.totalFruits)
            {
                objectFruitsCount.totalFruits = currentFruitsCount.totalFruits;
            }
        }
        
        private void EnemiesCheck()
        {
            EnemiesCount objectEnemiesCount = levelDataObject.LevelData.enemiesCount;
            EnemiesCount currentEnemiesCount = ScoreManager.Instance.CurrentEnemiesCount;

            for (int i = 0; i < currentEnemiesCount.enemies.Length; i++)
            {
                if (objectEnemiesCount.enemies[i] < currentEnemiesCount.enemies[i])
                {
                    objectEnemiesCount.enemies[i] = currentEnemiesCount.enemies[i];
                }
            }
            if (objectEnemiesCount.totalEnemies < currentEnemiesCount.totalEnemies)
            {
                objectEnemiesCount.totalEnemies = currentEnemiesCount.totalEnemies;
            }
        }
    }
}