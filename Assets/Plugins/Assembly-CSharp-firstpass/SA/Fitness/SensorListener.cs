using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;

namespace SA.Fitness
{
	public class SensorListener
	{
		public class Builder
		{
			private SensorListener listener = new SensorListener();

			public Builder SetDataType(DataType dataType)
			{
				listener.dataType = dataType;
				return this;
			}

			public Builder SetSamplingRate(long amount, TimeUnit unit)
			{
				listener.rateAmount = amount;
				listener.rateTimeUnit = unit;
				return this;
			}

			public SensorListener Build()
			{
				return listener;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<int> OnRegisterSuccess__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<int> OnRegisterFail__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<int, DataPoint> OnDataPointReceived__BackingField = delegate
		{
		};

		private int id;

		private DataType dataType;

		private long rateAmount;

		private TimeUnit rateTimeUnit;

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

		public long RateAmount
		{
			get
			{
				return rateAmount;
			}
		}

		public TimeUnit RateTimeUnit
		{
			get
			{
				return rateTimeUnit;
			}
		}

		public event Action<int> OnRegisterSuccess
		{
			add
			{
				Action<int> action = OnRegisterSuccess__BackingField;
				Action<int> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRegisterSuccess__BackingField, (Action<int>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<int> action = OnRegisterSuccess__BackingField;
				Action<int> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRegisterSuccess__BackingField, (Action<int>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<int> OnRegisterFail
		{
			add
			{
				Action<int> action = OnRegisterFail__BackingField;
				Action<int> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRegisterFail__BackingField, (Action<int>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<int> action = OnRegisterFail__BackingField;
				Action<int> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRegisterFail__BackingField, (Action<int>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action<int, DataPoint> OnDataPointReceived
		{
			add
			{
				Action<int, DataPoint> action = OnDataPointReceived__BackingField;
				Action<int, DataPoint> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnDataPointReceived__BackingField, (Action<int, DataPoint>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<int, DataPoint> action = OnDataPointReceived__BackingField;
				Action<int, DataPoint> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnDataPointReceived__BackingField, (Action<int, DataPoint>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private SensorListener()
		{
			id = IdFactory.NextId;
		}

		public void DispatchRegisterSuccess()
		{
			OnRegisterSuccess__BackingField(id);
		}

		public void DispatchRegisterFail()
		{
			OnRegisterFail__BackingField(id);
		}

		public void DispatchDataPointEvent(string[] bundle)
		{
			OnDataPointReceived__BackingField(id, new DataPoint(dataType, bundle, "|"));
		}
	}
}
