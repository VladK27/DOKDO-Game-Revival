using System;
using System.Collections.Generic;
using SA.Common.Pattern;

namespace SA.IOSNative.Privacy
{
	public static class PermissionsManager
	{
		private static Dictionary<string, Action<PermissionStatus>> OnResponseDictionary;

		static PermissionsManager()
		{
			OnResponseDictionary = new Dictionary<string, Action<PermissionStatus>>();
		}

		public static PermissionStatus CheckPermissions(Permission permission)
		{
			return PermissionStatus.NotDetermined;
		}

		public static void RequestPermission(Permission permission, Action<PermissionStatus> callback)
		{
			if (Singleton<NativeReceiver>.Instance == null)
			{
				Singleton<NativeReceiver>.Instance.Init();
			}
			OnResponseDictionary[permission.ToString()] = callback;
		}

		internal static void PermissionRequestResponse(string permissionData)
		{
			string[] array = permissionData.Split(new string[1] { "|%|" }, StringSplitOptions.None);
			for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
			{
			}
			if (array.Length <= 0)
			{
				return;
			}
			string key = array[0];
			Action<PermissionStatus> action = OnResponseDictionary[key];
			if (action == null)
			{
				return;
			}
			string text = array[1];
			if (text != null)
			{
				try
				{
					int num = int.Parse(text);
					PermissionStatus obj = (PermissionStatus)num;
					action(obj);
				}
				catch (FormatException ex)
				{
					ISN_Logger.Log(ex.ToString());
				}
			}
		}
	}
}
