using Actors.GenericClasses.Health.HealthSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericsClasses.Detectors
{
    public class HitDetector : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float playerLaunchForce;
        private Rigidbody2D playerRig;

        private void LaunchPlayer()
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, 0);
            playerRig.AddForce(Vector2.up * playerLaunchForce, ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (playerRig == null) playerRig = collision.GetComponent<Rigidbody2D>();
                if (playerRig.velocity.y > -0.2f) return;
                transform.parent.GetComponent<HealthSystem>().TakeDamage(1);
                LaunchPlayer();
            }
        }
    }
}