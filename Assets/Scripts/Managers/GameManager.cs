using AchievementSystem;
using Actors.Player;
using Data;
using ScriptableObjects;
using ScriptableObjects.Achievements;
using ScriptableObjects.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerHealth playerHealth;
        [SerializeField] private GameObject pauseObject;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Transform startPosition;
        [SerializeField] private PointSaver pointSaver;
        [SerializeField] private GameObject playerObject;
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private LevelDataObject selectedLevelData;
        [SerializeField] private LevelDataObject[] levelsDataObjects;
        private Animator pausePanelAnimator;

        [Header("Status")]
        [SerializeField] private bool canCallPause;
        [SerializeField] private bool isPaused;

        public static GameManager Instance { get; private set; }
        public bool CanCallPause { get => canCallPause; set => canCallPause = value; }
        public Transform StartPosition { get => startPosition; private set => startPosition = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            canCallPause = true;
            Time.timeScale = 1;
            LevelDataManager.Instance.CurrentLevelData.LevelData.playsCount++;
            AchievementsManager.Instance.VerifyPlayTimes();
            selectedLevelData.CloneLevelData(LevelDataManager.Instance.CurrentLevelData);
            pausePanelAnimator = pauseObject.GetComponent<Animator>();
            if (pointSaver.SavedCheckPoint == Vector3.zero)
            {
                playerObject.transform.position = startPosition.position;
            }
            else
            {
                playerObject.transform.position = pointSaver.SavedCheckPoint;
            }
        }
        public void RevivePlayerByAd()
        {
            ScreenUIManager.Instance.DisableRevivePanel();
            playerHealth.Revive();
        }

        public void ShowRewarded()
        {
            ScreenUIManager.Instance.SetReviveButtonsInteractableState(false);
            AdsManager.Instance.CallRewarded();
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

        public void Pause()
        {
            if (!canCallPause) return;
            if (LevelPanelManager.Instance.IsChangingPanel) return;
            if (LevelPanelManager.Instance.IsOnAnotherPanel)
            {
                LevelPanelManager.Instance.EnablePanel(pauseObject, false);
                LevelPanelManager.Instance.DisablePanel(LevelPanelManager.Instance.CurrentPanelAnimator);
                StartCoroutine(SetAnotherPanelDelay());
                return;
            }
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseObject.SetActive(true);
                StartCoroutine(PauseCoroutine(false));
            }
            else
            {
                pausePanelAnimator.SetTrigger("close");
                StartCoroutine(PauseCoroutine(true));
                Time.timeScale = 1;
            }
        }

        private IEnumerator PauseCoroutine(bool isUnPausing)
        {
            canCallPause = false;
            yield return new WaitForSecondsRealtime(0.667f);
            if (isUnPausing) pauseObject.SetActive(false);
            canCallPause = true;
        }

        private IEnumerator SetAnotherPanelDelay()
        {
            yield return new WaitForSecondsRealtime(0.667f);
            LevelPanelManager.Instance.IsOnAnotherPanel = false;
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}