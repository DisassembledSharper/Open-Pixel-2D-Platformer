using UnityEngine;

namespace Actors.GenericClasses.Health.HealthSystems
{
    public class HealthSystem : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] protected int startHealth = 3;

        [Header("Status")]
        [SerializeField] protected int currentHealth;
        [SerializeField] protected bool canTakeDamage = true;
        [SerializeField] protected bool isDead = false;

        public void TakeDamage(int damageValue)
        {
            if (!canTakeDamage || isDead) return;
            currentHealth -= damageValue;
            OnTakeDamage();
            if (currentHealth <= 0)
            {
                isDead = true;
                OnDie();
            }
        }

        protected virtual void OnTakeDamage() { }
        protected virtual void OnDie() { }
    }
}