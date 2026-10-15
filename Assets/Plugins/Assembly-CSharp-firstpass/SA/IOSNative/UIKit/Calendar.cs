using System;
using SA.Common.Pattern;

namespace SA.IOSNative.UIKit
{
	internal static class Calendar
	{
		private static Action<DateTime> OnCalendarClosed;

		static Calendar()
		{
			Singleton<NativeReceiver>.Instance.Init();
		}

		public static void PickDate(Action<DateTime> callback, int startFromYear = 0)
		{
			OnCalendarClosed = callback;
		}

		internal static void DatePicked(string time)
		{
			DateTime obj = DateTime.Parse(time);
			OnCalendarClosed(obj);
		}
	}
}
