using ScriptableObjects.Sounds;
using System.Collections;
using UnityEngine;

namespace Items
{
    public class BoxSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        public void PlayHitBox()
        {
            audioSource.PlayOneShot(soundsDB.HitBox);
        }

        public void PlayBreakBox()
        {
            audioSource.PlayOneShot(soundsDB.BreakBox);
        }
    }
}