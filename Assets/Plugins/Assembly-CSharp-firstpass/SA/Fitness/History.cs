using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Fitness
{
	public sealed class History : Singleton<History>
	{
		private Dictionary<int, ReadDailyTotalRequest> dailyTotalRequests = new Dictionary<int, ReadDailyTotalRequest>();

		private Dictionary<int, ReadHistoryRequest> readRequests = new Dictionary<int, ReadHistoryRequest>();

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void ReadData(ReadHistoryRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.StartTime.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(request.EndTime.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(request.TimeUnit.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(request.Limit);
			stringBuilder.Append("|");
			stringBuilder.Append(Convert.ToInt32(request.IsAggregated));
			stringBuilder.Append("|");
			if (request.IsAggregated)
			{
				stringBuilder.Append(request.DataType.Value);
				stringBuilder.Append("|");
				stringBuilder.Append(request.AggregateType.Value);
				stringBuilder.Append("|");
				stringBuilder.Append((int)request.BucketingType);
				stringBuilder.Append("|");
				stringBuilder.Append(request.MinDuration);
				stringBuilder.Append("|");
				stringBuilder.Append(request.BucketUnit.ToString());
			}
			else
			{
				stringBuilder.Append(request.DataType.Value);
			}
			readRequests.Add(request.Id, request);
			Proxy.ReadData(stringBuilder.ToString());
		}

		public void ReadDailyTotal(ReadDailyTotalRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.DataType.Value);
			dailyTotalRequests.Add(request.Id, request);
			Proxy.ReadDailyTotal(stringBuilder.ToString());
		}

		public void ReadDailyTotalFromLocalDevice(ReadDailyTotalRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.DataType.Value);
			dailyTotalRequests.Add(request.Id, request);
			Proxy.ReadDailyTotalFromLocalDevice(stringBuilder.ToString());
		}

		public void InsertData()
		{
			StringBuilder stringBuilder = new StringBuilder("hello insert data");
			Proxy.InsertData(stringBuilder.ToString());
		}

		public void UpdateData()
		{
			StringBuilder stringBuilder = new StringBuilder("hello update data");
			Proxy.UpdateData(stringBuilder.ToString());
		}

		public void DeleteData(DeleteDataRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder("hello delete data");
			Proxy.DeleteData(stringBuilder.ToString());
		}

		private void DispatchReadDailyTotalRequest(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			dailyTotalRequests[key].DispatchResult(array);
			dailyTotalRequests.Remove(key);
		}

		private void DispatchReadHistoryRequest(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			if (readRequests[key].IsAggregated)
			{
				readRequests[key].DispatchAggregatedResult(array);
			}
			else
			{
				readRequests[key].DispatchReadResult(array);
			}
			readRequests.Remove(key);
		}
	}
}
