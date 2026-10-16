using System.Collections.Generic;
using IronSourceJSON;
using UnityEngine;

// ponytail: stub for the IronSource config bridge (ads disabled). Original ctor called
// new AndroidJavaClass("com.ironsource.unity.androidbridge.AndroidBridge"), absent from the APK.
public class IronSourceConfig
{
	private const string unsupportedPlatformStr = "Unsupported Platform";

	private static IronSourceConfig _instance;

	private static AndroidJavaObject _androidBridge;

	private static readonly string AndroidBridge = "com.ironsource.unity.androidbridge.AndroidBridge";

	public static IronSourceConfig Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new IronSourceConfig();
			}
			return _instance;
		}
	}

	public IronSourceConfig()
	{
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass(AndroidBridge))
			{
				_androidBridge = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning("[STUB] IronSourceConfig native unavailable: " + ex.Message);
		}
	}

	private bool NoBridge(string method)
	{
		if (_androidBridge == null)
		{
			Debug.Log("[STUB] IronSourceConfig." + method);
			return true;
		}
		return false;
	}

	public void setLanguage(string language)
	{
		if (!NoBridge("setLanguage"))
		{
			_androidBridge.Call("setLanguage", language);
		}
	}

	public void setClientSideCallbacks(bool status)
	{
		if (!NoBridge("setClientSideCallbacks"))
		{
			_androidBridge.Call("setClientSideCallbacks", status);
		}
	}

	public void setRewardedVideoCustomParams(Dictionary<string, string> rewardedVideoCustomParams)
	{
		if (!NoBridge("setRewardedVideoCustomParams"))
		{
			_androidBridge.Call("setRewardedVideoCustomParams", Json.Serialize(rewardedVideoCustomParams));
		}
	}

	public void setOfferwallCustomParams(Dictionary<string, string> offerwallCustomParams)
	{
		if (!NoBridge("setOfferwallCustomParams"))
		{
			_androidBridge.Call("setOfferwallCustomParams", Json.Serialize(offerwallCustomParams));
		}
	}
}
