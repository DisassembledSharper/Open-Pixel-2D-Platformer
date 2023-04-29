using Managers;
using ScriptableObjects.Data;
using TMPro;
using UnityEngine;

namespace Data
{
    public class ScoresGetter : MonoBehaviour
    {
        [Header("References")]
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI starTwo;
        [SerializeField] private TextMeshProUGUI starThree;
        [SerializeField] private TextMeshProUGUI highScoreText;
        private LevelDataObject currentLevelData;

        private void OnEnable()
        {
            currentLevelData = LevelDataManager.Instance.CurrentLevelData;
            if (starTwo != null) starTwo.text = "Score: " + currentLevelData.MinScoreToTwoStars;
            if (starThree != null) starThree.text = "Score: " + currentLevelData.MinScoreToThreeStars;

            if (highScoreText != null)
            {
                int currentScore = 0;
                if (ScoreManager.Instance != null) currentScore = ScoreManager.Instance.Score;
                int levelDataHighScore = currentLevelData.LevelData.highScore;
                int highScore = currentScore > levelDataHighScore ? currentScore : levelDataHighScore;
                int zerosToPut = 7 - highScore.ToString().Length;
                string newScoreText = "";
                for (int i = 0; i < zerosToPut; i++)
                {
                    newScoreText += "0";
                }
                newScoreText += highScore.ToString();
                highScoreText.text = newScoreText;
            }
        }
    }
}