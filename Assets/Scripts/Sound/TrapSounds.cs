using ScriptableObjects.Sounds;
using UnityEngine;

namespace Sound
{
    public class TrapSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        public void PlayTrampolineJump()
        {
            audioSource.PlayOneShot(soundsDB.TrampolineJump);
        }
    }
}