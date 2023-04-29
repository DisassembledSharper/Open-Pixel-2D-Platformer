using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.EnemiesBehaviors;
using Actors.GenericClasses.SoundManagement;
using Actors.GenericsClasses.Detectors;
using Managers;
using ScriptableObjects.Sounds;
using Sound;
using UnityEngine;

namespace Actors.GenericClasses.Health.HealthSystems
{
    public class EnemyHealthSystem : HealthSystem
    {
        [Header("Config")]
        [SerializeField] protected int enemyId;
        [SerializeField] protected float freezeDelay;

        [Header("References")]
        [SerializeField] protected EnemyBehavior behavior;
        [SerializeField] protected SoundEffectsPlayer effectsPlayer;
        [SerializeField] protected SoundsDB soundsDB;
        [SerializeField] protected MonoBehaviour[] scriptsToDisable;
        [SerializeField] protected AnimatorManager animatorManager;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Rigidbody2D rig;
        [SerializeField] protected CapsuleCollider2D capsuleCollider;
        [SerializeField] protected HitDetector hitDetector;
        [SerializeField] protected GameObject desappearObject;

        private void Start()
        {
            currentHealth = startHealth;
        }

        protected override void OnTakeDamage()
        {
            animatorManager.SetTrigger("hit");
            effectsPlayer.PlaySound(soundsDB.Hit);
            behavior.FreezeMovements(freezeDelay);
        }

        protected override void OnDie()
        {
            ScoreManager.Instance.AddScore(10, enemyId);
            foreach (MonoBehaviour script in scriptsToDisable)
            {
                script.enabled = false;
            }
            effectsPlayer.PlaySound(soundsDB.Desappear);
            behavior.enabled = false;
            spriteRenderer.enabled = false;
            hitDetector.gameObject.SetActive(false);
            rig.gravityScale = 0;
            capsuleCollider.enabled = false;
            desappearObject.SetActive(true);
            Destroy(gameObject, 0.333f);
        }
    }
}