using Actors.GenericClasses.EnemiesBehaviors.Movement;
using System.Collections;
using UnityEngine;

namespace Actors.Enemies.AngryPig
{
    public class AngryPigBehavior : EnemyGroundMovement
    {
        [Header("Status")]
        [SerializeField] private bool isRunning;

        public bool IsRunning { get => isRunning; set => isRunning = value; }

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
            if (canMove)
            {
                if (isRunning) currentSpeed = speed * 2;
                else currentSpeed = speed;
            }
        }
    }
}