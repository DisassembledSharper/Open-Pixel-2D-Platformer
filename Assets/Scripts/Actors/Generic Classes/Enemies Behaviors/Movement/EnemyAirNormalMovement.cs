using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors.Movement
{
    public class EnemyAirNormalMovement : EnemyBehavior
    {
        [Header("Config")]
        [SerializeField] protected float acceleration;
        [SerializeField] protected float flySpeed;
        [SerializeField] protected float flyAmplitude;
        [SerializeField] protected float maxDistance;
        [SerializeField] protected bool showGizmos;

        [Header("Status")]
        [SerializeField] protected Vector2 startPosition;
        [SerializeField] protected float directionMultiplier;

        private void Start()
        {
            startPosition = transform.position;
            directionMultiplier = -1;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float flyVelocity = Mathf.Sin(Time.time * flySpeed) * flyAmplitude;

            rig.velocity = new Vector2(rig.velocity.x, flyVelocity);

            if (Mathf.Abs(rig.velocity.x) <= speed)
            {
                rig.AddForce(new Vector2(acceleration * directionMultiplier, 0));
            }
            float distance = transform.position.x - startPosition.x;

            if (Mathf.Abs(distance) >= maxDistance)
            {
                if (distance < 0 && directionMultiplier == -1)
                {
                    directionMultiplier = 1;
                    rig.AddForce(Vector2.right * (Mathf.Abs(rig.velocity.x) / 2.5f), ForceMode2D.Impulse);
                }
                else if (distance > 0 && directionMultiplier == 1)
                {
                    directionMultiplier = -1;
                    rig.AddForce(Vector2.left * (Mathf.Abs(rig.velocity.x) / 2.5f), ForceMode2D.Impulse);
                }

            }

            if (rig.velocity.x > 0) transform.eulerAngles = new Vector3(0, 180, 0);
            else transform.eulerAngles = Vector3.zero;
        }
        private void OnDrawGizmosSelected()
        {
            if (!showGizmos) return;
            Vector3 startLine = startPosition;
            Vector3 endLine = startPosition;
            startLine.x -= maxDistance;
            endLine.x += maxDistance;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startLine, endLine);
        }
    }
}