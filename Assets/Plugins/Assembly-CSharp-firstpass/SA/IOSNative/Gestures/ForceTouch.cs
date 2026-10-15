using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Gestures
{
	public class ForceTouch : Singleton<ForceTouch>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action OnForceTouchStarted__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action OnForceTouchFinished__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ForceInfo> OnForceChanged__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<string> OnAppShortcutClick__BackingField = delegate
		{
		};

		private static bool _IsTouchTrigerred;

		public static string AppOpenshortcutItem
		{
			get
			{
				return string.Empty;
			}
		}

		public event Action OnForceTouchStarted
		{
			add
			{
				Action action = OnForceTouchStarted__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceTouchStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnForceTouchStarted__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceTouchStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action OnForceTouchFinished
		{
			add
			{
				Action action = OnForceTouchFinished__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceTouchFinished__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnForceTouchFinished__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceTouchFinished__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<ForceInfo> OnForceChanged
		{
			add
			{
				Action<ForceInfo> action = OnForceChanged__BackingField;
				Action<ForceInfo> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceChanged__BackingField, (Action<ForceInfo>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<ForceInfo> action = OnForceChanged__BackingField;
				Action<ForceInfo> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnForceChanged__BackingField, (Action<ForceInfo>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<string> OnAppShortcutClick
		{
			add
			{
				Action<string> action = OnAppShortcutClick__BackingField;
				Action<string> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnAppShortcutClick__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<string> action = OnAppShortcutClick__BackingField;
				Action<string> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnAppShortcutClick__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void Setup(float forceTouchDelay, float baseForceTouchPressure, float triggeringForceTouchPressure)
		{
		}

		private void didStartForce(string array)
		{
			_IsTouchTrigerred = true;
			OnForceTouchStarted__BackingField();
		}

		private void didForceChanged(string array)
		{
			if (_IsTouchTrigerred)
			{
				string[] array2 = array.Split('|');
				float force = Convert.ToSingle(array2[0]);
				float maxForce = Convert.ToSingle(array2[1]);
				ForceInfo obj = new ForceInfo(force, maxForce);
				OnForceChanged__BackingField(obj);
			}
		}

		private void didForceEnded(string array)
		{
			_IsTouchTrigerred = false;
			OnForceTouchFinished__BackingField();
		}

		private void performActionForShortcutItem(string action)
		{
			OnAppShortcutClick__BackingField(action);
		}
	}
}
