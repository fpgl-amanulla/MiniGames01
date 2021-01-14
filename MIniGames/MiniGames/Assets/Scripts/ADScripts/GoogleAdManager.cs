using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;

public class GoogleAdManager : MonoBehaviour
{

    public static GoogleAdManager _instance;
    private RewardedAd rewardedAd1;
    public bool userEarnedReward1 = false;
    private InterstitialAd interstitial;

    private UnityAction<bool> adViewCallBack;

    public static GoogleAdManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GoogleAdManager>();
                if (_instance == null)
                {
                    GameObject g = new GameObject("GoogleAdManager");
                    _instance = g.AddComponent<GoogleAdManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        DontDestroyOnLoad(this);

        MobileAds.Initialize(initStatus => { });

        CreateAndLoadRewardedAd1();
        CreateAndLoadInterstitial();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void CreateAndLoadInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";//Test Ad
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        this.interstitial.OnAdClosed += HandleOnAdClosedInterstitial;
    }

    public void HandleOnAdClosedInterstitial(object sender, EventArgs args)
    {
        CreateAndLoadInterstitial();
    }

    public void ShowInterestitialAD()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            CreateAndLoadInterstitial();
        }
    }


    public void ShowRewaredAD(UnityAction<bool> _adViewCallBack = null)
    {
        adViewCallBack = _adViewCallBack;
        if(this.rewardedAd1.IsLoaded())
        {
            this.rewardedAd1.Show();
        }
        else
        {
            CreateAndLoadRewardedAd1();
        }
    }




    public void CreateAndLoadRewardedAd1()
    {
        //key reward
#if UNITY_ANDROID
        string rewardedadUnitId1 = "ca-app-pub-3940256099942544/5224354917"; //Test AD
#elif UNITY_IPHONE
        string rewardedadUnitId1 = "ca-app-pub-3940256099942544/4411468910";
#else
        string rewardedadUnitId1 = "unexpected_platform";
#endif

        this.rewardedAd1 = new RewardedAd(rewardedadUnitId1);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        this.rewardedAd1.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.rewardedAd1.OnAdLoaded += HandleRewardedAd1Loaded;
        // Called when an ad request failed to load.
        this.rewardedAd1.OnAdFailedToLoad += HandleRewardedAd1FailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd1.OnAdOpening += HandleRewardedAd1Opening;
        // Called when an ad request failed to show.
        this.rewardedAd1.OnAdFailedToShow += HandleRewardedAd1FailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd1.OnUserEarnedReward += HandleUserEarnedReward1;
        // Called when the ad is closed.
        this.rewardedAd1.OnAdClosed += HandleRewardedAd1Closed;
    }

    public void HandleRewardedAd1Loaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAd1FailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAd1Opening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAd1FailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.Message);
    }

    public void HandleRewardedAd1Closed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        if (userEarnedReward1 == true)
        {
            adViewCallBack?.Invoke(true);
        }
        else
        {
            adViewCallBack?.Invoke(false);
        }

        this.CreateAndLoadRewardedAd1();

    }

    public void HandleUserEarnedReward1(object sender, Reward args)
    {
        userEarnedReward1 = true;
    }

}
