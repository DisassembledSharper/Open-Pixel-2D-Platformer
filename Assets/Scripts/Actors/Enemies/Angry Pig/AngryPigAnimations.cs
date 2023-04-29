using Actors.GenericClasses.AnimationManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.Enemies.AngryPig
{
    public class AngryPigAnimations : AnimatorManager
    {
        [Header("----------")]
        [Header("References")]
        [SerializeField] private AngryPigBehavior behavior;
        [SerializeField] private AngryPigHealth health;
        private void Update()
        {
            if (behavior.CanMove)
            {
                if (behavior.IsRunning)
                {
                    if (health.CurrentHealth >= 2) SetInt("status", 2);
                    else SetInt("status", 3);
                }
                else SetInt("status", 1);
            }
            else SetInt("status", 0);
        }
    }
}