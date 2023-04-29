using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.Detectors;
using Actors.GenericClasses.EnemiesBehaviors.Movement;
using Actors.GenericClasses.SoundManagement;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors.Mechanics
{
    public class EnemyJump : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpDelay;
        [SerializeField] private float minJumpInterval;
        [SerializeField] private float maxJumpInterval;
        [SerializeField] private bool jumpWhenCollides;
        [SerializeField] private int minCounts = 5;
        
        [SerializeField] private bool callAnimationBeforeDelay;

        [Header("References")]
        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private AnimatorManager animatorManager;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private EnemyGroundMovement groundMovement;
        [Header("Status")]
        [SerializeField] private float jumpInterval;
        [SerializeField] private float jumpCounter;
        [SerializeField] private int currentCollideCounts;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool isFalling;

        private void Start()
        {
            if (jumpWhenCollides) groundMovement.OnCollideWall += IncrementCollideCount;
            GenerateNewJumpInterval();
        }

        private void Update()
        {
            animatorManager.SetBool("isJumping", isJumping);
            animatorManager.SetBool("isFalling", isFalling);

            jumpCounter += Time.deltaTime;

            if (jumpCounter >= jumpInterval)
            {
                jumpCounter = 0;
                
                StartCoroutine(Jump());
                GenerateNewJumpInterval();
            }

            if (jumpWhenCollides)
            {
                if (currentCollideCounts >= minCounts)
                {
                    jumpCounter = 0;
                    currentCollideCounts = 0;
                    StartCoroutine(Jump());
                    GenerateNewJumpInterval();
                }
            }
        }

        private void FixedUpdate()
        {
            if (rig.velocity.y <= 0.1f && isJumping) isFalling = true;

            if (groundDetector.IsOnGround && isFalling)
            {
                isJumping = false;
                isFalling = false;
            }
        }

        private void GenerateNewJumpInterval()
        {
            jumpInterval = Random.Range(minJumpInterval, maxJumpInterval);
        }
        private void IncrementCollideCount()
        {
            currentCollideCounts++;
        }
        private IEnumerator Jump()
        {
            if (jumpDelay > 0)
            {
                if (callAnimationBeforeDelay)
                {
                    animatorManager.SetTrigger("jump");
                    isJumping = true;
                    yield return new WaitForSeconds(jumpDelay);
                }
                else
                {
                    yield return new WaitForSeconds(jumpDelay);
                    isJumping = true;
                    animatorManager.SetTrigger("jump");
                }
            }
            else animatorManager.SetTrigger("jump");
            rig.velocity = new Vector2(rig.velocity.x, 0);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            effectsPlayer.PlaySound(soundsDB.Jump);
            isJumping = true;
        }
    }
}