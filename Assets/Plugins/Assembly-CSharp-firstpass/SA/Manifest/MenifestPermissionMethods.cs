namespace SA.Manifest
{
	internal static class MenifestPermissionMethods
	{
		public static string GetFullName(this ManifestPermission permission)
		{
			string text = "android.permission.";
			switch (permission)
			{
			case ManifestPermission.SET_ALARM:
				text = "com.android.alarm.permission.";
				break;
			case ManifestPermission.INSTALL_SHORTCUT:
			case ManifestPermission.UNINSTALL_SHORTCUT:
				text = "com.android.launcher.permission.";
				break;
			case ManifestPermission.ADD_VOICEMAIL:
				text = "com.android.voicemail.permission.";
				break;
			}
			return text + permission;
		}

		public static bool IsNormalPermission(this ManifestPermission permission)
		{
			switch (permission)
			{
			case ManifestPermission.ACCESS_LOCATION_EXTRA_COMMANDS:
			case ManifestPermission.ACCESS_NETWORK_STATE:
			case ManifestPermission.ACCESS_NOTIFICATION_POLICY:
			case ManifestPermission.ACCESS_WIFI_STATE:
			case ManifestPermission.ACCESS_WIMAX_STATE:
			case ManifestPermission.BLUETOOTH:
			case ManifestPermission.BLUETOOTH_ADMIN:
			case ManifestPermission.BROADCAST_STICKY:
			case ManifestPermission.CHANGE_NETWORK_STATE:
			case ManifestPermission.CHANGE_WIFI_MULTICAST_STATE:
			case ManifestPermission.CHANGE_WIFI_STATE:
			case ManifestPermission.CHANGE_WIMAX_STATE:
			case ManifestPermission.DISABLE_KEYGUARD:
			case ManifestPermission.EXPAND_STATUS_BAR:
			case ManifestPermission.FLASHLIGHT:
			case ManifestPermission.GET_PACKAGE_SIZE:
			case ManifestPermission.INTERNET:
			case ManifestPermission.KILL_BACKGROUND_PROCESSES:
			case ManifestPermission.MODIFY_AUDIO_SETTINGS:
			case ManifestPermission.NFC:
			case ManifestPermission.READ_SYNC_SETTINGS:
			case ManifestPermission.READ_SYNC_STATS:
			case ManifestPermission.RECEIVE_BOOT_COMPLETED:
			case ManifestPermission.REORDER_TASKS:
			case ManifestPermission.REQUEST_INSTALL_PACKAGES:
			case ManifestPermission.SET_TIME_ZONE:
			case ManifestPermission.SET_WALLPAPER:
			case ManifestPermission.SET_WALLPAPER_HINTS:
			case ManifestPermission.SUBSCRIBED_FEEDS_READ:
			case ManifestPermission.TRANSMIT_IR:
			case ManifestPermission.USE_FINGERPRINT:
			case ManifestPermission.VIBRATE:
			case ManifestPermission.WAKE_LOCK:
			case ManifestPermission.WRITE_SYNC_SETTINGS:
			case ManifestPermission.SET_ALARM:
			case ManifestPermission.INSTALL_SHORTCUT:
			case ManifestPermission.UNINSTALL_SHORTCUT:
				return true;
			default:
				return false;
			}
		}
	}
}
