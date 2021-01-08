using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdManager : MonoBehaviour
{

    public static GoogleAdManager Instance;

    private InterstitialAd interstitialAd;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            RequestInterstitial();
        });
    }


    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitialAd = new InterstitialAd(adUnitId);


        this.interstitialAd.OnAdClosed += HandleOnAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitialAd.LoadAd(request);
    }

    private void HandleOnAdClosed(object sender, EventArgs e)
    {
        //interstitialAd.Destroy();
        //Debug.Log("Destroy called............");
    }

    public void ShowInterestitialAD()
    {
        if (interstitialAd.IsLoaded())
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Ad not ready yet......");
        }
    }

}
