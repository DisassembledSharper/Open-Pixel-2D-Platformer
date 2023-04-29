using Actors.Enemies.Bat;
using Actors.GenericClasses.AnimationManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors.Movement
{
    public class EnemyAirFollow : EnemyBehavior
    {
        [Header("Config")]
        [SerializeField] protected float distanceToFollow;

        [Header("References")]
        [SerializeField] protected GameObject player;
        [SerializeField] protected AnimatorManager animatorManager;

        [Header("Status")]
        [SerializeField] protected float horizontalDistance;
        [SerializeField] protected Vector2 originalPosition;
        [SerializeField] protected bool isFlying;
        [SerializeField] protected bool enteredOnDistanceRadius;
        [SerializeField] protected bool followingPlayer;

        private void Awake()
        {
            currentSpeed = speed;
            originalPosition = transform.position;
            player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (horizontalDistance > 0) transform.eulerAngles = new Vector3(0, 180, 0);
            else transform.eulerAngles = Vector3.zero;

            if (Vector2.Distance(transform.position, player.transform.position) <= distanceToFollow) followingPlayer = true;
            else followingPlayer = false;
        }

        private void FixedUpdate()
        {
            if (followingPlayer)
            {
                FlyToPosition(player.transform.position, 1f);
            }
            else
            {
                FlyToPosition(originalPosition, 0);
            }
        }

        private void FlyToPosition(Vector2 target, float minDistance)
        {
            if (Vector2.Distance(rig.position, target) > minDistance)
            {
                if (!enteredOnDistanceRadius)
                {
                    OnEnterDistanceRadius();
                }
                enteredOnDistanceRadius = true;
                rig.MovePosition(Vector2.MoveTowards(rig.position, target, currentSpeed * Time.deltaTime));
                horizontalDistance = target.x - rig.position.x;
                OnEnterDistanceRadiusStay();
            }
            else
            {
                if (followingPlayer) return;
                enteredOnDistanceRadius = false;
                OnExitDistanceRadiusStay();
            }
        }

        protected virtual void OnEnterDistanceRadius() { }
        protected virtual void OnEnterDistanceRadiusStay() { }
        protected virtual void OnExitDistanceRadiusStay() { }
    }
}