using AchievementSystem;
using Data;
using ScriptableObjects;
using ScriptableObjects.Data;
using Sound;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MenuUIManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject levelPanel;
        [SerializeField] private GameObject levelsPanel;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private StarsManager starsManager;
        [SerializeField] private ScoresGetter[] scoresGetters;
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private LevelDataObject globalSelectedLevelData;

        [Header("Status")]
        [SerializeField] private bool isEnablingLevelPanel;
        [SerializeField] private bool isChangingPanel;
        [SerializeField] private bool isDisablingPanel;

        private void Awake()
        {
            if (LevelMusic.Instance != null)
            {
                Destroy(LevelMusic.Instance.gameObject);
            }
        }

        private void Start()
        {
            Time.timeScale = 1;
            
            if (playerSettings.EnableLevelPanel)
            {
                levelsPanel.SetActive(true);
            }
            playerSettings.EnableLevelPanel = true;
        }
        private void OnApplicationQuit()
        {
            playerSettings.EnableLevelPanel = false;
        }
        public void EnableLevelPanel(LevelDataObject levelDataObject)
        {
            globalSelectedLevelData.CloneLevelData(levelDataObject);
            levelPanel.SetActive(true);
            StartCoroutine(LevelPanelCoroutine());
        }

        public void LoadSelectedLevel()
        {
            ScenesManager.Instance.LoadScene(globalSelectedLevelData.LevelData.levelNumber);
        }
        public void EnablePanel(GameObject panel)
        {
            if (isChangingPanel || isEnablingLevelPanel) return;
            panel.SetActive(true);
            isChangingPanel = true;
        }

        public void DisablePanel(Animator panelAnimator)
        {
            if (!isChangingPanel || isDisablingPanel || isEnablingLevelPanel) return;

            StartCoroutine(DisableCoroutine(panelAnimator));
        }

        private IEnumerator DisableCoroutine(Animator panelAnimator)
        {
            isDisablingPanel = true;
            panelAnimator.SetTrigger("close");
            yield return new WaitForSeconds(0.667f);
            isDisablingPanel = false;
            isChangingPanel = false;
            panelAnimator.gameObject.SetActive(false);
        }

        private IEnumerator LevelPanelCoroutine()
        {
            isEnablingLevelPanel = true;
            yield return new WaitForSeconds(0.667f);
            isEnablingLevelPanel = false;
        }
    }
}