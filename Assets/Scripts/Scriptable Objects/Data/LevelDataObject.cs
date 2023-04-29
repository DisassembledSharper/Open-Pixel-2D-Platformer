using Data;
using UnityEngine;

namespace ScriptableObjects.Data
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Level Data")]
    public class LevelDataObject : ScriptableObject
    {
        [SerializeField] private int minScoreToTwoStars;
        [SerializeField] private int minScoreToThreeStars;
        [SerializeField] private LevelData levelData = new();
        
        public LevelData LevelData { get => levelData; set => levelData = value; }
        public int MinScoreToTwoStars { get => minScoreToTwoStars; private set => minScoreToTwoStars = value; }
        public int MinScoreToThreeStars { get => minScoreToThreeStars; private set => minScoreToThreeStars = value; }

        public void CloneLevelData(LevelDataObject levelDataObject)
        {
            minScoreToTwoStars = levelDataObject.MinScoreToTwoStars;
            minScoreToThreeStars = levelDataObject.MinScoreToThreeStars;
            levelData = levelDataObject.LevelData;
        }
    }
}