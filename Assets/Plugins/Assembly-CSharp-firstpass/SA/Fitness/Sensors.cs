using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Fitness
{
	public sealed class Sensors : Singleton<Sensors>
	{
		private Dictionary<int, SensorRequest> requests = new Dictionary<int, SensorRequest>();

		private Dictionary<int, SensorListener> listeners = new Dictionary<int, SensorListener>();

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void RequestSensors(SensorRequest request)
		{
			if (request.DataTypes.Count == 0 || request.DataSourceTypes.Count == 0)
			{
				Debug.LogWarning("[SA.Fitness] Sensore Request should be setup correctly!");
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(request.Id);
			stringBuilder.Append("~");
			stringBuilder.Append(request.DataTypes[0].Value);
			for (int i = 1; i < request.DataTypes.Count; i++)
			{
				stringBuilder.Append("|");
				stringBuilder.Append(request.DataTypes[i].Value);
			}
			stringBuilder.Append("~");
			stringBuilder.Append((int)request.DataSourceTypes[0]);
			for (int j = 1; j < request.DataSourceTypes.Count; j++)
			{
				stringBuilder.Append("|");
				stringBuilder.Append((int)request.DataSourceTypes[j]);
			}
			requests.Add(request.Id, request);
			Proxy.RequestDataSources(stringBuilder.ToString());
		}

		public void RegisterSensorListener(SensorListener listener)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(listener.Id);
			stringBuilder.Append("~");
			stringBuilder.Append(listener.DataType.Value);
			stringBuilder.Append("~");
			stringBuilder.Append(listener.RateAmount.ToString());
			stringBuilder.Append("~");
			stringBuilder.Append(listener.RateTimeUnit.ToString());
			listeners.Add(listener.Id, listener);
			Proxy.RegisterSensorListener(stringBuilder.ToString());
		}

		private void SensorsRequestResultHandler(string data)
		{
			string[] array = data.Split(new string[1] { "~" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			if (requests.ContainsKey(key))
			{
				requests[key].DispatchResult(array);
				requests.Remove(key);
			}
		}

		private void SensorListenerRegisterSuccessHandler(string data)
		{
			int key = int.Parse(data);
			if (listeners.ContainsKey(key))
			{
				listeners[key].DispatchRegisterSuccess();
			}
		}

		private void SensorListenerRegisterFailHandler(string data)
		{
			int key = int.Parse(data);
			if (listeners.ContainsKey(key))
			{
				listeners[key].DispatchRegisterFail();
			}
		}

		private void SensorListenerDataPointHandler(string data)
		{
			string[] array = data.Split(new string[1] { "~" }, StringSplitOptions.None);
			int key = int.Parse(array[0]);
			if (listeners.ContainsKey(key))
			{
				listeners[key].DispatchDataPointEvent(array);
			}
		}
	}
}
