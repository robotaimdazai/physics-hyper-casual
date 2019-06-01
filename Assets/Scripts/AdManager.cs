using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance{get{return instance;}}
    static AdManager instance = null;

    // Start is called before the first frame update
    [SerializeField] bool testMode = true;
    [SerializeField] string appID;
    [SerializeField] string interstitialAdID;

    private InterstitialAd interstitial;
    int adsAfterFailCount = 3;
    private int currentCount = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        #if UNITY_ANDROID
            
        #else
            appID = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appID);

        RequestInterstitial();

    }
    private void RequestInterstitial()
    {
        
        #if UNITY_ANDROID
        #else
        interstitialAdID = "unexpected_platform";
        #endif

        if (testMode)
        {
            interstitialAdID = "ca-app-pub-3940256099942544/1033173712";

        }

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstitialAdID);
        this.interstitial.OnAdOpening += HandleOnAdOpened;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);

    }

    public void ShowAd()
    {
        if(currentCount>=adsAfterFailCount)
        {
            if (this.interstitial.IsLoaded()) 
            {
                this.interstitial.Show();
            }
        }
        else
        {
            currentCount++;
        }
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        currentCount = 0;
    }



    
}
