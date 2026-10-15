using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Fitness
{
	public sealed class Sessions : Singleton<Sessions>
	{
		private Dictionary<int, StartSessionRequest> startSessionRequests = new Dictionary<int, StartSessionRequest>();

		private Dictionary<int, StopSessionRequest> stopSessionRequests = new Dictionary<int, StopSessionRequest>();

		private Dictionary<int, ReadSessionRequest> readSessionRequests = new Dictionary<int, ReadSessionRequest>();

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void StartSession(StartSessionRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.Name);
			stringBuilder.Append("|");
			stringBuilder.Append(request.SessionId);
			stringBuilder.Append("|");
			stringBuilder.Append(request.Description);
			stringBuilder.Append("|");
			stringBuilder.Append(request.StartTime);
			stringBuilder.Append("|");
			stringBuilder.Append(request.TimeUnit.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(request.Activity.Value);
			startSessionRequests.Add(request.Id, request);
			Proxy.StartSession(stringBuilder.ToString());
		}

		public void StopSession(StopSessionRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.SessionId);
			stopSessionRequests.Add(request.Id, request);
			Proxy.StopSession(stringBuilder.ToString());
		}

		public void InsertSession()
		{
		}

		public void ReadSession(ReadSessionRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.SessionId);
			stringBuilder.Append("|");
			stringBuilder.Append(request.StartTime);
			stringBuilder.Append("|");
			stringBuilder.Append(request.EndTime);
			stringBuilder.Append("|");
			stringBuilder.Append(request.TimeUnit.ToString());
			stringBuilder.Append("|");
			stringBuilder.Append(request.DataType.Value);
			readSessionRequests.Add(request.Id, request);
			Proxy.ReadSession(stringBuilder.ToString());
		}

		private void SessionStartCallbackHandler(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			int num = int.Parse(array[1]);
			string message = array[2];
			Result result = ((num != 0) ? new Result(new Error(num, message)) : new Result());
			startSessionRequests[key].DispatchSessionStartResult(result);
			startSessionRequests.Remove(key);
		}

		private void SessionStopCallbackHandler(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			stopSessionRequests[key].DispatchResult(array);
			stopSessionRequests.Remove(key);
		}

		private void SessionReadCallbackHandler(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			readSessionRequests[key].DispatchResult(array);
			readSessionRequests.Remove(key);
		}
	}
}
