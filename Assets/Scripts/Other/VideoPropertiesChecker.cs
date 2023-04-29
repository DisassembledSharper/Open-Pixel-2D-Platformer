using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class VideoPropertiesChecker : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown frameRateDropDown;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private UserSettings playerSettings;

        private void OnEnable()
        {
            if (playerSettings.IsMobile)
            {
                if (Screen.currentResolution.refreshRate > 60)
                {
                    frameRateDropDown.interactable = true;
                }
                else
                {
                    frameRateDropDown.interactable = false;
                    SettingsManager.Instance.SetFramerate(0);
                }
                vSyncToggle.interactable = false;
            }
        }
    }
}