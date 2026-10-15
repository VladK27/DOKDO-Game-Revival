using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_GestureRecognizer : Singleton<ISN_GestureRecognizer>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ISN_SwipeDirection> OnSwipe__BackingField = delegate
	{
	};

	public event Action<ISN_SwipeDirection> OnSwipe
	{
		add
		{
			Action<ISN_SwipeDirection> action = OnSwipe__BackingField;
			Action<ISN_SwipeDirection> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSwipe__BackingField, (Action<ISN_SwipeDirection>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_SwipeDirection> action = OnSwipe__BackingField;
			Action<ISN_SwipeDirection> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnSwipe__BackingField, (Action<ISN_SwipeDirection>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	private void OnSwipeAction(string data)
	{
		int obj = Convert.ToInt32(data);
		OnSwipe__BackingField((ISN_SwipeDirection)obj);
	}
}
