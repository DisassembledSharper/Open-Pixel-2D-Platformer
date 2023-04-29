using UnityEngine;
using Managers;
using Sound;
using ScriptableObjects.Sounds;

namespace Items.Fruits
{
    public class Fruit : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private FruitsTypes fruitType;

        [Header("References")]
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected SoundEffectsPlayer effectsPlayer;
        [SerializeField] protected SoundsDB soundsDB;
        [SerializeField] private GameObject collectedObject;

        public enum FruitsTypes
        {
            Apple, Banana, Cherry, Kiwi, Melon, Orange, Pineapple, Strawberry
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                FruitsManager.Instance.AddFruitCount(fruitType, 1);
                spriteRenderer.enabled = false;
                collectedObject.SetActive(true);
                effectsPlayer.PlaySound(soundsDB.PickFruit);
                Destroy(gameObject, 0.4f);
            }
        }
    }
}