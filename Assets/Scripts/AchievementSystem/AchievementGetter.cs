using ScriptableObjects.Achievements;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AchievementSystem
{
    public class AchievementGetter : MonoBehaviour
    {
        [SerializeField] private Image achievementIcon;
        [SerializeField] private GameObject lockedImage;
        [SerializeField] private Achievement achievement;

        private void Awake()
        {
            achievementIcon.sprite = achievement.icon;
        }

        private void OnEnable()
        {
            if (achievement.isUnlocked) lockedImage.SetActive(false);
            else lockedImage.SetActive(true);
        }
    }
}