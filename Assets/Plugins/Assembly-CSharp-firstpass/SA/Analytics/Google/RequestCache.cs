using System;
using System.Collections.Generic;
using SA.Common.Data;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Analytics.Google
{
	public class RequestCache
	{
		private const string DATA_SPLITTER = "|";

		private const string RQUEST_DATA_SPLITTER = "%rps%";

		private const string GA_DATA_CACHE_KEY = "GoogleAnalyticsRequestCache";

		public static string SavedData
		{
			get
			{
				if (PlayerPrefs.HasKey("GoogleAnalyticsRequestCache"))
				{
					return PlayerPrefs.GetString("GoogleAnalyticsRequestCache");
				}
				return string.Empty;
			}
			set
			{
				PlayerPrefs.SetString("GoogleAnalyticsRequestCache", value);
			}
		}

		public static List<CachedRequest> CurrenCachedRequests
		{
			get
			{
				if (SavedData == string.Empty)
				{
					return new List<CachedRequest>();
				}
				try
				{
					List<CachedRequest> list = new List<CachedRequest>();
					List<object> list2 = Json.Deserialize(SavedData) as List<object>;
					foreach (object item in list2)
					{
						List<object> list3 = item as List<object>;
						CachedRequest cachedRequest = new CachedRequest();
						int num = 1;
						foreach (object item2 in list3)
						{
							string text = item2 as string;
							switch (num)
							{
							case 1:
								cachedRequest.RequestBody = text;
								break;
							case 2:
								cachedRequest.TimeCreated = Convert.ToInt64(text);
								break;
							}
							num++;
						}
						list.Add(cachedRequest);
					}
					return list;
				}
				catch (Exception ex)
				{
					Clear();
					Debug.LogError(ex.Message);
					return new List<CachedRequest>();
				}
			}
		}

		public static void SaveRequest(string cache)
		{
			if (GA_Settings.Instance.IsRequetsCachingEnabled)
			{
				CachedRequest item = new CachedRequest(cache, DateTime.Now.Ticks);
				List<CachedRequest> currenCachedRequests = CurrenCachedRequests;
				currenCachedRequests.Add(item);
				CacheRequests(currenCachedRequests);
			}
		}

		public static void SendCachedRequests()
		{
			Singleton<CacheQueue>.Instance.Run();
		}

		public static void Clear()
		{
			PlayerPrefs.DeleteKey("GoogleAnalyticsRequestCache");
		}

		public static void CacheRequests(List<CachedRequest> requests)
		{
			List<List<string>> list = new List<List<string>>();
			foreach (CachedRequest request in requests)
			{
				List<string> list2 = new List<string>();
				list2.Add(request.RequestBody);
				list2.Add(request.TimeCreated.ToString());
				list.Add(list2);
			}
			SavedData = Json.Serialize(list);
		}
	}
}
