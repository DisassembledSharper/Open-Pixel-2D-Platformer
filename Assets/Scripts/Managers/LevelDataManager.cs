using ScriptableObjects.Data;
using UnityEngine;

namespace Managers
{
    public class LevelDataManager : MonoBehaviour
    {
        [SerializeField] private LevelDataObject currentLevelData;

        public LevelDataObject CurrentLevelData { get => currentLevelData; private set => currentLevelData = value; }
        public static LevelDataManager Instance { get; private set; }

        private void Awake() => Instance = this;

        private void OnDestroy()
        {
            if (Instance != null) Instance = null;
        }
    }
}