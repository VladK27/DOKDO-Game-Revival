using System;
using SA.Common.Pattern;
using UnityEngine;

public class ANNativeEventsExample : MonoBehaviour
{
	private void Start()
	{
		AndroidApp instance = Singleton<AndroidApp>.Instance;
		instance.OnActivityResult = (Action<AndroidActivityResult>)Delegate.Combine(instance.OnActivityResult, new Action<AndroidActivityResult>(OnActivityResult));
	}

	private void OnStop()
	{
		Debug.Log("Activity event: OnStop");
	}

	private void OnStart()
	{
		Debug.Log("Activity event: OnStart");
	}

	private void OnNewIntent()
	{
		Debug.Log("Activity event: OnNewIntent");
	}

	private void OnActivityResult(AndroidActivityResult result)
	{
		Debug.Log("Activity event: OnActivityResult");
		Debug.Log("result.code: " + result.code);
		Debug.Log("result.requestId: " + result.requestId);
	}
}
