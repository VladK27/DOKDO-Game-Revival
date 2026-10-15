using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;

public class AndroidAppInfoLoader : Singleton<AndroidAppInfoLoader>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<PackageAppInfo> ActionPacakgeInfoLoaded__BackingField = delegate
	{
	};

	public PackageAppInfo PacakgeInfo = new PackageAppInfo();

	public static event Action<PackageAppInfo> ActionPacakgeInfoLoaded
	{
		add
		{
			Action<PackageAppInfo> action = ActionPacakgeInfoLoaded__BackingField;
			Action<PackageAppInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPacakgeInfoLoaded__BackingField, (Action<PackageAppInfo>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<PackageAppInfo> action = ActionPacakgeInfoLoaded__BackingField;
			Action<PackageAppInfo> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPacakgeInfoLoaded__BackingField, (Action<PackageAppInfo>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void LoadPackageInfo()
	{
		AndroidNative.LoadPackageInfo();
	}

	private void OnPackageInfoLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		PacakgeInfo.versionName = array[0];
		PacakgeInfo.versionCode = array[1];
		PacakgeInfo.packageName = array[2];
		PacakgeInfo.lastUpdateTime = Convert.ToInt64(array[3]);
		PacakgeInfo.sharedUserId = array[3];
		PacakgeInfo.sharedUserLabel = array[4];
		ActionPacakgeInfoLoaded__BackingField(PacakgeInfo);
	}
}
