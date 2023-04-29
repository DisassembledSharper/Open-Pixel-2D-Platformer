using System.Collections;
using UnityEngine;

namespace Sound
{
    public class SoundEffectsPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource targetAudioSource; 

        public void PlaySound(AudioClip clip)
        {
            targetAudioSource.PlayOneShot(clip);
        }
    }
}