using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class BaseAndroidPopup : MonoBehaviour
{
	public string title;

	public string message;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AndroidDialogResult> ActionComplete__BackingField = delegate
	{
	};

	public event Action<AndroidDialogResult> ActionComplete
	{
		add
		{
			Action<AndroidDialogResult> action = ActionComplete__BackingField;
			Action<AndroidDialogResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<AndroidDialogResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AndroidDialogResult> action = ActionComplete__BackingField;
			Action<AndroidDialogResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionComplete__BackingField, (Action<AndroidDialogResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void onDismissed(string data)
	{
		ActionComplete__BackingField(AndroidDialogResult.CLOSED);
		UnityEngine.Object.Destroy(base.gameObject);
	}

	protected void DispatchAction(AndroidDialogResult res)
	{
		ActionComplete__BackingField(res);
	}
}
