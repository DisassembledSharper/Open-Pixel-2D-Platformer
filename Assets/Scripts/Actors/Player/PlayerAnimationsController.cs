using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.Detectors;
using ScriptableObjects;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerAnimationsController : AnimatorManager
    {
        [Header("References")]
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private PlayerJump playerJump;
        [SerializeField] private WallDetector wallDetector;
        [SerializeField] private GroundDetector groundDetector;

        private void Start()
        {
            animator.runtimeAnimatorController = playerSettings.GetCharacterAnimatorController();
        }

        private void Update()
        {
            animator.SetBool("isJumping", playerJump.IsJumping);
            animator.SetBool("canWallJump", playerJump.CanWallJump);
            if (playerMovement.IsWalking && !playerJump.IsJumping) SetInt("state", 1);
            else if (!playerMovement.IsWalking && !playerJump.IsJumping || !playerJump.IsFalling) SetInt("state", 0);
            else if (playerJump.IsJumping && playerJump.IsFalling) SetInt("state", 2);
        }
    }
}