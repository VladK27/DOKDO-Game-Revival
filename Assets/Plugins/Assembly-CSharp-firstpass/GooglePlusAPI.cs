using System;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlusAPI : Singleton<GooglePlusAPI>
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	[Obsolete("clearDefaultAccount is deprecated, please use ClearDefaultAccount instead.")]
	public void clearDefaultAccount()
	{
		ClearDefaultAccount();
	}

	public void ClearDefaultAccount()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSGeneralProxy.clearDefaultAccount();
			Singleton<GooglePlayConnection>.Instance.Disconnect();
		}
	}
}
