using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private bool isChangingPanel;
        [SerializeField] private bool isDisablingPanel;
        [SerializeField] private bool isOnAnotherPanel;
        [SerializeField] private Animator currentPanelAnimator;
        public bool IsOnAnotherPanel { get => isOnAnotherPanel; set => isOnAnotherPanel = value; }
        public static LevelPanelManager Instance { get; private set; }
        public Animator CurrentPanelAnimator { get => currentPanelAnimator; set => currentPanelAnimator = value; }
        public bool IsChangingPanel { get => isChangingPanel; private set => isChangingPanel = value; }

        private void Awake()
        {
            Instance = this;
        }
        public void EnablePanel(GameObject panel)
        {
            if (isChangingPanel) return;
            currentPanelAnimator = panel.GetComponent<Animator>();
            panel.SetActive(true);
            isChangingPanel = true;
        }
        public void EnablePanel(GameObject panel, bool changeCurrent)
        {
            if (isChangingPanel) return;
            if (changeCurrent) currentPanelAnimator = panel.GetComponent<Animator>();
            panel.SetActive(true);
            isChangingPanel = true;
        }
        public void DisablePanel(Animator panelAnimator)
        {
            if (!isChangingPanel || isDisablingPanel) return;

            StartCoroutine(DisableCoroutine(panelAnimator));
        }
        public void DisablePanelWithoutEnable(Animator panelAnimator)
        {
            if (isDisablingPanel) return;

            StartCoroutine(DisableCoroutine(panelAnimator));
        }
        private IEnumerator DisableCoroutine(Animator panelAnimator)
        {
            isDisablingPanel = true;
            panelAnimator.SetTrigger("close");
            yield return new WaitForSecondsRealtime(0.667f);
            isDisablingPanel = false;
            isChangingPanel = false;
            panelAnimator.gameObject.SetActive(false);
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}