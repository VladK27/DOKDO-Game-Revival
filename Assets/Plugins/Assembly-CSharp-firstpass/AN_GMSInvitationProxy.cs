using UnityEngine;

public class AN_GMSInvitationProxy : MonoBehaviour
{
	private const string CLASS_NAME = "com.androidnative.gms.core.GameInvitationManager";

	private static void CallActivityFunction(string methodName, params object[] args)
	{
		AN_ProxyPool.CallStatic("com.androidnative.gms.core.GameInvitationManager", methodName, args);
	}

	public static void registerInvitationListener()
	{
		CallActivityFunction("registerInvitationListener");
	}

	public static void unregisterInvitationListener()
	{
		CallActivityFunction("unregisterInvitationListener");
	}

	public static void LoadInvitations()
	{
		CallActivityFunction("loadInvitations");
	}
}
