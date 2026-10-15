using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class TimeIntervalTrigger : NotificationTrigger
	{
		public int intervalToFire;

		public TimeIntervalTrigger(int secondsInterval)
		{
			intervalToFire = secondsInterval;
		}

		public override string ToString()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("intervalToFire", intervalToFire);
			dictionary.Add("repeats", repeated);
			return Json.Serialize(dictionary);
		}
	}
}
