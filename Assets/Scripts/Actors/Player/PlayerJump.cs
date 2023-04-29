using Actors.GenericClasses.Detectors;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerJump : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float holdingJumpForce;
        [SerializeField] private float wallJumpForce;
        [SerializeField] private float slidingSpeed;
        [SerializeField] private float holdTime;

        [Header("References")]
        [SerializeField] private Rigidbody2D rig;
        [SerializeField] private GroundDetector groundDetector;
        [SerializeField] private WallDetector wallDetector;
        [SerializeField] private PlayerAnimationsController animationsController;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerInputGetter inputGetter;
        [SerializeField] private PlayerEffects effects;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;

        [Header("Status")]
        [SerializeField] private float holdCounter;
        [SerializeField] private float horizontalInput;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool jumpRequest;
        [SerializeField] private bool wallJumpRequest;
        [SerializeField] private bool doubleJumpRequest;
        [SerializeField] private bool canDoubleJump;
        [SerializeField] private bool canWallJump;
        [SerializeField] private bool isDoubleJumping;
        [SerializeField] private bool isHolding;
        [SerializeField] private bool isFalling;
        [SerializeField] private bool isOnGround;
        [SerializeField] private bool freezeInput;

        public bool IsJumping { get => isJumping; set => isJumping = value; }
        public bool IsOnGround { get => isOnGround; private set => isOnGround = value; }
        public bool IsFalling { get => isFalling; private set => isFalling = value; }
        public bool IsDoubleJumping { get => isDoubleJumping; private set => isDoubleJumping = value; }
        public bool FreezeInput { get => freezeInput; set => freezeInput = value; }
        public bool CanWallJump { get => canWallJump; private set => canWallJump = value; }

        private void Update()
        {
            if (!freezeInput)
            {
                horizontalInput = inputGetter.GetHorizontalAxis();
                inputGetter.GetJumpButtonDown();
                if (inputGetter.GetJumpButtonDown() && isOnGround) jumpRequest = true;
                if (inputGetter.GetJumpButtonDown() && canWallJump) wallJumpRequest = true;
                if (inputGetter.GetJumpButtonDown() && canDoubleJump) doubleJumpRequest = true;
                if (inputGetter.GetJumpButtonUp()) isHolding = false;

                if (wallDetector.OnWall && horizontalInput != 0 && !groundDetector.IsOnGround && rig.velocity.y <= 0)
                {
                    canWallJump = true;
                    IsJumping = false;
                }
                else canWallJump = false;

            }
            else
            {
                horizontalInput = 0;
                jumpRequest = false;
                doubleJumpRequest = false;
            }
        }

        private void FixedUpdate()
        {
            isOnGround = groundDetector.IsOnGround;
            if (jumpRequest)
            {
                holdCounter = 0;
                isHolding = true;
                Impulse();
                jumpRequest = false;
                IsJumping = true;
                canDoubleJump = true;
                animationsController.SetTrigger("jump");
                effectsPlayer.PlaySound(soundsDB.Jump);
            }
            else if (doubleJumpRequest)
            {
                effects.PlayDoubleJumpDust();
                Impulse();
                isHolding = true;
                holdCounter = 0;
                doubleJumpRequest = false;
                canDoubleJump = false;
                isDoubleJumping = true;
                animationsController.SetTrigger("doubleJump");
                effectsPlayer.PlaySound(soundsDB.Jump);
            }
            if (isHolding && IsJumping)
            {
                holdCounter += Time.deltaTime;
                if (holdCounter >= holdTime)
                {
                    isHolding = false;
                    holdCounter = 0;
                }
                rig.AddForce(Vector2.up * holdingJumpForce);
            }
            isFalling = rig.velocity.y <= 0 && isJumping;

            if (isOnGround && rig.velocity.y <= 1.5f)
            {
                isJumping = false;
                canDoubleJump = false;
                isDoubleJumping = false;
            }

            if (canWallJump)
            {
                canDoubleJump = false;
                rig.velocity = new Vector2(rig.velocity.x, slidingSpeed);
                effects.PlaySlidingWallDust();

                if (wallJumpRequest)
                {
                    effects.StopSlidingWallDust();
                    rig.velocity = new Vector3(0, 0);
                    rig.AddForce(Vector2.up * wallJumpForce, ForceMode2D.Impulse);
                    if (horizontalInput > 0)
                    {
                        rig.AddForce(Vector2.left * wallJumpForce, ForceMode2D.Impulse);
                    }
                    else if (horizontalInput < 0)
                    {
                        rig.AddForce(Vector2.right * wallJumpForce, ForceMode2D.Impulse);
                    }
                    effectsPlayer.PlaySound(soundsDB.Jump);
                    canWallJump = false;
                    wallJumpRequest = false;
                    animationsController.SetTrigger("jump");
                    IsJumping = true;
                }
            }
            else effects.StopSlidingWallDust();
        }
        private void Impulse()
        {
            rig.velocity = new Vector3(rig.velocity.x, 0);
            rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}