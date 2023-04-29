using Actors.GenericClasses.EnemiesBehaviors.Mechanics;
using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.Health.HealthSystems
{
    public class ShooterEnemyHealthSystem : EnemyHealthSystem
    {
        [Header("References")]
        [SerializeField] private EnemyShooter shooterBehavior;

        protected override void OnTakeDamage()
        {
            base.OnTakeDamage();
            StartCoroutine(FreezeCounter());
        }

        private IEnumerator FreezeCounter()
        {
            shooterBehavior.FreezeCounter = true;
            yield return new WaitForSeconds(0.333f);
            shooterBehavior.FreezeCounter = false;
        }
    }
}