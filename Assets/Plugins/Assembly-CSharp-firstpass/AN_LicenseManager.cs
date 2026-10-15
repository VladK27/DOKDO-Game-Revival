using System;
using SA.Common.Pattern;
using UnityEngine;

public class AN_LicenseManager : Singleton<AN_LicenseManager>
{
	public static Action<AN_LicenseRequestResult> OnLicenseRequestResult = delegate
	{
	};

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartLicenseRequest()
	{
		StartLicenseRequest(AndroidNativeSettings.Instance.base64EncodedPublicKey);
	}

	public void StartLicenseRequest(string base64PublicKey)
	{
		AN_LicenseManagerProxy.StartLicenseRequest(base64PublicKey);
	}

	private void OnLicenseRequestRes(string data)
	{
		Debug.Log("[OnLicenseRequestResult] Data: " + data);
		string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
		AN_LicenseRequestResult obj = new AN_LicenseRequestResult((AN_LicenseStatusCode)Enum.Parse(typeof(AN_LicenseStatusCode), array[0]), (AN_LicenseErrorCode)Enum.Parse(typeof(AN_LicenseErrorCode), array[1]));
		OnLicenseRequestResult(obj);
	}
}
