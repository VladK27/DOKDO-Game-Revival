using System.Collections.Generic;
using UnityEngine;

// ponytail: stub for the AppsFlyer attribution SDK. The original static fields called
// new AndroidJavaClass("com.appsflyer.*") at type load, which throws TypeInitializationException
// on device (Java classes absent from the APK). Analytics/attribution is disabled; every method
// is a no-op that logs, public signatures preserved.
public class AppsFlyer : MonoBehaviour
{
	private static void Stub(string method)
	{
		Debug.Log("[STUB] AppsFlyer." + method);
	}

	public static void trackEvent(string eventName, string eventValue)
	{
		Stub("trackEvent");
	}

	public static void setCurrencyCode(string currencyCode)
	{
		Stub("setCurrencyCode");
	}

	public static void setCustomerUserID(string customerUserID)
	{
		Stub("setCustomerUserID");
	}

	public static void loadConversionData(string callbackObject)
	{
		Stub("loadConversionData");
	}

	[System.Obsolete("Use loadConversionData(string callbackObject)")]
	public static void loadConversionData(string callbackObject, string callbackMethod, string callbackFailedMethod)
	{
		Stub("loadConversionData");
	}

	public static void setCollectIMEI(bool shouldCollect)
	{
		Stub("setCollectIMEI");
	}

	public static void setCollectAndroidID(bool shouldCollect)
	{
		Stub("setCollectAndroidID");
	}

	public static void init(string key, string callbackObject)
	{
		Stub("init");
	}

	public static void init(string key)
	{
		Stub("init");
	}

	public static void setAppsFlyerKey(string key)
	{
		Stub("setAppsFlyerKey");
	}

	public static void trackAppLaunch()
	{
		Stub("trackAppLaunch");
	}

	public static void setAppID(string packageName)
	{
		Stub("setAppID");
	}

	public static void createValidateInAppListener(string aObject, string callbackMethod, string callbackFailedMethod)
	{
		Stub("createValidateInAppListener");
	}

	public static void validateReceipt(string publicKey, string purchaseData, string signature, string price, string currency, Dictionary<string, string> extraParams)
	{
		Stub("validateReceipt");
	}

	public static void trackRichEvent(string eventName, Dictionary<string, string> eventValues)
	{
		Stub("trackRichEvent");
	}

	public static void setImeiData(string imeiData)
	{
		Stub("setImeiData");
	}

	public static void setAndroidIdData(string androidIdData)
	{
		Stub("setAndroidIdData");
	}

	public static void setIsDebug(bool isDebug)
	{
		Stub("setIsDebug");
	}

	public static void setIsSandbox(bool isSandbox)
	{
		Stub("setIsSandbox");
	}

	public static void getConversionData()
	{
		Stub("getConversionData");
	}

	public static void handleOpenUrl(string url, string sourceApplication, string annotation)
	{
		Stub("handleOpenUrl");
	}

	public static string getAppsFlyerId()
	{
		Stub("getAppsFlyerId");
		return string.Empty;
	}

	public static void setGCMProjectNumber(string googleGCMNumber)
	{
		Stub("setGCMProjectNumber");
	}

	public static void updateServerUninstallToken(string token)
	{
		Stub("updateServerUninstallToken");
	}

	public static void enableUninstallTracking(string senderId)
	{
		Stub("enableUninstallTracking");
	}
}
