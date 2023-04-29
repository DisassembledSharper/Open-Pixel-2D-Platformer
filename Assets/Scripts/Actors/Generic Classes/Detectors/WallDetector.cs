using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericClasses.Detectors
{
    public class WallDetector : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float detectorRadius;
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] private bool showGizmos;

        [Header("Status")]
        [SerializeField] private bool onWall;

        public bool OnWall { get => onWall; private set => onWall = value; }

        private void FixedUpdate()
        {
            Collider2D collider;
            collider = Physics2D.OverlapCircle(transform.position, detectorRadius, wallLayer);

            if (collider != null) onWall = true;
            else onWall = false;
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.DrawWireSphere(transform.position, detectorRadius);
        }
    }
}