using GoogleMobileAds.Api;
using System.Collections;
using UnityEngine;

namespace Managers
{
    public class AdsManager : MonoBehaviour
    {
        [SerializeField] private string interstitialId = "ca-app-pub-3940256099942544/1033173712";
        [SerializeField] private string rewardedId = "ca-app-pub-3940256099942544/5224354917";
        [SerializeField] private bool showAds = true;

        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;

        private bool isLoading;
        private int loadError;

        public static AdsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (showAds) MobileAds.Initialize(InitializationStatus => { });
        }
        #region Interstitial Methods
        private void RequestInterstitial()
        {
            if (interstitialAd != null)
            {
                interstitialAd.Destroy();
                interstitialAd = null;
            }
            AdRequest adRequest = new AdRequest.Builder().Build();

            InterstitialAd.Load(interstitialId, adRequest,
                (InterstitialAd ad, LoadAdError error) =>
                {
                    if (error != null || ad == null)
                    {
                        Debug.LogError("interstitial ad failed to load an ad " +
                                       "with error : " + error);
                        return;
                    }
                    interstitialAd = ad;
                });
            
        }
        private void ShowInterstitial()
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                interstitialAd.Show();
            }
            else if (interstitialAd == null)
            {
                RequestInterstitial();
            }
        }

        public void TryShowInterstitial()
        {
            if (!showAds) return;
            int counter = PlayerPrefs.GetInt("adCounter", 0) + 1;
            if (counter >= 3)
            {
                PlayerPrefs.SetInt("adCounter", 0);
                ShowInterstitial();
            }
            else
            {
                PlayerPrefs.SetInt("adCounter", counter);
                if (interstitialAd == null || !interstitialAd.CanShowAd()) RequestInterstitial();
            }
        }
        #endregion

        #region Rewarded Methods
        private void RequestRewarded()
        {
            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }
            loadError = 0;
            AdRequest adRequest = new AdRequest.Builder().Build();
            RewardedAd.Load(rewardedId, adRequest,
                (RewardedAd ad, LoadAdError error) =>
                {
                    ad.OnAdFullScreenContentClosed += () =>
                    {
                        ScreenUIManager.Instance.SetReviveButtonsInteractableState(true);
                    };
                    // if error is not null, the load request failed.
                    if (error != null || ad == null)
                    {
                        Debug.LogError("Rewarded ad failed to load an ad " +
                                       "with error : " + error);
                        loadError = 1;
                        isLoading = false;
                        ScreenUIManager.Instance.SetReviveButtonsInteractableState(true);
                        return;
                    }

                    rewardedAd = ad;
                });
            
        }

        public void ShowRewarded()
        {

            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                rewardedAd.Show((Reward reward) =>
                {
                    // TODO: Reward the user.
                    GameManager.Instance.RevivePlayerByAd();
                });
            }
        }
        public void CallRewarded()
        {
            if (!showAds)
            {
                GameManager.Instance.RevivePlayerByAd();
                return;
            }
            isLoading = true;
            RequestRewarded();
            StartCoroutine(WaitLoadRewarded());
        }
        private IEnumerator WaitLoadRewarded()
        {
            while (isLoading)
            {
                if (rewardedAd != null) break;
                yield return null;
            }
            isLoading = false;
            ScreenUIManager.Instance.SetReviveButtonsInteractableState(true);
            if (loadError == 1) yield break;

            ShowRewarded();
        }
        #endregion
    }
}