using Actors.Player;
using Data;
using Managers;
using ScriptableObjects;
using ScriptableObjects.Data;
using AchievementSystem;
using ScriptableObjects.Sounds;
using Sound;
using UnityEngine;

namespace Items.Points
{
    public class FinalPoint : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float launchForce;
        [SerializeField] private int nextScene;

        [Header("References")]
        [SerializeField] private GameObject completeObject;
        [SerializeField] private Animator animator;
        [SerializeField] private ParticleSystem confetti;
        [SerializeField] private SpriteRenderer finalSpriteRenderer;
        [SerializeField] private PointSaver pointSaver;
        [SerializeField] private LevelDataSaver levelDataSaver;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        private Rigidbody2D playerRig;

        [Header("Status")]
        [SerializeField] private bool wasPressed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (wasPressed) return;
                wasPressed = true;
                pointSaver.SavedCheckPoint = Vector3.zero;
                GameManager.Instance.CanCallPause = false;
                effectsPlayer.PlaySound(soundsDB.FinalPointPressed);
                playerRig = collision.GetComponent<Rigidbody2D>();
                collision.GetComponent<PlayerHealth>().CanTakeDamage = false;
                collision.GetComponent<PlayerJump>().FreezeInput = true;
                collision.GetComponent<PlayerMovement>().FreezeInput = true;
                
                levelDataSaver.OnFinishLevel();
                animator.SetTrigger("press");
                AchievementsManager.Instance.VerifyThreeStars();
                Invoke(nameof(LaunchPlayer), 0.3f);
                Invoke(nameof(EnablePanel), 2);
                AdsManager.Instance.TryShowInterstitial();
            }
        }

        private void EnablePanel()
        {
            completeObject.SetActive(true);
        }

        private void LaunchPlayer()
        {
            effectsPlayer.PlaySound(soundsDB.FinalPointReleased);
            confetti.Play();
            Invoke(nameof(ChangeLayer), 1);
            playerRig.velocity = new Vector2(playerRig.velocity.x, 0);
            playerRig.AddForce(launchForce * Vector2.up, ForceMode2D.Impulse);
        }
        private void ChangeLayer()
        {
            finalSpriteRenderer.sortingOrder -= 2;
        }
    }
}