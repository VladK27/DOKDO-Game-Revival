using SA.Analytics.Google;

namespace SA.Analytics.Firebase
{
	public static class Proxy
	{
		private const string CLASS_NAME = "com.stansassets.firebase.analytics.Bridge";

		public static void Init()
		{
			CallStaticFunction("Init");
		}

		public static void SetEnabled(bool enabled)
		{
			CallStaticFunction("SetEnabled", enabled);
		}

		public static void SetMinimumSessionDuration(long milliseconds)
		{
			CallStaticFunction("SetMinimumSessionDuration", milliseconds);
		}

		public static void SetSessionTimeoutDuration(long milliseconds)
		{
			CallStaticFunction("SetSessionTimeoutDuration", milliseconds);
		}

		public static void SetUserId(string userId)
		{
			CallStaticFunction("SetUserId", userId);
		}

		public static void SetUserProperty(string name, string value)
		{
			CallStaticFunction("SetUserProperty", name, value);
		}

		public static void LogEvent(string name, string data = null)
		{
			CallStaticFunction("LogEvent", name, data);
		}

		private static void CallStaticFunction(string methodName, params object[] args)
		{
			AndroidTools.CallStatic("com.stansassets.firebase.analytics.Bridge", methodName, args);
		}
	}
}
