using Items.Fruits;
using ScriptableObjects.Sounds;
using Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Box : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Fruit[] fruits;
        [SerializeField] private float minForce;
        [SerializeField] private float maxForce;
        [SerializeField] private float playerLaunchForce;
        [SerializeField] private int hitsToBreak;
        [SerializeField] private float hitLength;
        [SerializeField] private LayerMask playerLayer;

        [Header("References")]
        [SerializeField] private List<Rigidbody2D> fruitsRigs;
        [SerializeField] private Transform topHitPoint;
        [SerializeField] private Transform bottomHitPoint;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ParticleSystem boxBreaking;
        [SerializeField] private Animator animator;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;

        [Header("Status")]
        [SerializeField] private bool showGizmos;
        [SerializeField] private bool breaked;

        private void Start()
        {
            foreach (Fruit fruit in fruits)
            {
                GameObject instantiedFruit;
                instantiedFruit = Instantiate(fruit.gameObject, transform.position, fruit.transform.rotation);
                instantiedFruit.SetActive(false);
                fruitsRigs.Add(instantiedFruit.GetComponent<Rigidbody2D>());
            }
        }

        private void FixedUpdate()
        {
            if (!breaked)
            {
                RaycastHit2D topHit = Physics2D.Raycast(topHitPoint.position, Vector2.right, hitLength, playerLayer);
                RaycastHit2D bottomHit = Physics2D.Raycast(bottomHitPoint.position, Vector2.right, hitLength, playerLayer);

                if (topHit.collider != null || bottomHit.collider != null)
                {
                    Rigidbody2D playerRig;
                    
                    if (topHit.collider != null)
                    {
                        playerRig = topHit.transform.GetComponent<Rigidbody2D>();
                        playerRig.velocity = new Vector2(playerRig.velocity.x, 0);
                        playerRig.AddForce(Vector2.up * playerLaunchForce, ForceMode2D.Impulse);
                    }
                    else if (bottomHit.collider != null)
                    {
                        playerRig = bottomHit.transform.GetComponent<Rigidbody2D>();
                        playerRig.velocity = new Vector2(playerRig.velocity.x, 0);
                        playerRig.AddForce(Vector2.down * playerLaunchForce, ForceMode2D.Impulse);
                    }
                    hitsToBreak--;
                    animator.SetTrigger("hit");

                    if (hitsToBreak <= 0)
                    {
                        OnBreakBox();
                    }
                    else effectsPlayer.PlaySound(soundsDB.HitBox);
                }
            }
        }

        private void LaunchFruits()
        {
            Vector2 direction = Vector2.up;
            foreach (Rigidbody2D fruitRig in fruitsRigs)
            {
                fruitRig.gameObject.SetActive(true);
                direction.x = Random.Range(-1f, 2f);
                fruitRig.AddForce(direction * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            }
        }

        private void OnBreakBox()
        {
            breaked = true;
            boxCollider.enabled = false;
            spriteRenderer.enabled = false;
            boxBreaking.Play();
            effectsPlayer.PlaySound(soundsDB.BreakBox);
            LaunchFruits();
            Destroy(gameObject, 5);
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;

            Gizmos.DrawRay(topHitPoint.position, Vector2.right * hitLength);
            Gizmos.DrawRay(bottomHitPoint.position, Vector2.right * hitLength);
        }
    }
}