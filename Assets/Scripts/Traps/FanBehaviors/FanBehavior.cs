using System.Collections;
using UnityEngine;

namespace Traps.FanBehaviors
{
    public class FanBehavior : MonoBehaviour
    {
        [SerializeField] private float fanForce;
        [SerializeField] private Animator fanAnimator;
        [SerializeField] private GameObject airParticles;
        [SerializeField] private bool isOn;
        private Rigidbody2D playerRig;

        private void FixedUpdate()
        {
            fanAnimator.SetBool("isOn", isOn);
            airParticles.SetActive(isOn);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (playerRig == null) playerRig = collision.GetComponent<Rigidbody2D>();

                playerRig.AddForce(fanForce * Vector2.up, ForceMode2D.Force);
            }
        }
    }
}