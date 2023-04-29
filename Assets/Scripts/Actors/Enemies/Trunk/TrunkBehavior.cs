using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.EnemiesBehaviors;
using Actors.GenericsClasses.Bullet;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.Enemies.Trunk
{
    public class TrunkBehavior : EnemyBehavior
    {
        [Header("Config")]
        [SerializeField] private float minFollowDistance;
        [SerializeField] private float minAttackDistance;
        [SerializeField] private float shootInterval;
        [SerializeField] private float shootAnimationDelay;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float hitLength;
        [SerializeField] private LayerMask playerLayer;

        [Header("References")]
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;
        [SerializeField] private Transform hitPoint;
        [SerializeField] private AnimatorManager animatorManager;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private EnemyBullet[] bullets;

        [Header("Status")]
        [SerializeField] private bool lookingRight;
        [SerializeField] private bool canAttack;
        [SerializeField] private bool isAttacking;
        [SerializeField] private float directionMultiplier;
        [SerializeField] private float shootCounter;
        [SerializeField] private Vector3 directionToShoot;

        private void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            float horizontalDistance = playerTransform.position.x - transform.position.x;
            float distance = Vector2.Distance(playerTransform.position, transform.position);

            if (distance <= minFollowDistance)
            {
                if (horizontalDistance > 0)
                {
                    directionMultiplier = 1;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    directionToShoot.x = 1;
                }
                else
                {
                    directionMultiplier = -1;
                    directionToShoot.x = -1;
                    transform.eulerAngles = Vector3.zero;
                }
                if (distance > minAttackDistance)
                {
                    canAttack = false;
                    animatorManager.SetInt("state", 1);
                }
                else if (Mathf.Abs(horizontalDistance) <= minAttackDistance)
                {
                    directionMultiplier = 0;
                    animatorManager.SetInt("state", 0);
                    canAttack = true;
                }
            }
            else
            {
                directionMultiplier = 0;
                animatorManager.SetInt("state", 0);
                canAttack = false;
            }

            if (!canAttack) currentSpeed = speed;
            else currentSpeed = 0;
            if (!isAttacking && canAttack) StartCoroutine(Attack());
            rig.velocity = new Vector2(directionMultiplier * currentSpeed, rig.velocity.y);
        }

        private IEnumerator Attack()
        {
            if (isAttacking) yield break;
            isAttacking = true;
            
            animatorManager.SetTrigger("attack");
            yield return new WaitForSeconds(shootAnimationDelay);

            foreach (EnemyBullet bullet in bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = shootPoint.position;
                    bullet.Shoot(Random.Range(minForce, maxForce), (int) transform.eulerAngles.y, directionToShoot);
                    effectsPlayer.PlaySound(soundsDB.EnemyShoot);
                    break;
                }
            }
            yield return new WaitForSeconds(shootInterval);
            isAttacking = false;
        }
    }
}