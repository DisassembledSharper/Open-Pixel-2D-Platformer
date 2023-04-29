using ScriptableObjects.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Fruits
{
    public class FruitSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;
        
        public void PlayPickFruit()
        {
            audioSource.PlayOneShot(soundsDB.PickFruit);
        }
    }
}