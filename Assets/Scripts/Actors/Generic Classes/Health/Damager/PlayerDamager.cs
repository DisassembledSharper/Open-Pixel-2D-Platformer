using Actors.Player;
using UnityEngine;

namespace Actors.GenericClasses.Health.Damager
{
    public class PlayerDamager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private bool isTrigger;

        [Header("References")]
        [SerializeField] private PlayerHealth playerHealth;

        [Header("Status")]
        [SerializeField] private bool touchingPlayer;

        private void Update()
        {
            if (touchingPlayer)
            {
                playerHealth.TakeDamage(1);
            }
        }

        private void GetPlayerHealth(Collider2D collision)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.GetComponent<PlayerHealth>();
            }
        }

        private void GetPlayerHealth(Collision2D collision)
        {
            if (playerHealth == null)
            {
                playerHealth = collision.transform.GetComponent<PlayerHealth>();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!isTrigger) return;
                GetPlayerHealth(collision);
                touchingPlayer = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!isTrigger) return;
                GetPlayerHealth(collision);
                touchingPlayer = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                if (isTrigger) return;
                GetPlayerHealth(collision);
                touchingPlayer = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                if (isTrigger) return;
                GetPlayerHealth(collision);
                touchingPlayer = false;
            }
        }
    }
}