using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Fitness
{
	public sealed class Recording : Singleton<Recording>
	{
		private Dictionary<int, SubscribeRequest> subscriptions = new Dictionary<int, SubscribeRequest>();

		private Dictionary<int, UnsubscribeRequest> unsubs = new Dictionary<int, UnsubscribeRequest>();

		private Dictionary<int, SubscriptionsRequest> subsRequests = new Dictionary<int, SubscriptionsRequest>();

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void Subscribe(SubscribeRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.DataType.Value);
			subscriptions.Add(request.Id, request);
			Proxy.Subscribe(stringBuilder.ToString());
		}

		public void ListSubscriptions(SubscriptionsRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append((request.DataType != null) ? request.DataType.Value : string.Empty);
			subsRequests.Add(request.Id, request);
			Proxy.ListSubscriptions(stringBuilder.ToString());
		}

		public void Unsubscribe(UnsubscribeRequest request)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("|");
			stringBuilder.Append(request.DataType.Value);
			unsubs.Add(request.Id, request);
			Proxy.Unsubscribe(stringBuilder.ToString());
		}

		private void SubscribeResultListener(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			int num = int.Parse(array[1]);
			string message = array[2];
			Result result = ((num != 0) ? new Result(new Error(num, message)) : new Result());
			subscriptions[key].DispatchResult(result);
			subscriptions.Remove(key);
		}

		private void ListSubsResultListener(string data)
		{
			string[] array = data.Split(new string[1] { "~" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			subsRequests[key].DispatchRequestResult(array);
			subsRequests.Remove(key);
		}

		private void UnsubResultListener(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			int num = int.Parse(array[1]);
			string message = array[2];
			Result result = ((num != 0) ? new Result(new Error(num, message)) : new Result());
			unsubs[key].DispatchUnsubscribeResult(result);
			unsubs.Remove(key);
		}
	}
}
