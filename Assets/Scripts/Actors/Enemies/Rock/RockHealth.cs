using Actors.GenericClasses.Health.HealthSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.Enemies.Rock
{
    public class RockHealth : EnemyHealthSystem
    {
        [SerializeField] private int hitCount;

        protected override void OnTakeDamage()
        {
            hitCount++;
            if (hitCount < 1) behavior.Speed *= 2;
            else if (hitCount < 2) behavior.Speed *= 3;
            animatorManager.SetInt("hits", hitCount);
            base.OnTakeDamage();
        }
    }
}