using UnityEngine;

namespace Actors.GenericClasses.Detectors
{
    public class GroundDetector : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float detectorRadius;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool showGizmos;

        [Header("References")]
        [SerializeField] private Transform detectorPosition;

        [Header("Status")]
        [SerializeField] private bool isOnGround;

        public bool IsOnGround { get => isOnGround; private set => isOnGround = value; }

        private void FixedUpdate()
        {
            Collider2D collider = Physics2D.OverlapCircle(detectorPosition.position, detectorRadius, groundLayer);
            if (collider != null) isOnGround = true;
            else isOnGround = false;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.DrawWireSphere(detectorPosition.position, detectorRadius);
        }
    }
}