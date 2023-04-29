using Actors.GenericClasses.EnemiesBehaviors;
using Actors.GenericClasses.EnemiesBehaviors.Movement;
using Actors.GenericClasses.SoundManagement;
using System.Collections;
using UnityEngine;

namespace Actors.Enemies.Bat
{
    public class BatBehavior : EnemyAirFollow
    {
        [SerializeField] private GameObject flySoundObject;

        protected override void OnEnterDistanceRadius()
        {
            FreezeMovements(0.583f);
        }

        protected override void OnEnterDistanceRadiusStay()
        {
            animatorManager.SetBool("flying", true);
            flySoundObject.SetActive(true);
        }

        protected override void OnExitDistanceRadiusStay()
        {
            animatorManager.SetBool("flying", false);
            flySoundObject.SetActive(false);
        }
    }
}