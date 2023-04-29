using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        [SerializeField] private Image loadingFill;
        private AsyncOperation loadOperation;
        private void Start()
        {
            int sceneToLoad = PlayerPrefs.GetInt("sceneToLoad");
            loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);
            loadOperation.allowSceneActivation = false;
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            while (!loadOperation.isDone) 
            {
                loadingFill.fillAmount = Mathf.Clamp01(loadOperation.progress / .9f);
                if (loadOperation.progress >= 0.9f)
                {
                    loadOperation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}