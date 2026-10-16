using System;
using System.Collections.Generic;
using UnityEngine;

// ponytail: stub for the IronSource ad SDK. The original AndroidAgent called
// com.ironsource.unity.androidbridge.AndroidBridge via AndroidJavaClass, which no longer exists
// in the APK. Ads are disabled: every method is a no-op that logs, signatures preserved so the
// IronSource facade (IronSource.Agent) keeps working. Availability checks return false so the
// game never offers fake ad rewards.
public class AndroidAgent : IronSourceIAgent
{
	public AndroidAgent()
	{
		Debug.Log("[STUB] AndroidAgent ctr (IronSource disabled)");
	}

	private static void StubCall(string method)
	{
		Debug.Log("[STUB] IronSource.Agent." + method);
	}

	public void reportAppStarted()
	{
		StubCall("reportAppStarted");
	}

	public void onApplicationPause(bool pause)
	{
		StubCall("onApplicationPause");
	}

	public void setAge(int age)
	{
		StubCall("setAge");
	}

	public void setGender(string gender)
	{
		StubCall("setGender");
	}

	public void setMediationSegment(string segment)
	{
		StubCall("setMediationSegment");
	}

	public string getAdvertiserId()
	{
		StubCall("getAdvertiserId");
		return string.Empty;
	}

	public void validateIntegration()
	{
		StubCall("validateIntegration");
	}

	public void shouldTrackNetworkState(bool track)
	{
		StubCall("shouldTrackNetworkState");
	}

	public bool setDynamicUserId(string dynamicUserId)
	{
		StubCall("setDynamicUserId");
		return false;
	}

	public void setAdaptersDebug(bool enabled)
	{
		StubCall("setAdaptersDebug");
	}

	public void setUserId(string userId)
	{
		StubCall("setUserId");
	}

	public void init(string appKey)
	{
		StubCall("init");
	}

	public void init(string appKey, params string[] adUnits)
	{
		StubCall("init");
	}

	public void initISDemandOnly(string appKey, params string[] adUnits)
	{
		StubCall("initISDemandOnly");
	}

	public void showRewardedVideo()
	{
		StubCall("showRewardedVideo");
	}

	public void showRewardedVideo(string placementName)
	{
		StubCall("showRewardedVideo");
	}

	public bool isRewardedVideoAvailable()
	{
		StubCall("isRewardedVideoAvailable");
		return false;
	}

	public bool isRewardedVideoPlacementCapped(string placementName)
	{
		StubCall("isRewardedVideoPlacementCapped");
		return false;
	}

	public IronSourcePlacement getPlacementInfo(string placementName)
	{
		StubCall("getPlacementInfo");
		return null;
	}

	public void setRewardedVideoServerParams(Dictionary<string, string> parameters)
	{
		StubCall("setRewardedVideoServerParams");
	}

	public void clearRewardedVideoServerParams()
	{
		StubCall("clearRewardedVideoServerParams");
	}

	public void showISDemandOnlyRewardedVideo(string instanceId)
	{
		StubCall("showISDemandOnlyRewardedVideo");
	}

	public void showISDemandOnlyRewardedVideo(string instanceId, string placementName)
	{
		StubCall("showISDemandOnlyRewardedVideo");
	}

	public bool isISDemandOnlyRewardedVideoAvailable(string instanceId)
	{
		StubCall("isISDemandOnlyRewardedVideoAvailable");
		return false;
	}

	public void loadInterstitial()
	{
		StubCall("loadInterstitial");
	}

	public void showInterstitial()
	{
		StubCall("showInterstitial");
	}

	public void showInterstitial(string placementName)
	{
		StubCall("showInterstitial");
	}

	public bool isInterstitialReady()
	{
		StubCall("isInterstitialReady");
		return false;
	}

	public bool isInterstitialPlacementCapped(string placementName)
	{
		StubCall("isInterstitialPlacementCapped");
		return false;
	}

	public void loadISDemandOnlyInterstitial(string instanceId)
	{
		StubCall("loadISDemandOnlyInterstitial");
	}

	public void showISDemandOnlyInterstitial(string instanceId)
	{
		StubCall("showISDemandOnlyInterstitial");
	}

	public void showISDemandOnlyInterstitial(string instanceId, string placementName)
	{
		StubCall("showISDemandOnlyInterstitial");
	}

	public bool isISDemandOnlyInterstitialReady(string instanceId)
	{
		StubCall("isISDemandOnlyInterstitialReady");
		return false;
	}

	public void showOfferwall()
	{
		StubCall("showOfferwall");
	}

	public void showOfferwall(string placementName)
	{
		StubCall("showOfferwall");
	}

	public void getOfferwallCredits()
	{
		StubCall("getOfferwallCredits");
	}

	public bool isOfferwallAvailable()
	{
		StubCall("isOfferwallAvailable");
		return false;
	}

	public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position)
	{
		StubCall("loadBanner");
	}

	public void loadBanner(IronSourceBannerSize size, IronSourceBannerPosition position, string placementName)
	{
		StubCall("loadBanner");
	}

	public void destroyBanner()
	{
		StubCall("destroyBanner");
	}

	public void displayBanner()
	{
		StubCall("displayBanner");
	}

	public void hideBanner()
	{
		StubCall("hideBanner");
	}

	public bool isBannerPlacementCapped(string placementName)
	{
		StubCall("isBannerPlacementCapped");
		return false;
	}

	public void setSegment(IronSourceSegment segment)
	{
		StubCall("setSegment");
	}
}
