using Actors.GenericClasses.AnimationManagement;
using Actors.GenericClasses.EnemiesBehaviors;
using Actors.Player;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using UnityEngine;

public class ChamaleonBehavior : EnemyBehavior
{
    [Header("Config")]
    [SerializeField] private float minFollowDistance;
    [SerializeField] private float minAttackDistance;
    [SerializeField] private float attackInterval;
    [SerializeField] private float hitLength;
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private SoundEffectsPlayer effectsPlayer;
    [SerializeField] private SoundsDB soundsDB;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private Transform playerTransform;
    private PlayerHealth playerHealth;
    [Header("Status")]
    [SerializeField] private bool lookingRight;
    [SerializeField] private bool canAttack;
    [SerializeField] private bool isAttacking;
    [SerializeField] private float directionMultiplier;

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
            if (distance > minAttackDistance)
            {
                if (horizontalDistance > 0)
                {
                    directionMultiplier = 1;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    directionMultiplier = -1;
                    transform.eulerAngles = Vector3.zero;
                }
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
        Vector2 raycastDirection;
        animatorManager.SetTrigger("attack");
        if (transform.eulerAngles.y != 0) raycastDirection = Vector2.right;
        else raycastDirection = Vector2.left;

        yield return new WaitForSeconds(0.4f);
        effectsPlayer.PlaySound(soundsDB.ChameleonAttack);
        RaycastHit2D hit = Physics2D.Raycast(hitPoint.position, raycastDirection, hitLength, playerLayer);

        if (hit.collider != null)
        {
            if (playerHealth == null) playerHealth = hit.collider.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
        }
        yield return new WaitForSeconds(attackInterval);

        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Vector2 raycastDirection;
        if (transform.eulerAngles.y != 0) raycastDirection = Vector2.right;
        else raycastDirection = Vector2.left;
        Gizmos.DrawRay(hitPoint.position, raycastDirection * hitLength);
    }
}
