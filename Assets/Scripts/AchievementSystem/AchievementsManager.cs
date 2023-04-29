using Data;
using Managers;
using ScriptableObjects.Achievements;
using ScriptableObjects.Data;
using ScriptableObjects.Sounds;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace AchievementSystem
{
    public class AchievementsManager : MonoBehaviour
    {
        [SerializeField] private Achievement[] completeThreeStars;
        [SerializeField] private Achievement[] playTimes;
        [SerializeField] private Achievement[] allAchievements;
        [SerializeField] private LevelDataObject[] levelsData;
        [SerializeField] private GameObject achievementWarnObject;
        [SerializeField] private Image warnIcon;
        [SerializeField] private Image warnPrize;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private bool isShowingWarning;
        public static AchievementsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void VerifyAll()
        {
            VerifyThreeStars();
            VerifyPlayTimes();
        }

        public void VerifyThreeStars()
        {
            int levelsWithThreeStars = 0;

            foreach (LevelDataObject levelData in levelsData)
            {
                if (levelData.LevelData.starsCount == 3) levelsWithThreeStars++;
            }
            foreach (Achievement completeAchievement in completeThreeStars)
            {
                if (levelsWithThreeStars >= completeAchievement.value)
                {
                    UnlockAchievement(completeAchievement);
                }
            }
        }
        public void VerifyPlayTimes()
        {
            int totalPlayTimes = 0;

            foreach (LevelDataObject levelData in levelsData)
            {
                totalPlayTimes += levelData.LevelData.playsCount;
            }
            foreach (Achievement completeAchievement in playTimes)
            {
                if (totalPlayTimes >= completeAchievement.value)
                {
                    UnlockAchievement(completeAchievement);
                }
            }
        }

        private void UnlockAchievement(Achievement achievement)
        {
            if (!achievement.isUnlocked)
            {
                achievement.Unlock();
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.SaveGameData();
                }
                else
                {
                    GameDataLoader.Instance.SaveGameData();
                }
                StartCoroutine(ShowWarn(achievement.icon, achievement.prize, achievement.textKey));
            }
        }
        private IEnumerator ShowWarn(Sprite icon, Sprite prize, string key)
        {
            while (isShowingWarning)
            {
                yield return null;
            }
            warnIcon.sprite = icon;
            warnPrize.sprite = prize;
            LocalizationSettings.StringDatabase.GetLocalizedStringAsync(key).Completed += result => achievementText.text = result.Result;
            
            isShowingWarning = true;
            achievementWarnObject.SetActive(true);
            audioSource.PlayOneShot(soundsDB.Achievement);
            yield return new WaitForSecondsRealtime(4f);
            achievementWarnObject.SetActive(false);
            isShowingWarning = false;
        }

        public List<Achievement> GetAllAchievements()
        {
            List<Achievement> list = new();
            list.AddRange(allAchievements);
            return list;
        }
    }
}