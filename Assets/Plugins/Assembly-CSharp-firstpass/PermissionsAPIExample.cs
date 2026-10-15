using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

public class PermissionsAPIExample : MonoBehaviour
{
	private void Awake()
	{
		PermissionsManager.ActionPermissionsRequestCompleted += HandleActionPermissionsRequestCompleted;
	}

	public void CheckPermission()
	{
		Debug.Log("CheckPermission");
		bool flag = PermissionsManager.IsPermissionGranted(AN_Permission.WRITE_EXTERNAL_STORAGE);
		Debug.Log(flag);
		flag = PermissionsManager.IsPermissionGranted(AN_Permission.INTERNET);
		Debug.Log(flag);
		CheckShouldRequestPermission();
	}

	public void RequestPermission()
	{
		Singleton<PermissionsManager>.Instance.RequestPermissions(AN_Permission.WRITE_EXTERNAL_STORAGE, AN_Permission.CAMERA);
	}

	public void CheckShouldRequestPermission()
	{
		Debug.Log("CheckShouldRequestPermission: " + PermissionsManager.ShouldShowRequestPermission(AN_Permission.WRITE_EXTERNAL_STORAGE));
	}

	private void HandleActionPermissionsRequestCompleted(AN_GrantPermissionsResult res)
	{
		Debug.Log("HandleActionPermissionsRequestCompleted");
		foreach (KeyValuePair<AN_Permission, AN_PermissionState> item in res.RequestedPermissionsState)
		{
			Debug.Log(item.Key.GetFullName() + " / " + item.Value);
		}
	}
}
