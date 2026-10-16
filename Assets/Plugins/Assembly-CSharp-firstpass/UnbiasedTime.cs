using System;
using UnityEngine;

public class UnbiasedTime : MonoBehaviour
{
	private static UnbiasedTime instance;

	[HideInInspector]
	public long timeOffset;

	public static UnbiasedTime Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject gameObject = new GameObject("UnbiasedTimeSingleton");
				instance = gameObject.AddComponent<UnbiasedTime>();
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			return instance;
		}
	}

	private void Awake()
	{
		SessionStart();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			SessionEnd();
		}
		else
		{
			SessionStart();
		}
	}

	private void OnApplicationQuit()
	{
		SessionEnd();
	}

	public DateTime Now()
	{
		return DateTime.Now.AddSeconds(-1f * (float)timeOffset);
	}

	public void UpdateTimeOffset()
	{
		UpdateTimeOffsetAndroid();
	}

	public bool IsUsingSystemTime()
	{
		return UsingSystemTimeAndroid();
	}

	private void SessionStart()
	{
		StartAndroid();
	}

	private void SessionEnd()
	{
		EndAndroid();
	}

	// ponytail: the native com.vasilij.unbiasedtime.UnbiasedTime Java class is absent from the APK,
	// so every AndroidJavaClass call throws. Wrapped in try/catch -> falls back to system time
	// (timeOffset stays 0). Anti-time-cheat is effectively disabled; gameplay is unaffected.
	private void UpdateTimeOffsetAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime"))
				{
					AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					if (androidJavaObject != null && androidJavaClass2 != null)
					{
						timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[1] { androidJavaObject });
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[STUB] UnbiasedTime native unavailable: " + ex.Message);
			timeOffset = 0L;
		}
	}

	private void StartAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime"))
				{
					AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					if (androidJavaObject != null && androidJavaClass2 != null)
					{
						androidJavaClass2.CallStatic("vtcOnSessionStart", androidJavaObject);
						timeOffset = androidJavaClass2.CallStatic<long>("vtcTimestampOffset", new object[0]);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[STUB] UnbiasedTime native unavailable: " + ex.Message);
			timeOffset = 0L;
		}
	}

	private void EndAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime"))
				{
					AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					if (androidJavaObject != null && androidJavaClass2 != null)
					{
						androidJavaClass2.CallStatic("vtcOnSessionEnd", androidJavaObject);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[STUB] UnbiasedTime native unavailable: " + ex.Message);
		}
	}

	private bool UsingSystemTimeAndroid()
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return true;
		}
		try
		{
			using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			{
				using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.vasilij.unbiasedtime.UnbiasedTime"))
				{
					AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
					if (androidJavaObject != null && androidJavaClass2 != null)
					{
						return androidJavaClass2.CallStatic<bool>("vtcUsingDeviceTime", new object[0]);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[STUB] UnbiasedTime native unavailable: " + ex.Message);
		}
		return true;
	}
}
