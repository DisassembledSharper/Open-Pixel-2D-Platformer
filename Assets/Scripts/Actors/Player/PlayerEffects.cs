using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerEffects : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float appearingDuration;
        [SerializeField] private float desappearingDuration;
        [SerializeField] private float slidingWallDustInterval;
        [Header("References")]
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private GameObject appearingObject;
        [SerializeField] private GameObject desappearingObject;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ParticleSystem slidingWallDust;
        [SerializeField] private ParticleSystem doubleJumpDust;
        [SerializeField] private ParticleSystem groundSlideDust;

        private void Start()
        {
            StartCoroutine(Appear());
        }

        public void PlayGroundSlideDust()
        {
            if (!groundSlideDust.isPlaying) groundSlideDust.Play();
        }
        public void PlayDoubleJumpDust()
        {
            if (!doubleJumpDust.isPlaying) doubleJumpDust.Play();
        }

        public void PlaySlidingWallDust()
        {
            if (!slidingWallDust.isPlaying) slidingWallDust.Play();
        }
        public void StopSlidingWallDust()
        {
            if (slidingWallDust.isPlaying) slidingWallDust.Stop();
        }
        public IEnumerator Appear()
        {
            spriteRenderer.enabled = false;
            playerMovement.FreezeInput = true;
            playerJump.FreezeInput = true;
            yield return new WaitForSeconds(0.5f);
            effectsPlayer.PlaySound(soundsDB.Appear);
            appearingObject.SetActive(true);
            yield return new WaitForSeconds(appearingDuration);
            playerMovement.FreezeInput = false;
            playerJump.FreezeInput = false;
            spriteRenderer.enabled = true;
            appearingObject.SetActive(false);
        }

        public IEnumerator Desappear()
        {
            spriteRenderer.enabled = false;
            desappearingObject.SetActive(true);
            effectsPlayer.PlaySound(soundsDB.Desappear);
            yield return new WaitForSeconds(desappearingDuration);
            gameObject.SetActive(false);
            desappearingObject.SetActive(false);
        }
    }
}