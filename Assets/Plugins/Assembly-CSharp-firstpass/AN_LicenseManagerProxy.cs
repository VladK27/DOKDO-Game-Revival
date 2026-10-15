public class AN_LicenseManagerProxy
{
	private const string CLASS_NAME = "com.androidnative.licensing.LicenseManager";

	private static void CallActivityFunction(string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic("com.androidnative.licensing.LicenseManager", methodName, args);
	}

	public static void StartLicenseRequest(string base64PublicKey)
	{
		CallActivityFunction("StartLicenseRequest", base64PublicKey);
	}
}
