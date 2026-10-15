using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;

namespace SA.Fitness
{
	public class StopSessionRequest
	{
		public class Builder
		{
			private StopSessionRequest request = new StopSessionRequest();

			public Builder SetIdentifier(string sessionId)
			{
				request.sessionId = sessionId;
				return this;
			}

			public StopSessionRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<StopSessionResult> OnSessionStopped__BackingField = delegate
		{
		};

		private int id = IdFactory.NextId;

		private string sessionId = string.Empty;

		public int Id
		{
			get
			{
				return id;
			}
		}

		public string SessionId
		{
			get
			{
				return sessionId;
			}
		}

		public event Action<StopSessionResult> OnSessionStopped
		{
			add
			{
				Action<StopSessionResult> action = OnSessionStopped__BackingField;
				Action<StopSessionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionStopped__BackingField, (Action<StopSessionResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<StopSessionResult> action = OnSessionStopped__BackingField;
				Action<StopSessionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionStopped__BackingField, (Action<StopSessionResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private StopSessionRequest()
		{
		}

		public void DispatchResult(string[] bundle)
		{
			int num = int.Parse(bundle[1]);
			StopSessionResult stopSessionResult = ((num != 0) ? new StopSessionResult(id, num, bundle[2]) : new StopSessionResult(id));
			if (stopSessionResult.IsSucceeded)
			{
				for (int i = 2; i < bundle.Length; i++)
				{
					string[] array = bundle[i].Split(new string[1] { "~" }, StringSplitOptions.None);
					Session session = new Session();
					session.StartTime = long.Parse(array[0]);
					session.EndTime = long.Parse(array[1]);
					session.Name = array[2];
					session.Id = array[3];
					session.Description = array[4];
					session.Activity = new Activity(array[5]);
					session.AppPackageName = array[6];
					stopSessionResult.AddSession(session);
				}
			}
			OnSessionStopped__BackingField(stopSessionResult);
		}
	}
}
