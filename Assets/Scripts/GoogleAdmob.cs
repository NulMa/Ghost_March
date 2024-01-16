using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAdmob : MonoBehaviour{
    public static GoogleAdmob instance;
    private AppOpenAd openAd;


    public void InitAds() {
        string adUnitId;
        //android app open test code : ca-app-pub-3940256099942544/9257395921

        adUnitId = "ca-app-pub-3940256099942544/9257395921";
        AppOpenAd.Load(adUnitId, new AdRequest.Builder().Build(), LoadCallBack);
    }

    public void LoadCallBack(AppOpenAd openAd, LoadAdError loadAdError) {
        if(openAd != null) {
            this.openAd = openAd;
            Debug.Log("Load Comp");
        }
        else {
            Debug.Log(loadAdError.GetMessage());
        }
    }

    public void ShowAds() {
        if (openAd.CanShowAd()) {
            openAd.Show();
        }
        else {
            Debug.Log("Fail to show ADs");
        }
    }


}

