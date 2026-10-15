using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;

namespace SA.Fitness
{
	public class SensorRequest
	{
		public class Builder
		{
			private SensorRequest request = new SensorRequest();

			public Builder AddDataType(DataType dataType)
			{
				if (!request.dataTypes.Contains(dataType))
				{
					request.dataTypes.Add(dataType);
				}
				return this;
			}

			public Builder AddDataSourceType(DataSourceType dataSourceType)
			{
				if (!request.dataSourceTypes.Contains(dataSourceType))
				{
					request.DataSourceTypes.Add(dataSourceType);
				}
				return this;
			}

			public SensorRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<SensorRequestResult> OnRequestFinished__BackingField = delegate
		{
		};

		private int id;

		private List<DataType> dataTypes = new List<DataType>();

		private List<DataSourceType> dataSourceTypes = new List<DataSourceType>();

		public int Id
		{
			get
			{
				return id;
			}
		}

		public List<DataType> DataTypes
		{
			get
			{
				return dataTypes;
			}
		}

		public List<DataSourceType> DataSourceTypes
		{
			get
			{
				return dataSourceTypes;
			}
		}

		public event Action<SensorRequestResult> OnRequestFinished
		{
			add
			{
				Action<SensorRequestResult> action = OnRequestFinished__BackingField;
				Action<SensorRequestResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRequestFinished__BackingField, (Action<SensorRequestResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<SensorRequestResult> action = OnRequestFinished__BackingField;
				Action<SensorRequestResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRequestFinished__BackingField, (Action<SensorRequestResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private SensorRequest()
		{
			id = IdFactory.NextId;
		}

		public void DispatchResult(string[] bundle)
		{
			int num = int.Parse(bundle[1]);
			string message = bundle[2];
			SensorRequestResult sensorRequestResult = ((num != 0) ? new SensorRequestResult(id, num, message) : new SensorRequestResult(id));
			for (int i = 3; i < bundle.Length; i++)
			{
				string[] bundle2 = bundle[i].Split(new string[1] { "|" }, StringSplitOptions.None);
				DataSource source = new DataSource(bundle2);
				sensorRequestResult.AddDataSource(source);
			}
			OnRequestFinished__BackingField(sensorRequestResult);
		}
	}
}
