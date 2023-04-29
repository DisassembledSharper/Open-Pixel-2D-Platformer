using ScriptableObjects.Data;
using UnityEngine;

namespace Managers
{
    public class StarsManager : MonoBehaviour
    {
        [Header("Config")]
        [Tooltip("Mark as true if the level data is not getting by reference")]
        [SerializeField] private bool getDataInDataManager = true;

        [Header("References")]
        [SerializeField] private Animator[] starsAnimators;
        [SerializeField] private LevelDataObject currentLevelData;

        private void OnEnable()
        {
            if (getDataInDataManager) currentLevelData = LevelDataManager.Instance.CurrentLevelData;
            if (currentLevelData.LevelData.isCompleted)
            {
                starsAnimators[0].SetBool("active", true);
            }

            if (currentLevelData.LevelData.starsCount > 1)
            {
                starsAnimators[1].SetBool("active", true);
            }
            else if (ScoreManager.Instance != null)
            {
                if (ScoreManager.Instance.Score >= currentLevelData.MinScoreToTwoStars)
                {
                    starsAnimators[1].SetBool("active", true);
                }  
            }

            if (currentLevelData.LevelData.starsCount > 2)
            {
                starsAnimators[2].SetBool("active", true);
            }
            else if (ScoreManager.Instance != null)
            {
                if (ScoreManager.Instance.Score >= currentLevelData.MinScoreToThreeStars)
                {
                    starsAnimators[2].SetBool("active", true);
                }
            }
        }
    }
}