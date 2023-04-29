using Actors.Player;
using System.Collections;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Traps
{
    public class Saw : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float sawSpeed;
        [SerializeField] private float sawMaxDistance;
        [SerializeField] private float changeDirectionDelay;
        [SerializeField] private float gizmosOffset;
        [SerializeField] private float sawRadius;
        [SerializeField] private Axes axis;
        [SerializeField] private Directions startDirection;
        [SerializeField] private LayerMask sawLayer;

        [Header("References")]
        [SerializeField] private Animator sawAnimator;

        [Header("Status")]
        [SerializeField] private Directions currentDirection;
        [SerializeField] private Vector2 startPosition;
        [SerializeField] private bool isOn;
        [SerializeField] private bool isChanging;

        public enum Directions { Right, Left, Up, Down}
        public enum Axes { Horizontal, Vertical }
        public bool IsOn { get => isOn; set => isOn = value; }

        private void Start()
        {
            startPosition = transform.position;
            currentDirection = startDirection;
        }

        private void Update()
        {
            sawAnimator.SetBool("isOn", IsOn);

            if (isOn)
            {
                if (axis == Axes.Horizontal)
                {
                    if (currentDirection == Directions.Right)
                    {
                        transform.position += sawSpeed * Time.deltaTime * Vector3.right;
                    }
                    else if (currentDirection == Directions.Left)
                    {
                        transform.position += sawSpeed * Time.deltaTime * Vector3.left;
                    }
                }
                else
                {
                    if (currentDirection == Directions.Up)
                    {
                        transform.position += sawSpeed * Time.deltaTime * Vector3.up;
                    }
                    else if (currentDirection == Directions.Down)
                    {
                        transform.position += sawSpeed * Time.deltaTime * Vector3.down;
                    }
                }
            }
            
        }

        private void FixedUpdate()
        {
            Vector2 origin = startPosition;
            Vector2 direction;

            if (axis == Axes.Horizontal)
            {
                if (startDirection == Directions.Right)
                {
                    origin.x += sawMaxDistance;
                    direction = Vector2.left;
                }
                else
                {
                    origin.x -= sawMaxDistance;
                    direction = Vector2.right;
                }
                
            }
            else
            {
                if (startDirection == Directions.Up)
                {
                    origin.y += sawMaxDistance;
                    direction = Vector2.down;
                }
                else
                {
                    origin.y -= sawMaxDistance;
                    direction = Vector2.up;
                }
            }

            RaycastHit2D sawLine = Physics2D.Raycast(origin, direction, sawMaxDistance, sawLayer);
            if (sawLine.collider is null)
            {
                ChangeDirection();
            }
        }

        private void ChangeDirection()
        {
            if (isChanging) return;

            isChanging = true;

            if (axis == Axes.Horizontal)
            {
                if (currentDirection == Directions.Right)
                {
                    currentDirection = Directions.Left;
                }
                else if (currentDirection == Directions.Left)
                {
                    currentDirection = Directions.Right;
                }
            }
            else
            {
                if (currentDirection == Directions.Up)
                {
                    currentDirection = Directions.Down;
                }
                else if (currentDirection == Directions.Down)
                {
                    currentDirection = Directions.Up;
                }
            }
            StartCoroutine(WaitChange(changeDirectionDelay));
        }

        private IEnumerator WaitChange(float delay)
        {
            yield return new WaitForSeconds(delay);
            isChanging = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerHealth>().TakeDamage(1);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 position = transform.position;
            Vector3 currentPosition = transform.position;
            Vector3 finalPosition = transform.position;
            if (axis == Axes.Horizontal)
            {
                if (startDirection == Directions.Right)
                {
                    position.x += sawMaxDistance;
                    currentPosition.x -= gizmosOffset;
                    finalPosition.x = position.x + gizmosOffset;
                    
                }
                else
                {
                    position.x -= sawMaxDistance;
                    currentPosition.x += gizmosOffset;
                    finalPosition.x = position.x - gizmosOffset;
                }
            }
            else
            {
                if (startDirection == Directions.Up)
                {
                    position.y += sawMaxDistance;
                    currentPosition.y -= gizmosOffset;
                    finalPosition.y = position.y + gizmosOffset;
                }
                else
                {
                    position.y -= sawMaxDistance;
                    currentPosition.y += gizmosOffset;
                    finalPosition.y = position.y - gizmosOffset;
                }
            }
            Gizmos.DrawWireSphere(currentPosition, sawRadius);
            Gizmos.DrawWireSphere(finalPosition, sawRadius);
            Gizmos.DrawLine(transform.position, position);
        }
    }
}