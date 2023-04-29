using UnityEngine;

namespace Traps
{
    public class FallingPlatform : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float fallDelay;

        [Header("References")]
        [SerializeField] private TargetJoint2D targetJoint;

        private void Fall()
        {
            targetJoint.enabled = false;
            Destroy(gameObject, 2);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Invoke(nameof(Fall), fallDelay);
            }
        }
    }
}