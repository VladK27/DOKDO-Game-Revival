using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class PermissionsManager : Singleton<PermissionsManager>
{
	private const string PM_CLASS_NAME = "com.androidnative.features.permissions.PermissionsManager";

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<AN_GrantPermissionsResult> ActionPermissionsRequestCompleted__BackingField = delegate
	{
	};

	public static event Action<AN_GrantPermissionsResult> ActionPermissionsRequestCompleted
	{
		add
		{
			Action<AN_GrantPermissionsResult> action = ActionPermissionsRequestCompleted__BackingField;
			Action<AN_GrantPermissionsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPermissionsRequestCompleted__BackingField, (Action<AN_GrantPermissionsResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AN_GrantPermissionsResult> action = ActionPermissionsRequestCompleted__BackingField;
			Action<AN_GrantPermissionsResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPermissionsRequestCompleted__BackingField, (Action<AN_GrantPermissionsResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static bool IsPermissionGranted(AN_Permission permission)
	{
		return IsPermissionGranted(permission.GetFullName());
	}

	public static bool IsPermissionGranted(string permission)
	{
		return AN_ProxyPool.CallStatic<bool>("com.androidnative.features.permissions.PermissionsManager", "checkForPermission", new object[1] { permission });
	}

	public static bool ShouldShowRequestPermission(AN_Permission permission)
	{
		return AN_ProxyPool.CallStatic<bool>("com.androidnative.features.permissions.PermissionsManager", "shouldShowRequestPermissionRationale", new object[1] { permission.GetFullName() });
	}

	public void RequestPermissions(params AN_Permission[] permissions)
	{
		List<string> list = new List<string>();
		foreach (AN_Permission permission in permissions)
		{
			list.Add(permission.GetFullName());
		}
		RequestPermissions(list.ToArray());
	}

	public void RequestPermissions(params string[] permissions)
	{
		AN_ProxyPool.CallStatic("com.androidnative.features.permissions.PermissionsManager", "requestPermissions", AndroidNative.ArrayToString(permissions));
	}

	private void OnPermissionsResult(string data)
	{
		UnityEngine.Debug.Log("OnPermissionsResult:" + data);
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		string[] array2 = AndroidNative.StringToArray(array[0]);
		string[] array3 = AndroidNative.StringToArray(array[1]);
		string[] array4 = array2;
		foreach (string message in array4)
		{
			UnityEngine.Debug.Log(message);
		}
		string[] array5 = array3;
		foreach (string message2 in array5)
		{
			UnityEngine.Debug.Log(message2);
		}
		AN_GrantPermissionsResult obj = new AN_GrantPermissionsResult(array2, array3);
		ActionPermissionsRequestCompleted__BackingField(obj);
	}

	public static AN_Permission GetPermissionByName(string fullName)
	{
		foreach (AN_Permission value in Enum.GetValues(typeof(AN_Permission)))
		{
			if (value.GetFullName().Equals(fullName))
			{
				return value;
			}
		}
		return AN_Permission.UNDEFINED;
	}
}
