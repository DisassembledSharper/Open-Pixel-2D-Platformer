using Actors.GenericClasses.Health.Damager;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericsClasses.Bullet
{
    public class EnemyBullet : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] protected string thisBulletTag;

        [Header("References")]
        [SerializeField] protected SoundEffectsPlayer effectsPlayer;
        [SerializeField] protected SoundsDB soundsDB;
        [SerializeField] protected Rigidbody2D rig;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected ParticleSystem bulletPieces;
        [SerializeField] protected PlayerDamager damager;

        [Header("Status")]
        [SerializeField] private bool isTouched;

        public void Shoot(float force, float torque, Vector2 direction)
        {
            rig.freezeRotation = false;
            gameObject.SetActive(true);
            rig.velocity = Vector2.zero;
            rig.AddForce(force * direction, ForceMode2D.Impulse);
            rig.AddTorque(torque);
        }
        public void Shoot(float force, int rotation, Vector2 direction)
        {
            rig.freezeRotation = false;
            gameObject.SetActive(true);
            transform.eulerAngles = new Vector3(0, rotation, 0);
            rig.velocity = Vector2.zero;
            rig.AddForce(force * direction, ForceMode2D.Impulse);
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(thisBulletTag) || collision.CompareTag("Bullet Pieces")) return;
            if (collision.gameObject.layer == 10) return;
            if (isTouched) return;
            isTouched = true;
            spriteRenderer.enabled = false;
            rig.velocity = Vector2.zero;
            bulletPieces.Play();
            effectsPlayer.PlaySound(soundsDB.BreakBullet);
            if (!collision.CompareTag("Player")) damager.enabled = false;
            
            StartCoroutine(RestoreState());
        }

        private IEnumerator RestoreState()
        {
            yield return new WaitForSeconds(1);
            isTouched = false;
            damager.enabled = true;
            rig.freezeRotation = true;
            transform.eulerAngles = Vector3.zero;
            bulletPieces.Stop();
            spriteRenderer.enabled = true;
            gameObject.SetActive(false);
        }
    }
}