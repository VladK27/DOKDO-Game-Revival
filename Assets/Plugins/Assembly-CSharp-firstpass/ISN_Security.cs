using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_Security : Singleton<ISN_Security>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<ISN_LocalReceiptResult> OnReceiptLoaded__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnReceiptRefreshComplete__BackingField = delegate
	{
	};

	public static event Action<ISN_LocalReceiptResult> OnReceiptLoaded
	{
		add
		{
			Action<ISN_LocalReceiptResult> action = OnReceiptLoaded__BackingField;
			Action<ISN_LocalReceiptResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnReceiptLoaded__BackingField, (Action<ISN_LocalReceiptResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_LocalReceiptResult> action = OnReceiptLoaded__BackingField;
			Action<ISN_LocalReceiptResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnReceiptLoaded__BackingField, (Action<ISN_LocalReceiptResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> OnReceiptRefreshComplete
	{
		add
		{
			Action<Result> action = OnReceiptRefreshComplete__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnReceiptRefreshComplete__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnReceiptRefreshComplete__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnReceiptRefreshComplete__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void RetrieveLocalReceipt()
	{
	}

	public void StartReceiptRefreshRequest()
	{
	}

	private void Event_ReceiptLoaded(string data)
	{
		ISN_LocalReceiptResult obj = new ISN_LocalReceiptResult(data);
		OnReceiptLoaded__BackingField(obj);
	}

	private void Event_ReceiptRefreshRequestReceived(string data)
	{
		Result obj = ((!data.Equals("1")) ? new Result(new Error()) : new Result());
		OnReceiptRefreshComplete__BackingField(obj);
	}
}
