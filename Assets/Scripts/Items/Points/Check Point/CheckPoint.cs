using Actors.GenericClasses.Health.HealthSystems;
using Actors.Player;
using ScriptableObjects;
using ScriptableObjects.Sounds;
using Sound;
using UnityEngine;

namespace Items.Points
{
    public class CheckPoint : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PointSaver pointSaver;
        [SerializeField] private Animator animator;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;

        [Header("Status")]
        [SerializeField] private bool saved;

        private void Start()
        {
            if (pointSaver.SavedCheckPoint == transform.position)
            {
                animator.SetTrigger("flagOut");
                animator.SetBool("noFlag", false);
                saved = true;
            }
            else
            {
                animator.SetBool("noFlag", true);
                saved = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (saved) return;
                saved = true;
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
                playerHealth.SetHealth(4);
                pointSaver.SaveCheckPoint(this);
                animator.SetBool("noFlag", false);
                animator.SetTrigger("flagOut");
                effectsPlayer.PlaySound(soundsDB.CheckPoint);
            }
        }
    }
}