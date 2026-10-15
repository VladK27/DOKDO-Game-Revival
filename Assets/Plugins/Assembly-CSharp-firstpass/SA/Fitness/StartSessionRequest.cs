using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Util;

namespace SA.Fitness
{
	public class StartSessionRequest
	{
		public class Builder
		{
			private StartSessionRequest request = new StartSessionRequest();

			public Builder SetName(string name)
			{
				request.name = name;
				return this;
			}

			public Builder SetIdentifier(string id)
			{
				request.sessionId = id;
				return this;
			}

			public Builder SetDescription(string description)
			{
				request.description = description;
				return this;
			}

			public Builder SetStartTime(long startTime, TimeUnit timeUnit)
			{
				request.startTime = startTime;
				request.timeUnit = timeUnit;
				return this;
			}

			public Builder SetActivity(Activity activity)
			{
				request.activity = activity;
				return this;
			}

			public StartSessionRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<Result> OnSessionStarted__BackingField = delegate
		{
		};

		private int id = IdFactory.NextId;

		private string name = string.Empty;

		private string sessionId = string.Empty;

		private string description = string.Empty;

		private long startTime;

		private TimeUnit timeUnit;

		private Activity activity = Activity.UNKNOWN;

		public int Id
		{
			get
			{
				return id;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
		}

		public string SessionId
		{
			get
			{
				return sessionId;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}
		}

		public long StartTime
		{
			get
			{
				return startTime;
			}
		}

		public TimeUnit TimeUnit
		{
			get
			{
				return timeUnit;
			}
		}

		public Activity Activity
		{
			get
			{
				return activity;
			}
		}

		public event Action<Result> OnSessionStarted
		{
			add
			{
				Action<Result> action = OnSessionStarted__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionStarted__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<Result> action = OnSessionStarted__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionStarted__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private StartSessionRequest()
		{
		}

		public void DispatchSessionStartResult(Result result)
		{
			OnSessionStarted__BackingField(result);
		}
	}
}
