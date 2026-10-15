using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public static class ParsePushesStub
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<string, Dictionary<string, object>> OnPushReceived__BackingField = delegate
	{
	};

	public static event Action<string, Dictionary<string, object>> OnPushReceived
	{
		add
		{
			Action<string, Dictionary<string, object>> action = OnPushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string, Dictionary<string, object>> action = OnPushReceived__BackingField;
			Action<string, Dictionary<string, object>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPushReceived__BackingField, (Action<string, Dictionary<string, object>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static void InitParse()
	{
	}
}
