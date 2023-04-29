using Actors.GenericClasses.Health.HealthSystems;
using Managers;
using ScriptableObjects;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Actors.Player
{
    public class PlayerHealth : HealthSystem
    {
        [Header("Config")]
        [SerializeField] private float blinkHealthDelay = 0.5f;

        [Header("References")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PlayerEffects playerEffects;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private PlayerAnimationsController animationsController;
        [SerializeField] private PointSaver pointSaver;

        [Header("Status")]
        [SerializeField] private int currentRevives;
        [SerializeField] private bool canBlinkHealth;
        public bool CanTakeDamage { get => canTakeDamage; set => canTakeDamage = value; }

        private void Start()
        {
            currentHealth = startHealth;
            canTakeDamage = true;
            UpdateHealthImage();
            ScreenUIManager.Instance.UpdateReviveText(currentRevives);
        }

        protected override void OnTakeDamage()
        {
            UpdateHealthImage();
            animationsController.SetTrigger("hit");
            playerJump.IsJumping = false;
            effectsPlayer.PlaySound(soundsDB.Hit);
            if (currentHealth == 1)
            {
                canBlinkHealth = true;
                StartCoroutine(BlinkHealth());
            }
            else canBlinkHealth = false;
            if (Gamepad.current != null)
            {
                StartCoroutine(Rumble());
            }
            StartCoroutine(Recovery(0.05f));
        }

        protected override void OnDie()
        {
            playerMovement.FreezeInput = true;
            playerJump.FreezeInput = true;
            StartCoroutine(playerEffects.Desappear());
            if (Gamepad.current != null) Gamepad.current.SetMotorSpeeds(0, 0);
            AdsManager.Instance.TryShowInterstitial();
            if (currentRevives > 0) ScreenUIManager.Instance.ShowRevive(currentRevives);
            else ScreenUIManager.Instance.ShowGameOver();
        }

        private void UpdateHealthImage()
        {
            ScreenUIManager.Instance.UpdatePlayerHealth(currentHealth, startHealth);
        }

        public void SetHealth(int value)
        {
            currentHealth = value;
            UpdateHealthImage();
            canBlinkHealth = false;
        }

        public void Revive()
        {
            currentRevives--;
            ScreenUIManager.Instance.UpdateReviveText(currentRevives);
            isDead = false;
            currentHealth = startHealth;
            UpdateHealthImage();
            gameObject.SetActive(true);
            StartCoroutine(playerEffects.Appear());
            if (pointSaver.SavedCheckPoint != Vector3.zero)
            {
                transform.position = pointSaver.SavedCheckPoint;
            }
            else
            {
                transform.position = GameManager.Instance.StartPosition.position;
            }
            StartCoroutine(Recovery(0.05f));
            GameManager.Instance.CanCallPause = true;
        }

        private IEnumerator BlinkHealth()
        {
            while (canBlinkHealth)
            {
                ScreenUIManager.Instance.SetHealthObjectActive(false);
                yield return new WaitForSeconds(blinkHealthDelay);
                ScreenUIManager.Instance.SetHealthObjectActive(true);
                yield return new WaitForSeconds(blinkHealthDelay);
            }
        }

        private IEnumerator Rumble()
        {
            Gamepad.current.SetMotorSpeeds(0.5f, 0.5f);
            yield return new WaitForSecondsRealtime(0.5f);
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
        private IEnumerator Recovery(float delay)
        {
            if (isDead) yield break;

            Color color = spriteRenderer.color;
            canTakeDamage = false;

            playerMovement.FreezeInput = true;
            playerJump.FreezeInput = true;

            yield return new WaitForSeconds(0.467f);

            playerMovement.FreezeInput = false;
            playerJump.FreezeInput = false;

            for (int i = 0; i < 10; i++)
            {
                color.a = 0;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(delay);
                color.a = 1;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(delay);
            }
            canTakeDamage = true;
        }
    }
}