using UnityEngine;

namespace Actors.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float deceleration;

        [Header("References")]
        [SerializeField] private PlayerInputGetter inputGetter;
        [SerializeField] private PlayerJump jump;
        [SerializeField] private PlayerEffects effects;
        [SerializeField] private Rigidbody2D rig;

        [Header("Status")]
        [SerializeField] private float horizontalInput;
        [SerializeField] private float lastDirection;
        [SerializeField] private bool canMove = true;
        [SerializeField] private bool freezeInput;
        [SerializeField] private bool isWalking;

        public bool IsWalking { get => isWalking; private set => isWalking = value; }
        public float HorizontalInput { get => horizontalInput; private set => horizontalInput = value; }
        public bool FreezeInput { get => freezeInput; set => freezeInput = value; }
        public bool CanMove { get => canMove; set => canMove = value; }

        private void Update()
        {
            if (!freezeInput)
            {
                horizontalInput = inputGetter.GetHorizontalAxis();
            }
            else horizontalInput = 0;
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                float absVelocity = Mathf.Abs(rig.velocity.x);
                if (rig.velocity.x > 0) lastDirection = 1;
                else if (rig.velocity.x < 0) lastDirection = -1;
                if (horizontalInput != 0)
                {
                    IsWalking = true;

                    if (horizontalInput > 0)
                    {
                        if (rig.velocity.x <= maxSpeed)
                        {
                            rig.AddForce(Vector2.right * acceleration);
                        }
                    }
                    else
                    {
                        if (rig.velocity.x >= -maxSpeed)
                        {
                            rig.AddForce(Vector2.left * acceleration);
                        }
                    }
                }
                else
                {
                    if (absVelocity > 0.5f)
                    {
                        rig.AddForce(new Vector2(deceleration * -lastDirection, 0));
                    }
                    else
                    {
                        rig.velocity = new Vector2(0, rig.velocity.y);
                    }
                    IsWalking = false;
                }
            }
            else isWalking = false;

            if (horizontalInput > 0f && transform.eulerAngles.y != 0) Flip(true);
            else if (horizontalInput < 0f && transform.eulerAngles.y != 180) Flip(false);
        }

        private void Flip(bool right)
        {
            if (right) transform.eulerAngles = Vector2.zero;
            else transform.eulerAngles = new Vector2(0, 180);

            if (Mathf.Abs(rig.velocity.x) >= 1 && !jump.IsJumping) effects.PlayGroundSlideDust();
        }
    }
}