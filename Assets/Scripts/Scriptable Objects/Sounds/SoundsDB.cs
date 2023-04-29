using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Sounds
{
    /// <summary>
    /// Stores all game sounds.
    /// </summary>
    [CreateAssetMenu(fileName = "Sounds DB", menuName = "ScriptableObjects/Sounds DB")]   
    public class SoundsDB : ScriptableObject
    {
        [Header("Musics")]
        [SerializeField] private AudioClip menu;
        [SerializeField] private AudioClip[] levelThemes;
        [SerializeField] private AudioClip[] darkLevelThemes;
        [Space]
        [Header("Sound Effects")]
        [Space]
        [Header("UI")]
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip achievement;
        [Space]
        [Header("Actors")]
        [SerializeField] private AudioClip appear;
        [SerializeField] private AudioClip desappear;
        [SerializeField] private AudioClip jump;
        [SerializeField] private AudioClip hit;
        [SerializeField] private AudioClip enemyHit;
        [SerializeField] private AudioClip flyEnemyWings;
        [SerializeField] private AudioClip falling;
        [SerializeField] private AudioClip enemyShoot;
        [SerializeField] private AudioClip chameleonAttack;

        [Header("Items")]
        [SerializeField] private AudioClip pickFruit;
        [SerializeField] private AudioClip trampolineJump;
        [SerializeField] private AudioClip hitBox;
        [SerializeField] private AudioClip breakBox;
        [SerializeField] private AudioClip breakBullet;
        [SerializeField] private AudioClip checkPoint;
        [SerializeField] private AudioClip finalPointPressed;
        [SerializeField] private AudioClip finalPointReleased;

        public AudioClip Click { get => click; private set => click = value; }
        public AudioClip Menu { get => menu; private set => menu = value; }
        public AudioClip[] LevelThemes { get => levelThemes; private set => levelThemes = value; }
        public AudioClip[] DarkLevelThemes { get => darkLevelThemes; private set => darkLevelThemes = value; }
        public AudioClip Jump { get => jump; private set => jump = value; }
        public AudioClip Hit { get => hit; private set => hit = value; }
        public AudioClip PickFruit { get => pickFruit; private set => pickFruit = value; }
        public AudioClip Appear { get => appear; private set => appear = value; }
        public AudioClip Desappear { get => desappear; private set => desappear = value; }
        public AudioClip HitBox { get => hitBox; private set => hitBox = value; }
        public AudioClip BreakBox { get => breakBox; private set => breakBox = value; }
        public AudioClip EnemyHit { get => enemyHit; private set => enemyHit = value; }
        public AudioClip FlyEnemyWings { get => flyEnemyWings; private set => flyEnemyWings = value; }
        public AudioClip Falling { get => falling; private set => falling = value; }
        public AudioClip TrampolineJump { get => trampolineJump; private set => trampolineJump = value; }
        public AudioClip EnemyShoot { get => enemyShoot; private set => enemyShoot = value; }
        public AudioClip ChameleonAttack { get => chameleonAttack; private set => chameleonAttack = value; }
        public AudioClip BreakBullet { get => breakBullet; private set => breakBullet = value; }
        public AudioClip CheckPoint { get => checkPoint; private set => checkPoint = value; }
        public AudioClip FinalPointPressed { get => finalPointPressed; private set => finalPointPressed = value; }
        public AudioClip FinalPointReleased { get => finalPointReleased; private set => finalPointReleased = value; }
        public AudioClip Achievement { get => achievement; private set => achievement = value; }
    }
}