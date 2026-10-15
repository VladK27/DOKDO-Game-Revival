using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;

namespace SA.IOSNative.UIKit
{
	public static class DateTimePicker
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<DateTime> OnPickerClosed__BackingField;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<DateTime> OnPickerDateChanged__BackingField;

		private static event Action<DateTime> OnPickerClosed
		{
			add
			{
				Action<DateTime> action = OnPickerClosed__BackingField;
				Action<DateTime> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnPickerClosed__BackingField, (Action<DateTime>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<DateTime> action = OnPickerClosed__BackingField;
				Action<DateTime> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnPickerClosed__BackingField, (Action<DateTime>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private static event Action<DateTime> OnPickerDateChanged
		{
			add
			{
				Action<DateTime> action = OnPickerDateChanged__BackingField;
				Action<DateTime> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnPickerDateChanged__BackingField, (Action<DateTime>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<DateTime> action = OnPickerDateChanged__BackingField;
				Action<DateTime> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnPickerDateChanged__BackingField, (Action<DateTime>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		static DateTimePicker()
		{
			Singleton<NativeReceiver>.Instance.Init();
		}

		public static void Show(DateTimePickerMode mode, Action<DateTime> callback)
		{
			OnPickerClosed__BackingField = callback;
		}

		public static void Show(DateTimePickerMode mode, DateTime dateTime, Action<DateTime> callback)
		{
			OnPickerClosed__BackingField = callback;
			DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			double totalSeconds = (dateTime - dateTime2).TotalSeconds;
		}

		internal static void DateChangedEvent(string time)
		{
			DateTime obj = DateTime.Parse(time);
			OnPickerDateChanged__BackingField(obj);
		}

		internal static void PickerClosed(string time)
		{
			DateTime obj = DateTime.Parse(time);
			OnPickerClosed__BackingField(obj);
		}
	}
}
