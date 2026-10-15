using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AN_DeepLinkingManager : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string> OnDeepLinkReceived__BackingField;

	public static event Action<string> OnDeepLinkReceived
	{
		add
		{
			Action<string> action = OnDeepLinkReceived__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeepLinkReceived__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = OnDeepLinkReceived__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnDeepLinkReceived__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static void GetLaunchDeepLinkId()
	{
		AN_SocialSharingProxy.GetLaunchDeepLinkId();
	}

	private void DeepLinkReceived(string linkId)
	{
		OnDeepLinkReceived__BackingField(linkId);
	}
}
