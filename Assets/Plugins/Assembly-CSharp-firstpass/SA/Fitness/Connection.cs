using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.Fitness
{
	public sealed class Connection : Singleton<Connection>
	{
		public const string SEPARATOR1 = "|";

		public const string SEPARATOR2 = "~";

		public const string SEPARATOR3 = "$";

		public const string SEPARATOR4 = "%";

		public const string SEPARATOR5 = "^";

		private const int RESULT_OK = -1;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ConnectionResult> OnConnectionFinished__BackingField = delegate
		{
		};

		private List<LoginApi> apis = new List<LoginApi>();

		private List<LoginScope> scopes = new List<LoginScope>();

		private ConnectionState connectionState = ConnectionState.DISCONNECTED;

		private bool shouldManageReconnection;

		public ConnectionState ConnectionState
		{
			get
			{
				return connectionState;
			}
		}

		public event Action<ConnectionResult> OnConnectionFinished
		{
			add
			{
				Action<ConnectionResult> action = OnConnectionFinished__BackingField;
				Action<ConnectionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnConnectionFinished__BackingField, (Action<ConnectionResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<ConnectionResult> action = OnConnectionFinished__BackingField;
				Action<ConnectionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnConnectionFinished__BackingField, (Action<ConnectionResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		private void OnApplicationPause(bool pauseStatus)
		{
			if (shouldManageReconnection)
			{
				if (pauseStatus)
				{
					Disconnect();
				}
				else
				{
					Connect();
				}
			}
		}

		public void AddApi(LoginApi api)
		{
			if (!apis.Contains(api))
			{
				apis.Add(api);
			}
		}

		public void AddScope(LoginScope scope)
		{
			if (!scopes.Contains(scope))
			{
				scopes.Add(scope);
			}
		}

		public void Connect()
		{
			if (apis.Count == 0 || scopes.Count == 0)
			{
				UnityEngine.Debug.LogWarning("[SA.Fitness] Please, define login APIs & Scopes");
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(apis[0].Value);
			for (int i = 1; i < apis.Count; i++)
			{
				stringBuilder.Append("|");
				stringBuilder.Append(apis[i].Value);
			}
			stringBuilder.Append("~");
			stringBuilder.Append(scopes[0].Value);
			for (int j = 1; j < scopes.Count; j++)
			{
				stringBuilder.Append("|");
				stringBuilder.Append(scopes[j].Value);
			}
			connectionState = ConnectionState.CONNECTING;
			Proxy.Connect(stringBuilder.ToString());
		}

		private void Disconnect()
		{
			Proxy.Disconnect();
			connectionState = ConnectionState.DISCONNECTED;
		}

		private void ConnectionResultHandler(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			int num = int.Parse(array[0]);
			ConnectionResult connectionResult = ((num != -1) ? new ConnectionResult(num, array[1]) : new ConnectionResult());
			connectionState = ((!connectionResult.IsSucceeded) ? ConnectionState.DISCONNECTED : ConnectionState.CONNECTED);
			if (!shouldManageReconnection)
			{
				OnConnectionFinished__BackingField(connectionResult);
			}
			shouldManageReconnection = connectionResult.IsSucceeded;
		}
	}
}
