using ScriptableObjects.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    public class UISoundsManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        public void PlayClick()
        {
            audioSource.PlayOneShot(soundsDB.Click);
        }
    }
}