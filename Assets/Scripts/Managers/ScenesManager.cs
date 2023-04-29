using Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] private Animator transitionAnimator;

        public static ScenesManager Instance { get; private set; }

        private void Awake() => Instance = this;

        public void ReloadScene()
        {
            StartCoroutine(ReLoadScene(SceneManager.GetActiveScene().buildIndex, 1));
        }

        public void LoadScene(int buildIndex)
        {
            PlayerPrefs.SetInt("sceneToLoad", buildIndex + 1);
            StartCoroutine(LoadScene(1f));
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private IEnumerator LoadScene(float delay)
        {
            transitionAnimator.SetTrigger("cover");
            yield return new WaitForSecondsRealtime(delay);
            //if (PlayerPrefs.GetInt("sceneToLoad") == 7) Destroy(LevelMusic.Instance.gameObject);
            SceneManager.LoadScene(1);
        }
        private IEnumerator ReLoadScene(int buildIndex, float delay)
        {
            transitionAnimator.SetTrigger("cover");
            yield return new WaitForSecondsRealtime(delay);
            SceneManager.LoadSceneAsync(buildIndex);
        }
    }
}