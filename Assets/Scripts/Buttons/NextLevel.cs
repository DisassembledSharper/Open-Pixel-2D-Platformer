using Managers;
using ScriptableObjects.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class NextLevel : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;

        private void Start()
        {
            nextLevelButton.interactable = LevelDataManager.Instance.CurrentLevelData.LevelData.isCompleted;
        }
    }
}