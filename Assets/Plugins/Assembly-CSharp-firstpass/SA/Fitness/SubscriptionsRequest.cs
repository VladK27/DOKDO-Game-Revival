using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;

namespace SA.Fitness
{
	public class SubscriptionsRequest
	{
		public class Builder
		{
			private SubscriptionsRequest request = new SubscriptionsRequest();

			public Builder SetDataType(DataType dataType)
			{
				request.dataType = dataType;
				return this;
			}

			public SubscriptionsRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<SubscriptionsRequestResult> OnRequestFinished__BackingField = delegate
		{
		};

		private int id = IdFactory.NextId;

		private DataType dataType;

		public int Id
		{
			get
			{
				return id;
			}
		}

		public DataType DataType
		{
			get
			{
				return dataType;
			}
		}

		public event Action<SubscriptionsRequestResult> OnRequestFinished
		{
			add
			{
				Action<SubscriptionsRequestResult> action = OnRequestFinished__BackingField;
				Action<SubscriptionsRequestResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRequestFinished__BackingField, (Action<SubscriptionsRequestResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<SubscriptionsRequestResult> action = OnRequestFinished__BackingField;
				Action<SubscriptionsRequestResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRequestFinished__BackingField, (Action<SubscriptionsRequestResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private SubscriptionsRequest()
		{
		}

		public void DispatchRequestResult(string[] bundle)
		{
			int num = int.Parse(bundle[1]);
			string message = bundle[2];
			if (num == 0)
			{
				SubscriptionsRequestResult subscriptionsRequestResult = new SubscriptionsRequestResult(id);
				for (int i = 3; i < bundle.Length; i++)
				{
					if (!bundle[i].Equals(string.Empty))
					{
						Subscription subscription = new Subscription(new DataType(bundle[i]));
						subscriptionsRequestResult.AddSubscription(subscription);
					}
				}
			}
			else
			{
				SubscriptionsRequestResult subscriptionsRequestResult = new SubscriptionsRequestResult(id, num, message);
			}
			OnRequestFinished__BackingField(new SubscriptionsRequestResult(id));
		}
	}
}
