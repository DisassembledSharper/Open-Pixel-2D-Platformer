using UnityEngine;
using TMPro;
using UnityEngine.UI;
using ScriptableObjects;
using System.Collections;

namespace Managers
{
    public class ScreenUIManager : MonoBehaviour
    {
        [Header("References")]
        [Header("Images")]
        [SerializeField] private Image healthFill;
        [SerializeField] private GameObject healthObject;
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI reviveText;

        [Header("Game Objects")]
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private GameObject gameOverObject;
        [SerializeField] private GameObject revivePanel;
        [SerializeField] private GameObject confirmReviveButton;
        [SerializeField] private GameObject closeReviveButton;
        [SerializeField] private GameObject videoReviveButton;

        public static ScreenUIManager Instance { get; private set; }

        private void Awake() => Instance = this;


        public void UpdateScoreText(string text)
        {
            int zerosToPut = 7 - text.Length;
            string newScoreText = "";
            for (int i = 0; i < zerosToPut; i++)
            {
                newScoreText += "0";
            }
            newScoreText += text;
            scoreText.text = newScoreText;
        }

        public void UpdatePlayerHealth(int currentHealth, int startHealth)
        {
            StartCoroutine(SmoothHealth(currentHealth, startHealth));
        }
        public void SetHealthObjectActive(bool value)
        {
            healthObject.SetActive(value);
        }
        public void ShowGameOver()
        {
            gameOverObject.SetActive(true);
            GameManager.Instance.CanCallPause = false;
        }
        public void DisableRevivePanel()
        {
            revivePanel.SetActive(false);
        }
        public void ShowRevive(int remainingRevives)
        {
            confirmReviveButton.GetComponent<Button>().interactable = true;
            if (remainingRevives > 1)
            {
                confirmReviveButton.SetActive(true);
                videoReviveButton.SetActive(false);
            }
            else
            {
                videoReviveButton.SetActive(true);
                confirmReviveButton.SetActive(false);
            }
            revivePanel.SetActive(true);
            GameManager.Instance.CanCallPause = false;
        }

        public void SetReviveButtonsInteractableState(bool interactable)
        {
            videoReviveButton.GetComponent<Button>().interactable = interactable;
            closeReviveButton.GetComponent<Button>().interactable = interactable;
            StartCoroutine(RevertReviveButtons());
        }

        public void UpdateReviveText(int amount)
        {
            reviveText.text = amount.ToString();
        }
        private IEnumerator SmoothHealth(int currentHealth, int startHealth)
        {
            float newValue = (float) currentHealth / startHealth;

            while (healthFill.fillAmount != newValue)
            {
                healthFill.fillAmount = Mathf.MoveTowards(healthFill.fillAmount, newValue, Time.fixedDeltaTime * 1); ;
                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator RevertReviveButtons()
        {
            yield return new WaitForSecondsRealtime(5);
            SetReviveButtonsInteractableState(true);
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}