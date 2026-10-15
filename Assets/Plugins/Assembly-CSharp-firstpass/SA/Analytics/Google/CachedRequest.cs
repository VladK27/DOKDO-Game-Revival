using System;
using System.Collections.Generic;

namespace SA.Analytics.Google
{
	public class CachedRequest
	{
		private long _TimeCreated;

		private string _RequestBody;

		public long TimeCreated
		{
			get
			{
				return _TimeCreated;
			}
			set
			{
				_TimeCreated = value;
			}
		}

		public string RequestBody
		{
			get
			{
				return _RequestBody;
			}
			set
			{
				_RequestBody = value;
			}
		}

		public string Delay
		{
			get
			{
				DateTime value = new DateTime(TimeCreated);
				double totalMilliseconds = DateTime.Now.Subtract(value).TotalMilliseconds;
				return Convert.ToInt64(totalMilliseconds).ToString();
			}
		}

		public List<string> DataForJSON
		{
			get
			{
				List<string> list = new List<string>();
				list.Add(RequestBody);
				list.Add(TimeCreated.ToString());
				return list;
			}
		}

		public CachedRequest()
		{
		}

		public CachedRequest(string body, long ticks)
		{
			_RequestBody = body;
			_TimeCreated = ticks;
		}
	}
}
