using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class VolumeManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private SoundTypes soundType;

        [Header("References")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private UserSettings playerSettings;
        
        public enum SoundTypes { Music, Effects }

        private void Update()
        {
            float volume;
            if (soundType == SoundTypes.Music) volume = playerSettings.MusicVolume;
            else volume = playerSettings.EffectsVolume;

            audioSource.volume = volume;
        }
    }
}