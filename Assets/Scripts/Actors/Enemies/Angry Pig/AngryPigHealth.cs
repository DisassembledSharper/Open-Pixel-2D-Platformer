using Actors.GenericClasses.Health.HealthSystems;
using UnityEngine;

namespace Actors.Enemies.AngryPig
{
    public class AngryPigHealth : EnemyHealthSystem
    {
        [Header("References")]
        [SerializeField] private AngryPigBehavior pigBehavior;
        [SerializeField] private AngryPigAnimations animations;

        [Header("Status")]
        [SerializeField] private float hitCount;

        public float HitCount { get => hitCount; private set => hitCount = value; }
        public int CurrentHealth { get => currentHealth; private set => currentHealth = value; }

        protected override void OnTakeDamage()
        {
            hitCount++;
            pigBehavior.IsRunning = true;
            animations.SetBool("isRed", currentHealth < 2);
            base.OnTakeDamage();
        }
    }
}