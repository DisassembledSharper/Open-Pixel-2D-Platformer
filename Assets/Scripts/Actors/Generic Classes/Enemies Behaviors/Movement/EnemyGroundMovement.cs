using Actors.GenericClasses.AnimationManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors.Movement
{
    public class EnemyGroundMovement : EnemyBehavior
    {
        [Header("Enemy Type Config")]
        [SerializeField] protected WalkTypes walkType;

        [Header("Directions Config")]
        [SerializeField] protected bool manageAnimations;
        [SerializeField] protected float wallDetectorRadius;
        [SerializeField] protected float terrainDetectorRadius;
        [SerializeField] protected bool canFall;
        [SerializeField] protected LayerMask wallLayers;
        [SerializeField] protected LayerMask terrainLayer;

        [Header("Follow Player Config")]
        [SerializeField] protected float minFollowDistance;
        [SerializeField] protected float stopDistance;

        [Header("References")]
        [SerializeField] protected AnimatorManager animatorManager;
        [SerializeField] protected Transform wallDetectorPoint;
        [SerializeField] protected Transform terrainDetectorPoint;
        protected Transform playerTransform;

        [Header("Status")]
        [SerializeField] protected int directionMultiplier;
        [SerializeField] protected bool walkingToRight;

        [Header("Gizmos")]
        [SerializeField] protected bool showGizmos;

        public event Action OnCollideWall;
        public enum WalkTypes { WalkToDirections, FollowPlayer }

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected void FixedUpdate()
        {
            switch (walkType)
            {
                case WalkTypes.WalkToDirections:
                    NormalMove();
                    CheckCollisions();
                    break;
                case WalkTypes.FollowPlayer:
                    FollowMove();
                    break;
            }
        }

        private void FollowMove()
        {
            float horizontalDistance = playerTransform.position.x - transform.position.x;
            float distance = Vector2.Distance(playerTransform.position, transform.position);

            if (distance <= minFollowDistance)
            {
                if (Mathf.Abs(horizontalDistance) > stopDistance)
                {
                    if (horizontalDistance > 0)
                    {
                        directionMultiplier = 1;
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    else
                    {
                        directionMultiplier = -1;
                        transform.eulerAngles = Vector3.zero;
                    }
                    if (manageAnimations) animatorManager.SetInt("state", 1);
                }
                else
                {
                    directionMultiplier = 0;
                    if (manageAnimations) animatorManager.SetInt("state", 0);
                }
            }
            else
            {
                directionMultiplier = 0;
                if (manageAnimations) animatorManager.SetInt("state", 0);
            }
            rig.velocity = new Vector2(directionMultiplier * currentSpeed, rig.velocity.y);
        }

        private void NormalMove()
        {
            if (canMove)
            {
                Vector2 direction = new Vector2(0, rig.velocity.y);

                if (walkingToRight) direction.x = currentSpeed;
                else direction.x = -currentSpeed;

                rig.velocity = direction;
                if (manageAnimations) animatorManager.SetInt("state", 1);
            }
            else
            {
                if (manageAnimations) animatorManager.SetInt("state", 0);
            }
        }
        private void CheckCollisions()
        {
            Collider2D wallDetector = Physics2D.OverlapCircle(wallDetectorPoint.position, wallDetectorRadius, wallLayers);
            if (wallDetector is not null)
            {
                ChangeDirection();
                OnCollideWall?.Invoke();
            }

            if (!canFall)
            {
                Collider2D terrainDetector = Physics2D.OverlapCircle(terrainDetectorPoint.position, terrainDetectorRadius, terrainLayer);
                if (terrainDetector is null)
                {
                    ChangeDirection();
                }
            }
        }
        private void ChangeDirection()
        {
            walkingToRight = !walkingToRight;

            if (walkingToRight)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                transform.eulerAngles = Vector2.zero;
            }
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            Gizmos.DrawWireSphere(wallDetectorPoint.position, wallDetectorRadius);
            Gizmos.DrawWireSphere(terrainDetectorPoint.position, terrainDetectorRadius);
        }
    }
}