using GoogleMobileAds.Api;
using System;
using UnityEngine;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance;

    public bool IsInterstitialAdAvailable=>interstitialAd.IsLoaded();
    public bool IsRewardedAdAvailable=>rewardedAd.IsLoaded();

    private const string UNSUPPORTED_PLATFORM = "unexpected_platform";

    [HideInInspector] public UnityEvent WatchedAd;
    [HideInInspector] public UnityEvent DidntWatchAd;

    private string rewardingAdId;
    private string interstitialAdId;

    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd;

    // Ad-mob is multi-threaded so we need to call functions on Unity's main thread
    private bool hasWatchedAd;
    private bool hasCanceledAd;

    private string message = string.Empty;
    private Action callBack;
    private int matchesCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init(null);
    }

    private void Init(Action _calBack)
    {
        SetAdIds();

        if (rewardingAdId == UNSUPPORTED_PLATFORM || interstitialAdId == UNSUPPORTED_PLATFORM)
        {
            FinishInit(_calBack);
            return;
        }

        MobileAds.Initialize(_result =>
        {
            Debug.Log(_result);
            RequestAds();
            SetTriggers();
            FinishInit(_calBack);
        });
    }

    private void FinishInit(Action _calBack)
    {
        callBack = _calBack;
    }

    private void SetAdIds()
    {
        if (RuntimePlatform.WindowsEditor == Application.platform)
        {
            rewardingAdId = "ca-app-pub-3940256099942544/5224354917";
            interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
        }
        else if (RuntimePlatform.OSXEditor == Application.platform)
        {
            rewardingAdId = "ca-app-pub-3940256099942544/1712485313";
            interstitialAdId = "ca-app-pub-3940256099942544/4411468910";
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            rewardingAdId = "ca-app-pub-3940256099942544/1712485313"; // TODO: Change me for real Apple ID
            interstitialAdId = "ca-app-pub-3940256099942544/4411468910"; // TODO: Change me for real Apple ID
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            rewardingAdId = "ca-app-pub-3940256099942544/5224354917";// TODO: Change me for real Android ID
            interstitialAdId = "ca-app-pub-3940256099942544/1033173712";// TODO: Change me for real Android ID
        }
        else
        {
            rewardingAdId = UNSUPPORTED_PLATFORM;
            interstitialAdId = UNSUPPORTED_PLATFORM;
        }
    }

    private void SetTriggers()
    {
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleInterstitialAdOpening;
        interstitialAd.OnAdFailedToShow += HandleInterstitialAdFailedToShow;
        interstitialAd.OnAdClosed += HandleInterstitialAdClosed;
    }

    private void Update()
    {
        if (hasWatchedAd)
        {
            hasWatchedAd = false;
            WatchedAd?.Invoke();
            WatchedAd?.RemoveAllListeners();
            RequestRewardedAd();
        }

        if (hasCanceledAd)
        {
            hasCanceledAd = false;
            DidntWatchAd?.Invoke();
            WatchedAd?.RemoveAllListeners();
            DidntWatchAd?.RemoveAllListeners();
            RequestAds();
        }

        if (message != string.Empty)
        {
            UIManager.Instance.OkDialog.Show(message);
            message = string.Empty;
        }

        if (callBack != null)
        {
            callBack?.Invoke();
            callBack = null;
        }
    }

    public void PlayRewardedAd()
    {
        if (rewardingAdId == UNSUPPORTED_PLATFORM)
        {
            message = "Ads don't support this platform";
            return;
        }

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            if (!SceneManager.IsDataCollectorScene)
            {
                message = "Ad is not ready yet, try again later";
            }
        }
    }

    public void PlayInterstitialAd()
    {
        if (interstitialAdId == UNSUPPORTED_PLATFORM)
        {
            message = "Ads don't support this platform";
            return;
        }

        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            if (!SceneManager.IsDataCollectorScene)
            {
                message = "Ad is not ready yet, try again later";
            }
        }
    }

    private void HandleRewardedAdLoaded(object _sender, EventArgs _args)
    {
        // Handle rewarded ad loaded event
    }

    private void HandleRewardedAdFailedToLoad(object _sender, AdFailedToLoadEventArgs _args)
    {
        if (!SceneManager.IsDataCollectorScene)
        {
            message = "Ad failed to load, please try again later.";
        }
    }

    private void HandleRewardedAdOpening(object _sender, EventArgs _args)
    {
        // Handle rewarded ad opening event
    }

    private void HandleRewardedAdFailedToShow(object _sender, AdErrorEventArgs _args)
    {
        if (!SceneManager.IsDataCollectorScene)
        {
            message = "Ad failed to show, please try again later.";
        }
    }

    private void HandleRewardedAdClosed(object _sender, EventArgs _args)
    {
        hasCanceledAd = true;
    }

    private void HandleUserEarnedReward(object _sender, Reward _args)
    {
        hasWatchedAd = true;
    }

    private void HandleInterstitialAdLoaded(object _sender, EventArgs _args)
    {
        // Handle interstitial ad loaded event
    }

    private void HandleInterstitialAdFailedToLoad(object _sender, AdFailedToLoadEventArgs _args)
    {
        if (!SceneManager.IsDataCollectorScene)
        {
            message = "Ad failed to load, please try again later.";
        }
    }

    private void HandleInterstitialAdOpening(object _sender, EventArgs _args)
    {
        // Handle interstitial ad opening event
    }

    private void HandleInterstitialAdFailedToShow(object _sender, AdErrorEventArgs _args)
    {
        if (!SceneManager.IsDataCollectorScene)
        {
            message = "Ad failed to show, please try again later.";
        }
    }

    private void HandleInterstitialAdClosed(object _sender, EventArgs _args)
    {
        hasWatchedAd = true;
    }

    private void RequestRewardedAd()
    {
        rewardedAd ??= new RewardedAd(rewardingAdId);

        AdRequest _request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(_request);
    }

    private void RequestInterstitialAd()
    {
        interstitialAd ??= new InterstitialAd(interstitialAdId);

        AdRequest _request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(_request);
    }

    private void RequestAds()
    {
        RequestRewardedAd();
        RequestInterstitialAd();
    }
}
