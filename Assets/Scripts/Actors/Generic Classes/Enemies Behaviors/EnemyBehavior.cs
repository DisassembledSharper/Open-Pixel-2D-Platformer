using System.Collections;
using UnityEngine;

namespace Actors.GenericClasses.EnemiesBehaviors
{
    public class EnemyBehavior : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] protected float speed;

        [Header("References")]
        [SerializeField] protected Rigidbody2D rig;

        [Header("Status")]
        [SerializeField] protected bool canMove  = true;
        [SerializeField] protected float currentSpeed;

        public bool CanMove { get => canMove; set => canMove = value; }
        public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
        public float Speed { get => speed; set => speed = value; }

        private void Awake()
        {
            currentSpeed = speed;
        }

        public void FreezeMovements(float delay)
        {
            StartCoroutine(FreezeMovement(delay));
        }

        protected IEnumerator FreezeMovement(float delay)
        {
            canMove = false;
            currentSpeed = 0;
            if (rig.bodyType == RigidbodyType2D.Dynamic) rig.velocity = Vector2.zero;
            yield return new WaitForSeconds(delay);
            canMove = true;
            currentSpeed = speed;
        }
    }
}