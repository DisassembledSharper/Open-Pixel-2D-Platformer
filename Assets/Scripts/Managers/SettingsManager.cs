using ScriptableObjects;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Managers
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown framerateDropDown;
        [SerializeField] private TMP_Dropdown languageDropDown;
        [SerializeField] private TMP_Dropdown controlDropDown;
        [SerializeField] private Toggle showFpsToggle;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider effectsSlider;
        [SerializeField] private GameObject fpsCounterObject;
        [SerializeField] private UserSettings userSettings;
        [SerializeField] private bool changingLanguage;
        public static SettingsManager Instance { get; private set; }
        public bool ChangingLanguage { get => changingLanguage; private set => changingLanguage = value; }

        private void Awake() => Instance = this;

        private void Start()
        {
            LoadSettings();
            SetFramerate(userSettings.TargetFramerate);
            SetFpsCounterState(userSettings.ShowFps);
            SetVSync(userSettings.VSync);
            SetMusicVolume(userSettings.MusicVolume);
            SetEffectsVolume(userSettings.EffectsVolume);
            SetLanguage(userSettings.Language);
            SetControl(userSettings.Control);
        }

        public void LoadSettings()
        {
            bool firstLoad;
            if (!PlayerPrefs.HasKey("firstLoad")) firstLoad = true;
            else firstLoad = false;

            if (firstLoad)
            {
                userSettings.MusicVolume = 1;
                userSettings.EffectsVolume = 1;
                userSettings.TargetFramerate = 0;
                userSettings.ShowFps = false;
                if (!userSettings.IsMobile) userSettings.VSync = true;
                userSettings.Control = 0;
                switch (Application.systemLanguage)
                {
                    case SystemLanguage.Portuguese:
                        userSettings.Language = 1;
                        break;
                    default:
                        userSettings.Language = 0;
                        break;
                }
                PlayerPrefs.SetInt("firstLoad", 1);
                SetCharacter(0);
                SaveSettings();
            }
            else
            {
                userSettings.MusicVolume = PlayerPrefs.GetFloat("musicVolume");
                userSettings.EffectsVolume = PlayerPrefs.GetFloat("effectsVolume");
                userSettings.TargetFramerate = PlayerPrefs.GetInt("targetFramerate");
                userSettings.Control = PlayerPrefs.GetInt("control");
                SetCharacter(PlayerPrefs.GetInt("selectedCharacter", 0));
                int value = PlayerPrefs.GetInt("showFps");
                userSettings.ShowFps = value == 1;
                value = PlayerPrefs.GetInt("vSync");
                userSettings.VSync = value == 1;
                userSettings.Language = PlayerPrefs.GetInt("language");
            }

            controlDropDown.value = userSettings.Control;
            musicSlider.value = userSettings.MusicVolume;
            effectsSlider.value = userSettings.EffectsVolume;
            framerateDropDown.value = userSettings.TargetFramerate;
            showFpsToggle.isOn = userSettings.ShowFps;
            vSyncToggle.isOn = userSettings.VSync;
            languageDropDown.value = userSettings.Language;
            if (firstLoad) SaveSettings();
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetFloat("musicVolume", userSettings.MusicVolume);
            PlayerPrefs.SetFloat("effectsVolume", userSettings.EffectsVolume);
            PlayerPrefs.SetInt("targetFramerate", userSettings.TargetFramerate);
            PlayerPrefs.SetInt("showFps", userSettings.ShowFps ? 1 : 0);
            PlayerPrefs.SetInt("vSync", userSettings.VSync ? 1 : 0);
            PlayerPrefs.SetInt("language", userSettings.Language);
            PlayerPrefs.SetInt("control", userSettings.Control);
        }
        
        public void SetCharacter(int value)
        {
            userSettings.Character = (UserSettings.Characters) value;
            PlayerPrefs.SetInt("selectedCharacter", value);
        }

        public void SetControl(int value)
        {
            userSettings.Control = value;
        }
        public void SetMusicVolume(float value)
        {
            userSettings.MusicVolume = value;
        }
        public void SetEffectsVolume(float value)
        {
            userSettings.EffectsVolume = value;
        }
        public void SetFramerate(int value)
        {
            userSettings.TargetFramerate = value;
            Application.targetFrameRate = value == 0 ? 60 : 120;
        }
        public void SetFpsCounterState(bool value)
        {
            userSettings.ShowFps = value;
            fpsCounterObject.SetActive(value);
        }

        public void SetVSync(bool value)
        {
            userSettings.VSync = value;
            if (value == true)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }
        }

        public void SetLanguage(int value)
        {
            if (changingLanguage) return;
            changingLanguage = true;
            userSettings.Language = value;
            //StartCoroutine(SetLanguageCoroutine(value));
            LocalizationSettings.InitializationOperation.WaitForCompletion();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
            changingLanguage = false;
        }

        private IEnumerator SetLanguageCoroutine(int value)
        {
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
            changingLanguage = false;
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}