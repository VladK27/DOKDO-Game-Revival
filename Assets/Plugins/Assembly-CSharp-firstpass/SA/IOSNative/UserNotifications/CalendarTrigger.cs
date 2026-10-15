using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class CalendarTrigger : NotificationTrigger
	{
		private DateComponents ComponentsOfDateToFire;

		public CalendarTrigger(DateComponents dateComponents)
		{
			ComponentsOfDateToFire = dateComponents;
		}

		public override string ToString()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int? year = ComponentsOfDateToFire.Year;
			if (year.HasValue)
			{
				dictionary.Add("year", ComponentsOfDateToFire.Year);
			}
			int? month = ComponentsOfDateToFire.Month;
			if (month.HasValue)
			{
				dictionary.Add("month", ComponentsOfDateToFire.Month);
			}
			int? day = ComponentsOfDateToFire.Day;
			if (day.HasValue)
			{
				dictionary.Add("day", ComponentsOfDateToFire.Day);
			}
			int? hour = ComponentsOfDateToFire.Hour;
			if (hour.HasValue)
			{
				dictionary.Add("hour", ComponentsOfDateToFire.Hour);
			}
			int? minute = ComponentsOfDateToFire.Minute;
			if (minute.HasValue)
			{
				dictionary.Add("minute", ComponentsOfDateToFire.Minute);
			}
			int? second = ComponentsOfDateToFire.Second;
			if (second.HasValue)
			{
				dictionary.Add("second", ComponentsOfDateToFire.Second);
			}
			int? weekday = ComponentsOfDateToFire.Weekday;
			if (weekday.HasValue)
			{
				dictionary.Add("weekday", ComponentsOfDateToFire.Weekday);
			}
			int? quarter = ComponentsOfDateToFire.Quarter;
			if (quarter.HasValue)
			{
				dictionary.Add("quarter", ComponentsOfDateToFire.Quarter);
			}
			dictionary.Add("repeats", repeated);
			return Json.Serialize(dictionary);
		}
	}
}
