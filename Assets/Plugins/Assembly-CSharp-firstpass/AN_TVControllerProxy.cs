public class AN_TVControllerProxy
{
	private const string CLASS_NAME = "com.androidnative.features.AN_TV";

	private static void CallActivityFunction(string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic("com.androidnative.features.AN_TV", methodName, args);
	}

	public static void AN_CheckForATVDevice()
	{
		CallActivityFunction("AN_CheckForATVDevice");
	}
}
