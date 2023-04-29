using ScriptableObjects.Sounds;
using Sound;
using UnityEngine;

namespace Traps
{
    public class Trampoline : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float jumpForce;

        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D playerRig;
        [SerializeField] private SoundEffectsPlayer effectsPlayer;
        [SerializeField] private SoundsDB soundsDB;

        private void LaunchBody(Rigidbody2D rigidbody)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
            effectsPlayer.PlaySound(soundsDB.TrampolineJump);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (playerRig == null) playerRig = collision.GetComponent<Rigidbody2D>();
                LaunchBody(playerRig);
            }
            if (collision.gameObject.layer == 9)
            {
                LaunchBody(collision.GetComponent<Rigidbody2D>());
            }
        }
    }
}