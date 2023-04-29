using Actors.GenericClasses.AnimationManagement;
using Actors.GenericsClasses.Bullet;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors.Mechanics
{
    public class EnemyShooter : EnemyBehavior
    {
        [Header("Config")]
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float torqueForce;
        [SerializeField] private float minDistanceToShoot;
        [SerializeField] private float shootInterval;
        [SerializeField] private float shootAnimationDelay;
        [SerializeField] private bool isFixedShooter;

        [Header("References")]
        [SerializeField] protected SoundEffectsPlayer effectsPlayer;
        [SerializeField] protected SoundsDB soundsDB;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject player;
        [SerializeField] protected AnimatorManager animatorManager;
        [SerializeField] private EnemyBullet[] bullets;

        [Header("Status")]
        [SerializeField] private float shootCounter;
        [SerializeField] private bool isRight;
        [SerializeField] private bool canShoot;
        [SerializeField] private bool freezeCounter;
        [SerializeField] private Vector2 directionToShoot;

        public bool FreezeCounter { get => freezeCounter; set => freezeCounter = value; }

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            shootCounter = shootInterval;
        }

        private void Update()
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            float horizontalDistance = transform.position.x - player.transform.position.x;

            if (isFixedShooter)
            {
                if (Mathf.Abs(horizontalDistance) <= minDistanceToShoot) canShoot = true;
                else canShoot = false;
            }
            else
            {
                if (isRight) transform.eulerAngles = new Vector3(0, 180, 0);
                else transform.eulerAngles = Vector3.zero;

                if (transform.eulerAngles.y == 180)
                {
                    if (horizontalDistance < 0) canShoot = true;
                    else canShoot = false;
                    directionToShoot = Vector2.right;
                }
                else
                {
                    if (horizontalDistance > 0) canShoot = true;
                    else canShoot = false;
                    directionToShoot = Vector2.left;
                }
            }

            if (distance <= minDistanceToShoot)
            {
                if (!freezeCounter) shootCounter += Time.deltaTime;

                if (canShoot)
                {
                    if (shootCounter >= shootInterval)
                    {
                        StartCoroutine(Shoot());
                    }
                }
            }
            else shootCounter = shootInterval;
        }

        protected IEnumerator Shoot()
        {
            shootCounter = 0;
            animatorManager.SetTrigger("attack");
            yield return new WaitForSeconds(shootAnimationDelay);

            foreach (EnemyBullet bullet in bullets)
            {
                if (!bullet.gameObject.activeSelf)
                {
                    bullet.transform.position = shootPoint.position;
                    bullet.Shoot(Random.Range(minForce, maxForce), torqueForce, directionToShoot);
                    effectsPlayer.PlaySound(soundsDB.EnemyShoot);
                    break;
                }
            }
        }
    }
}