using System;
using SA.Common.Pattern;
using UnityEngine;

public class AndroidApp : Singleton<AndroidApp>
{
	public Action<AndroidActivityResult> OnActivityResult = delegate
	{
	};

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Debug.Log("GooglePlayTBM Created");
	}

	public void ActivateListner()
	{
	}

	private void onActivityResult(string data)
	{
		string[] array = data.Split("|"[0]);
		AndroidActivityResult obj = new AndroidActivityResult(array[0], array[1]);
		OnActivityResult(obj);
	}
}
