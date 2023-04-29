using AchievementSystem;
using Managers;
using ScriptableObjects;
using ScriptableObjects.Achievements;
using ScriptableObjects.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class GameDataLoader : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private LevelDataObject[] levelsDataObjects;

        [Header("Panels")]
        [SerializeField] GameObject savePanel;

        [Header("Load Texts")]
        [SerializeField] GameObject saveNotFound;
        [SerializeField] GameObject loading;
        [SerializeField] GameObject creating;
        [SerializeField] GameObject loaded;
        [SerializeField] GameObject created;
        [SerializeField] GameObject saveCorrupt;

        [Header("Game data")]
        [SerializeField] private GameData gameData;

        [Header("Status")]
        [SerializeField] private int loadStatus;

        public int LoadStatus { get => loadStatus; set => loadStatus = value; }
        public static GameDataLoader Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            if (!playerSettings.EnableLevelPanel)
            {
                StartCoroutine(LoadCoroutine());
            }
        }
        public void SaveGameData()
        {
            GameData gameData = new()
            {
                globalFruitsCount = playerStatistics.FruitsCount,
                globalEnemiesCount = playerStatistics.EnemiesCount,
                unlockedNinjaFrog = playerStatistics.UnlockedNinjaFrog,
                unlockedPinkMan = playerStatistics.UnlockedPinkMan,
                unlockedVirtualGuy = playerStatistics.UnlockedVirtualGuy,
                fruitsBalance = playerStatistics.FruitsBalance,
                levelsData = new()
            };
            foreach (LevelDataObject levelDataObject in levelsDataObjects)
            {
                gameData.levelsData.Add(levelDataObject.LevelData);
            }
            foreach (Achievement achievement in AchievementsManager.Instance.GetAllAchievements())
            {
                gameData.allAchievements.Add(achievement.isUnlocked);
            }
            SaveManager.SaveData(gameData);
        }
        public void LoadGameData()
        {
            gameData = SaveManager.LoadData();
        }

        public void WriteGameDataOnObjects()
        {
            if (gameData != null)
            {
                playerStatistics.FruitsCount = gameData.globalFruitsCount;
                playerStatistics.UnlockedNinjaFrog = gameData.unlockedNinjaFrog;
                playerStatistics.UnlockedPinkMan = gameData.unlockedPinkMan;
                playerStatistics.UnlockedVirtualGuy = gameData.unlockedVirtualGuy;
                playerStatistics.FruitsBalance = gameData.fruitsBalance;
                int i = 0;
                foreach (LevelData levelData in gameData.levelsData)
                {
                    levelsDataObjects[i].LevelData = levelData;
                    i++;
                }
                i = 0;
                List<Achievement> achievements = AchievementsManager.Instance.GetAllAchievements();
                foreach (Achievement achievement in achievements)
                {
                    if (achievements.Count > gameData.allAchievements.Count && i + 1 > gameData.allAchievements.Count)
                    {
                        achievement.isUnlocked = false;
                        i++;
                        continue;
                    }

                    achievement.isUnlocked = gameData.allAchievements[i];
                    i++;
                }
            }
            else
            {
                creating.SetActive(true);
                playerStatistics.FruitsCount = new();
                playerStatistics.UnlockedNinjaFrog = false;
                playerStatistics.UnlockedPinkMan = false;
                playerStatistics.UnlockedVirtualGuy = false;
                playerStatistics.FruitsBalance = new();
                int i = 0;
                foreach (LevelDataObject levelDataObject in levelsDataObjects)
                {
                    levelDataObject.LevelData = new();
                    levelDataObject.LevelData.levelNumber = i;
                    i++;
                }
                foreach (Achievement achievement in AchievementsManager.Instance.GetAllAchievements())
                {
                    achievement.isUnlocked = false;
                }
                SaveManager.SaveData(gameData);
                creating.SetActive(false);
                created.SetActive(true);
            }
            AchievementsManager.Instance.VerifyAll();
            gameData = null;
        }
        private IEnumerator LoadCoroutine()
        {
            while (SettingsManager.Instance.ChangingLanguage)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(1);
            savePanel.SetActive(true);
            LoadGameData();
            loading.SetActive(false);

            if (loadStatus == 0)
            {
                WriteGameDataOnObjects();
                created.SetActive(false);
                loaded.SetActive(true);
            }
            else if (loadStatus == 1)
            {
                saveNotFound.SetActive(true);
            }
            else if (loadStatus == 2)
            {
                saveCorrupt.SetActive(true);
            }
        }
    }
}