using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actors.GenericClasses.AnimationManagement
{
    public class AnimatorManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected Animator animator;

        public void SetBool(string boolName, bool value)
        {
            animator.SetBool(boolName, value);
        }

        public void SetFloat(string floatName, float value)
        {
            animator.SetFloat(floatName, value);
        }

        public void SetInt(string intName, int value)
        {
            animator.SetInteger(intName, value);
        }

        public void SetTrigger(string triggerName)
        {
            animator.SetTrigger(triggerName);
        }
    }
}