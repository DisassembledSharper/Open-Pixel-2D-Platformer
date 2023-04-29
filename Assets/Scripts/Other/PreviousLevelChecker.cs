using ScriptableObjects.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Other
{
    public class PreviousLevelChecker : MonoBehaviour
    {
        [SerializeField] private Button thisLevelButton;
        [SerializeField] private LevelDataObject previousLevelDataObject;

        private void OnEnable()
        {
            thisLevelButton.interactable = previousLevelDataObject.LevelData.isCompleted;
        }
    }
}