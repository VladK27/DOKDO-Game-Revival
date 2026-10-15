using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class MNProxyPool
{
	private static Dictionary<string, AndroidJavaObject> pool = new Dictionary<string, AndroidJavaObject>();

	public static void CallStatic(string className, string methodName, params object[] args)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		Debug.Log("AN: Using proxy for class: " + className + " method:" + methodName);
		try
		{
			AndroidJavaObject bridge;
			if (pool.ContainsKey(className))
			{
				bridge = pool[className];
			}
			else
			{
				bridge = new AndroidJavaObject(className);
				pool.Add(className, bridge);
			}
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject androidJavaObject = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			androidJavaObject.Call("runOnUiThread", (AndroidJavaRunnable)(() =>
			{
				bridge.CallStatic(methodName, args);
			}));
		}
		catch (Exception ex)
		{
			Debug.LogWarning(ex.Message);
		}
	}
}
