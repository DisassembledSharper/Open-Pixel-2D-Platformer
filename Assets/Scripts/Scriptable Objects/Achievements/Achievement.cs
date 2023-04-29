using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjects.Achievements
{
    [Serializable]
    [CreateAssetMenu(fileName = "Achievement", menuName = "ScriptableObjects/Achievement")]
    public class Achievement : ScriptableObject
    {
        public Sprite icon;
        public Sprite prize;
        public string textKey;
        public int value;
        public bool isUnlocked;
        public UnityEvent OnUnlock;

        public void Unlock()
        {
            isUnlocked = true;
            OnUnlock.Invoke();
        }
    }
}