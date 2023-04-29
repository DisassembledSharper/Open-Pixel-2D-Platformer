using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "ScriptableObjects/Player Settings")]
    public class UserSettings : ScriptableObject
    {
        [SerializeField] private float musicVolume;
        [SerializeField] private float effectsVolume;
        [SerializeField] private int targetFramerate;
        [SerializeField] private bool showFps;
        [SerializeField] private bool vSync;
        [SerializeField] private int language;
        [SerializeField] private int control;
        [SerializeField] private Characters character;
        [SerializeField] private RuntimeAnimatorController maskDudeController;
        [SerializeField] private RuntimeAnimatorController pinkManController;
        [SerializeField] private RuntimeAnimatorController ninjaFrogController;
        [SerializeField] private RuntimeAnimatorController virtualGuyController;

        [SerializeField] private bool isMobile;
        [SerializeField] private bool enableLevelPanel;

        public Characters Character { get => character; set => character = value; }
        public bool IsMobile { get => isMobile; set => isMobile = value; }
        public bool EnableLevelPanel { get => enableLevelPanel; set => enableLevelPanel = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public float EffectsVolume { get => effectsVolume; set => effectsVolume = value; }
        public int TargetFramerate { get => targetFramerate; set => targetFramerate = value; }
        public bool ShowFps { get => showFps; set => showFps = value; }
        public bool VSync { get => vSync; set => vSync = value; }
        public int Language { get => language; set => language = value; }
        public int Control { get => control; set => control = value; }

        public enum Characters { MaskDude, PinkMan, NinjaFrog, VirtualGuy }

        public RuntimeAnimatorController GetCharacterAnimatorController()
        {
            switch (character)
            {
                case Characters.MaskDude:
                    return maskDudeController;
                case Characters.PinkMan:
                    return pinkManController;
                case Characters.NinjaFrog:
                    return ninjaFrogController;
                case Characters.VirtualGuy:
                    return virtualGuyController;
                default:
                    return null;
            }
        }
    }
}