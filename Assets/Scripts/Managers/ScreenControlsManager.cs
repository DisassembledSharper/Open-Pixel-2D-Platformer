using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    public class ScreenControlsManager : MonoBehaviour
    {
        [SerializeField] private GameObject controls;
        [SerializeField] private UserSettings playerSettings;
        private bool controlByScreen;

        public static ScreenControlsManager Instance { get; private set; }
        public bool ControlByScreen { get => controlByScreen; set => controlByScreen = value; }

        private void Awake()
        {
            controlByScreen = playerSettings.IsMobile;
            Instance = this;
        }

        private void Start()
        {
            if (controlByScreen)
            {
                Application.targetFrameRate = 60;
                controls.SetActive(true);
            }
            else
            {
                controls.SetActive(false);
            }
        }
        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}