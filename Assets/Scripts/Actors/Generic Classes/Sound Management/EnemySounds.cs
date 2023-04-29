using ScriptableObjects.Sounds;
using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.SoundManagement
{
    public class EnemySounds : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        public void PlayHit()
        {
            audioSource.PlayOneShot(soundsDB.EnemyHit);
        }

        public void PlayShoot()
        {
            audioSource.PlayOneShot(soundsDB.EnemyShoot);
        }

        public void PlayChameleonAttack()
        {
            audioSource.PlayOneShot(soundsDB.ChameleonAttack);
        }
        public void PlayDesappear()
        {
            audioSource.PlayOneShot(soundsDB.Desappear);
        }

        public void PlayWings()
        {
            audioSource.PlayOneShot(soundsDB.FlyEnemyWings);
        }

        public void PlayFalling()
        {
            audioSource.PlayOneShot(soundsDB.Falling);
        }

        public void PlayJump()
        {
            audioSource.PlayOneShot(soundsDB.Jump);
        }
    }
}