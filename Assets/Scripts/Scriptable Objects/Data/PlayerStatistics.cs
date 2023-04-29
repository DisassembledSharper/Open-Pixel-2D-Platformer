using Data;
using UnityEngine;

namespace ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "Player Statistics", menuName = "ScriptableObjects/Player Statistics")]
    public class PlayerStatistics : ScriptableObject
    {
        [SerializeField] private FruitsCount fruitsCount = new();
        [SerializeField] private EnemiesCount enemiesCount = new();
        [SerializeField] private FruitsCount fruitsBalance = new();
        [SerializeField] private bool unlockedNinjaFrog;
        [SerializeField] private bool unlockedPinkMan;
        [SerializeField] private bool unlockedVirtualGuy;

        public FruitsCount FruitsCount { get => fruitsCount; set => fruitsCount = value; }
        public EnemiesCount EnemiesCount { get => enemiesCount; set => enemiesCount = value; }
        public bool UnlockedNinjaFrog { get => unlockedNinjaFrog; set => unlockedNinjaFrog = value; }
        public bool UnlockedPinkMan { get => unlockedPinkMan; set => unlockedPinkMan = value; }
        public bool UnlockedVirtualGuy { get => unlockedVirtualGuy; set => unlockedVirtualGuy = value; }
        public FruitsCount FruitsBalance { get => fruitsBalance; set => fruitsBalance = value; }
    }
}