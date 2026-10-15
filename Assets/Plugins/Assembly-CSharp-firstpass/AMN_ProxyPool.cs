using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AMN_ProxyPool
{
	private static Dictionary<string, AndroidJavaObject> pool = new Dictionary<string, AndroidJavaObject>();

	private static bool isInitialized = false;

	public static void CallStatic(string className, string methodName, params object[] args)
	{
		if (Application.platform != RuntimePlatform.Android)
		{
			return;
		}
		try
		{
			AndroidJavaObject androidJavaObject;
			if (pool.ContainsKey(className))
			{
				androidJavaObject = pool[className];
			}
			else
			{
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.amazonnative.AMNMobileAd");
				androidJavaObject = androidJavaClass.CallStatic<AndroidJavaObject>("getInstance", new object[0]);
				pool.Add(className, androidJavaObject);
			}
			if (isInitialized)
			{
				androidJavaObject.Call(methodName, args);
			}
			else
			{
				isInitialized = true;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning(ex.Message);
		}
	}
}
