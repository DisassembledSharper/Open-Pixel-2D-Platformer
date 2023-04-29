using ScriptableObjects.Sounds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        public void PlayHitSound()
        {
            audioSource.PlayOneShot(soundsDB.Hit);
        }

        public void PlayJumpSound()
        {
            audioSource.PlayOneShot(soundsDB.Jump);
        }

        public void PlayAppearSound()
        {
            audioSource.PlayOneShot(soundsDB.Appear);
        }

        public void PlayDesappearSound()
        {
            audioSource.PlayOneShot(soundsDB.Desappear);
        }
    }
}