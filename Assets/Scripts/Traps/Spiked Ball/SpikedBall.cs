using UnityEngine;

namespace Traps
{
    public class SpikedBall : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float pushForce;
        [SerializeField] private float pushDelay;

        [Header("References")]
        [SerializeField] private Rigidbody2D rig;

        [Header("Status")]
        [SerializeField] private float pushCounter;

        private void Start()
        {
            pushCounter = pushDelay;
        }

        private void Update()
        {
            pushCounter += Time.deltaTime;

            if (pushCounter >= pushDelay)
            {
                pushCounter = 0;
                PushBall();
            }
        }
        private void PushBall()
        {
            float direction;
            if (rig.velocity.x > 0) direction = 1;
            else direction = -1;

            rig.AddForce(new Vector2(direction * pushForce, 0), ForceMode2D.Impulse);
        }
    }
}