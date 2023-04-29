using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.EnemiesBehaviors;
using Actors.GenericClasses.SoundManagement;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.Enemies.FatBird
{
    public class FatBirdBehavior : EnemyBehavior
    {
        [Header("Config")]
        [SerializeField] private float raycastLength;
        [SerializeField] private float backSpeed;
        [SerializeField] private float flySpeed;
        [SerializeField] private float flyAmplitude;
        [SerializeField] private float groundPoundForce;

        [Header("References")]
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private Transform raycastPosition;
        [SerializeField] private AnimatorManager animatorManager;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private GameObject flySoundObject;

        [Header("Status")]
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private bool backingToStart;
        [SerializeField] private bool groundPounding;

        private void Start()
        {
            rig.gravityScale = 0;
            startPosition = transform.position;
        }

        private void FixedUpdate()
        {
            if (!backingToStart)
            {
                if (!groundPounding)
                {
                    float flyVelocity = Mathf.Sin(Time.time * flySpeed) * flyAmplitude;
                    rig.velocity = new Vector2(rig.velocity.x, flyVelocity);
                }

                RaycastHit2D playerRaycast;
                playerRaycast = Physics2D.Raycast(raycastPosition.position, Vector2.down, raycastLength, playerLayer);
                if (playerRaycast.collider != null)
                {
                    if (!groundPounding) StartCoroutine(GroundPoundCoroutine());
                }
            }
            else
            {
                if (Vector2.Distance(transform.position, startPosition) > 0.3f)
                {
                    rig.velocity = new Vector2(rig.velocity.x, backSpeed);
                }
                else
                {
                    rig.velocity = Vector2.zero;
                    backingToStart = false;
                }
            }
        }
        private IEnumerator GroundPoundCoroutine()
        {
            groundPounding = true;
            flySoundObject.SetActive(false);
            animatorManager.SetBool("groundPounding", groundPounding);
            animatorManager.SetTrigger("fall");
            effectsPlayer.PlaySound(soundsDB.Falling);
            yield return new WaitForSeconds(0.267f);
            rig.gravityScale = 4;
            rig.AddForce(Vector2.down * groundPoundForce, ForceMode2D.Impulse);
        }
        private IEnumerator BackToStartCoroutine()
        {
            groundPounding = false;
            flySoundObject.SetActive(true);
            animatorManager.SetBool("groundPounding", groundPounding);
            rig.gravityScale = 0;
            yield return new WaitForSeconds(0.267f);
            backingToStart = true;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 6)
            {
                StartCoroutine(BackToStartCoroutine());
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(raycastPosition.position, Vector2.down * raycastLength);
        }
    }
}