using System.Collections;
using UnityEngine;

namespace Items.Fruits
{
    public class PhysicFruit : Fruit
    {
        [Header("Physic Fruit")]
        [Header("Config")]
        [SerializeField] private float desappearTime;
        [SerializeField] private float animationDelay;

        [Header("Status")]
        [SerializeField] private bool isDesappearing;
        [SerializeField] private float desappearCounter;

        private void Start()
        {
            desappearCounter = desappearTime;
        }

        private void Update()
        {
            desappearCounter -= Time.deltaTime;

            if (desappearCounter <= 2)
            {
                if (!isDesappearing)
                {
                    isDesappearing = true;
                    StartCoroutine(DesappearAnimation());
                }
            }
        }

        private IEnumerator DesappearAnimation()
        {
            Color color = spriteRenderer.color;
            while (desappearCounter > 0)
            {
                color.a = 0;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(animationDelay);

                color.a = 1;
                spriteRenderer.color = color;
                yield return new WaitForSeconds(animationDelay);
            }

            Destroy(gameObject);
        }
    }
}