using ScriptableObjects.Sounds;
using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.SoundManagement
{
    public class FlyEnemyWingSound : MonoBehaviour
    {
        [Header("Config")]
        [Range(0.1f, 10)]
        [SerializeField] private float playDelay;

        [Header("References")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private SoundsDB soundsDB;

        [Header("Status")]
        [SerializeField] private float counter;

        private void Update()
        {
            counter += Time.deltaTime;

            if (counter >= playDelay)
            {
                counter = 0;
                audioSource.PlayOneShot(soundsDB.FlyEnemyWings);
            }
        }
    }
}