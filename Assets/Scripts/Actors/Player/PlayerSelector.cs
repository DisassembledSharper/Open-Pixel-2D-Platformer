using Managers;
using ScriptableObjects;
using ScriptableObjects.Data;
using Sound;
using System;
using System.Collections;
using UnityEngine;

namespace Actors.Player
{
    public class PlayerSelector : MonoBehaviour
    {
        [SerializeField] private UISoundsManager uiSounds;
        [SerializeField] private UserSettings playerSettings;
        [SerializeField] private PlayerStatistics playerStatistics;
        [SerializeField] private Animator maskDude;
        [SerializeField] private Animator pinkMan;
        [SerializeField] private Animator ninjaFrog;
        [SerializeField] private Animator virtualGuy;
        private bool enabling;

        private void OnEnable()
        {
            enabling = true;
            SelectCharacter((int)playerSettings.Character);
            enabling = false;
        }

        public void SelectCharacter(int character)
        {
            Animator selectedCharacter;
            switch (character)
            {
                case 1:
                    selectedCharacter = pinkMan;
                    if (!playerStatistics.UnlockedPinkMan) return;
                    break;
                case 2:
                    selectedCharacter = ninjaFrog;
                    if (!playerStatistics.UnlockedNinjaFrog) return;
                    break;
                case 3:
                    selectedCharacter = virtualGuy;
                    if (!playerStatistics.UnlockedVirtualGuy) return;
                    break;
                default:
                    selectedCharacter = maskDude;
                    break;
            }
            
            selectedCharacter.SetBool("selected", true);
            Animator[] otherCharacters = { maskDude, pinkMan, ninjaFrog, virtualGuy};

            foreach (Animator characterAnimator in otherCharacters)
            {
                if (characterAnimator == selectedCharacter) continue;
                characterAnimator.SetBool("selected", false);
            }
            SettingsManager.Instance.SetCharacter(character);
            if (!enabling) uiSounds.PlayClick();
        }
    }
}