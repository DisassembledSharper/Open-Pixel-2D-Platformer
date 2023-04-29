using ScriptableObjects.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class LevelMusic : MonoBehaviour
    {
        [SerializeField] private MusicThemes levelTheme;
        [SerializeField] private int themeNumber;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private AudioSource audioSource;

        public enum MusicThemes { NormalTheme, DarkTheme}
        public static LevelMusic Instance { get; private set; }

        private void Awake()
        {
            AudioClip themeClip;
            switch (levelTheme)
            {
                case MusicThemes.DarkTheme:
                    themeClip = soundsDB.DarkLevelThemes[themeNumber];
                    break;

                default:
                    themeClip = soundsDB.LevelThemes[themeNumber];
                    break;
            }
            audioSource.clip = themeClip;
            audioSource.Play();
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (Instance.levelTheme == levelTheme && Instance.themeNumber == themeNumber)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(Instance.gameObject);
                    Instance = this;
                    DontDestroyOnLoad(this);
                }
            }
        }
    }
}