using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class AboutPanelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] contents;
        [SerializeField] private Image[] contentsCircles;
        [SerializeField] private Sprite whiteCircle;
        [SerializeField] private Sprite blackCircle;
        [SerializeField] private int previousContent;
        [SerializeField] private int currentContent;

        private void OnEnable()
        {
            ChangeCirclesColor();
        }

        private void ChangeCirclesColor()
        {
            int i = 0;
            foreach (Image contentCircle in contentsCircles)
            {
                if (i == currentContent) contentCircle.sprite = blackCircle;
                else contentCircle.sprite = whiteCircle;
                i++;
            }
        }

        public void ChangeContent(bool isToLeft)
        {
            previousContent = currentContent;

            if (isToLeft) currentContent--;
            else currentContent++;

            if (currentContent < 0) currentContent = contents.Length - 1;
            else if (currentContent >= contents.Length) currentContent = 0;

            contents[previousContent].SetActive(false);
            contents[currentContent].SetActive(true);
            ChangeCirclesColor();
        }

    }
}