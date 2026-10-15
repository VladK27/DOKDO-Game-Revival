namespace SA.AndroidNative.DynamicLinks
{
	public static class Proxy
	{
		private const string CLASS_NAME = "com.androidnative.firebase.dynamiclinks.Bridge";

		public static string GetPendingLink()
		{
			return CallStaticFunction<string>("getPendingLink", new object[0]);
		}

		public static void RequestLink(string request)
		{
			CallStaticFunction("requestShortLink", request);
		}

		private static T CallStaticFunction<T>(string methodName, params object[] args)
		{
			return AN_ProxyPool.CallStatic<T>("com.androidnative.firebase.dynamiclinks.Bridge", methodName, args);
		}

		private static void CallStaticFunction(string methodName, params object[] args)
		{
			AN_ProxyPool.CallStatic("com.androidnative.firebase.dynamiclinks.Bridge", methodName, args);
		}
	}
}
