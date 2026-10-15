using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Util;

namespace SA.Fitness
{
	public class UnsubscribeRequest
	{
		public class Builder
		{
			private UnsubscribeRequest request = new UnsubscribeRequest();

			public Builder SetDataType(DataType dataType)
			{
				request.dataType = dataType;
				return this;
			}

			public UnsubscribeRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<Result> OnUnsubscribeFinished__BackingField = delegate
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

		public event Action<Result> OnUnsubscribeFinished
		{
			add
			{
				Action<Result> action = OnUnsubscribeFinished__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnUnsubscribeFinished__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<Result> action = OnUnsubscribeFinished__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnUnsubscribeFinished__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private UnsubscribeRequest()
		{
		}

		public void DispatchUnsubscribeResult(Result result)
		{
			OnUnsubscribeFinished__BackingField(result);
		}
	}
}
