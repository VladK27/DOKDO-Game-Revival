using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using SA.Common.Models;
using UnityEngine;

namespace SA.Common.Util
{
	public static class General
	{
		private static string[] _rfc3339Formats = new string[0];

		private const string Rfc3339Format = "yyyy-MM-dd'T'HH:mm:ssK";

		private const string MinRfc339Value = "0001-01-01T00:00:00Z";

		public static int CurrentTimeStamp
		{
			get
			{
				return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			}
		}

		public static string[] DateTimePatterns
		{
			get
			{
				if (_rfc3339Formats.Length > 0)
				{
					return _rfc3339Formats;
				}
				_rfc3339Formats = new string[11];
				_rfc3339Formats[0] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
				_rfc3339Formats[1] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffK";
				_rfc3339Formats[2] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffK";
				_rfc3339Formats[3] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK";
				_rfc3339Formats[4] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK";
				_rfc3339Formats[5] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK";
				_rfc3339Formats[6] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK";
				_rfc3339Formats[7] = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";
				_rfc3339Formats[8] = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK";
				_rfc3339Formats[9] = DateTimeFormatInfo.InvariantInfo.UniversalSortableDateTimePattern;
				_rfc3339Formats[10] = DateTimeFormatInfo.InvariantInfo.SortableDateTimePattern;
				return _rfc3339Formats;
			}
		}

		public static void Invoke(float time, Action callback, string name = "")
		{
			Invoker invoker = Invoker.Create(name);
			invoker.StartInvoke(callback, time);
		}

		public static T ParseEnum<T>(string value)
		{
			try
			{
				return (T)Enum.Parse(typeof(T), value, true);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Enum Parsing failed: " + ex.Message);
				return default(T);
			}
		}

		public static string DateTimeToRfc3339(DateTime dateTime)
		{
			if (dateTime == DateTime.MinValue)
			{
				return "0001-01-01T00:00:00Z";
			}
			return dateTime.ToString("yyyy-MM-dd'T'HH:mm:ssK", DateTimeFormatInfo.InvariantInfo);
		}

		public static DateTime ConvertFromUnixTimestamp(long timestamp)
		{
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
		}

		public static long ConvertToUnixTimestamp(DateTime date)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			return (long)(date.ToUniversalTime() - dateTime).TotalSeconds;
		}

		public static bool TryParseRfc3339(string s, out DateTime result)
		{
			bool result2 = false;
			result = DateTime.Now;
			DateTime result3;
			if (!string.IsNullOrEmpty(s) && DateTime.TryParseExact(s, DateTimePatterns, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal, out result3))
			{
				result = DateTime.SpecifyKind(result3, DateTimeKind.Utc);
				result = result.ToLocalTime();
				result2 = true;
			}
			return result2;
		}

		public static string HMAC(string key, string data)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			using (HMACSHA256 hMACSHA = new HMACSHA256(bytes))
			{
				hMACSHA.ComputeHash(Encoding.UTF8.GetBytes(data));
				byte[] hash = hMACSHA.Hash;
				string text = string.Empty;
				for (int i = 0; i < hash.Length; i++)
				{
					text += hash[i].ToString("X2");
				}
				return text.ToLower();
			}
		}

		public static void CleanupInstallation()
		{
		}
	}
}
