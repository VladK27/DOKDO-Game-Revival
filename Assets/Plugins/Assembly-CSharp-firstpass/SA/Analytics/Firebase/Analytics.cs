using System.Collections.Generic;
using System.Text;

namespace SA.Analytics.Firebase
{
	public static class Analytics
	{
		private const string SEPARATOR1 = "%";

		private const string SEPARATOR2 = "|";

		public static void Init()
		{
			Proxy.Init();
		}

		public static void SetEnabled(bool enabled)
		{
			Proxy.SetEnabled(enabled);
		}

		public static void SetMinimumSessionDuration(long milliseconds)
		{
			Proxy.SetMinimumSessionDuration(milliseconds);
		}

		public static void SetSessionTimeoutDuration(long milliseconds)
		{
			Proxy.SetSessionTimeoutDuration(milliseconds);
		}

		public static void SetUserId(string userId)
		{
			Proxy.SetUserId(userId);
		}

		public static void SetUserProperty(string name, string value)
		{
			Proxy.SetUserProperty(name, value);
		}

		public static void LogEvent(string name, Dictionary<string, object> data = null)
		{
			if (data == null || data.Count == 0)
			{
				Proxy.LogEvent(name);
				return;
			}
			Dictionary<string, object>.Enumerator enumerator = data.GetEnumerator();
			enumerator.MoveNext();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(enumerator.Current.Key);
			stringBuilder.Append("|");
			stringBuilder.Append(enumerator.Current.Value.ToString());
			while (enumerator.MoveNext())
			{
				stringBuilder.Append("%");
				stringBuilder.Append(enumerator.Current.Key);
				stringBuilder.Append("|");
				stringBuilder.Append(enumerator.Current.Value.ToString());
			}
			Proxy.LogEvent(name, stringBuilder.ToString());
		}
	}
}
