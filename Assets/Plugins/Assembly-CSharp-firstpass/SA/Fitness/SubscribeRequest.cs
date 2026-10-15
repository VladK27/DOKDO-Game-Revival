using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Util;

namespace SA.Fitness
{
	public class SubscribeRequest
	{
		public class Builder
		{
			private SubscribeRequest request = new SubscribeRequest();

			public Builder SetDataType(DataType dataType)
			{
				request.dataType = dataType;
				return this;
			}

			public SubscribeRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<Result> OnSubscribeFinished__BackingField = delegate
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

		public event Action<Result> OnSubscribeFinished
		{
			add
			{
				Action<Result> action = OnSubscribeFinished__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSubscribeFinished__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<Result> action = OnSubscribeFinished__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSubscribeFinished__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private SubscribeRequest()
		{
		}

		public void DispatchResult(Result result)
		{
			OnSubscribeFinished__BackingField(result);
		}
	}
}
