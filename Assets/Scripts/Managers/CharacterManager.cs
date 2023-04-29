using ScriptableObjects;
using ScriptableObjects.Data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] private UserSettings.Characters character;
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private UserSettings playerSettings;
        private Image characterImage;

        private void Awake()
        {
            characterImage = GetComponent<Image>();
        }

        private void OnEnable()
        {
            bool unlockedCharacter = false;
            switch (character)
            {
                case UserSettings.Characters.PinkMan:
                    unlockedCharacter = playerStatistics.UnlockedPinkMan;
                    break;
                case UserSettings.Characters.NinjaFrog:
                    unlockedCharacter = playerStatistics.UnlockedNinjaFrog;
                    break;
                case UserSettings.Characters.VirtualGuy:
                    unlockedCharacter = playerStatistics.UnlockedVirtualGuy;
                    break;
            }

            if (unlockedCharacter) characterImage.color = Color.white;
            else characterImage.color = Color.black;
        }
    }
}