using ScriptableObjects.Achievements;
using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class GameData
    {
        public FruitsCount globalFruitsCount;
        public EnemiesCount globalEnemiesCount;
        public List<LevelData> levelsData;
        public bool unlockedNinjaFrog;
        public bool unlockedPinkMan;
        public bool unlockedVirtualGuy;
        public FruitsCount fruitsBalance;
        public List<bool> allAchievements = new();
    }
    [Serializable]
    public class FruitsCount
    {
        public int totalFruits;
        public int[] fruits = new int[8];
    }
    [Serializable]
    public class EnemiesCount
    {
        public int totalEnemies;
        public int[] enemies = new int[20];
    }
    [Serializable]
    public class LevelData
    {
        public int levelNumber;
        public int playsCount;
        public int starsCount;
        public int highScore;
        public bool isCompleted;
        public EnemiesCount enemiesCount = new();
        public FruitsCount pickedFruitsCount = new();
    }
}