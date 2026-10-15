using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Util;

namespace SA.Fitness
{
	public class ReadSessionRequest
	{
		public class Builder
		{
			private ReadSessionRequest request = new ReadSessionRequest();

			public Builder SetTimeinterval(long startTime, long endTime, TimeUnit timeUnit)
			{
				request.startTime = startTime;
				request.endTime = endTime;
				request.timeUnit = timeUnit;
				return this;
			}

			public Builder SetIdentifier(string sessionId)
			{
				request.sessionId = sessionId;
				return this;
			}

			public Builder SetDataTypeToRead(DataType dataType)
			{
				request.dataType = dataType;
				return this;
			}

			public ReadSessionRequest Build()
			{
				return request;
			}
		}

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ReadSessionResult> OnSessionReadFinished__BackingField = delegate
		{
		};

		private long startTime;

		private long endTime;

		private TimeUnit timeUnit = TimeUnit.Milliseconds;

		private string sessionId = string.Empty;

		private DataType dataType;

		private int id = IdFactory.NextId;

		public int Id
		{
			get
			{
				return id;
			}
		}

		public long StartTime
		{
			get
			{
				return startTime;
			}
		}

		public long EndTime
		{
			get
			{
				return endTime;
			}
		}

		public TimeUnit TimeUnit
		{
			get
			{
				return timeUnit;
			}
		}

		public string SessionId
		{
			get
			{
				return sessionId;
			}
		}

		public DataType DataType
		{
			get
			{
				return dataType;
			}
		}

		public event Action<ReadSessionResult> OnSessionReadFinished
		{
			add
			{
				Action<ReadSessionResult> action = OnSessionReadFinished__BackingField;
				Action<ReadSessionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionReadFinished__BackingField, (Action<ReadSessionResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<ReadSessionResult> action = OnSessionReadFinished__BackingField;
				Action<ReadSessionResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnSessionReadFinished__BackingField, (Action<ReadSessionResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private ReadSessionRequest()
		{
		}

		public void DispatchResult(string[] bundle)
		{
			int num = int.Parse(bundle[1]);
			ReadSessionResult readSessionResult = ((num != 0) ? new ReadSessionResult(id, num, bundle[2]) : new ReadSessionResult(id));
			if (readSessionResult.IsSucceeded)
			{
				for (int i = 3; i < bundle.Length; i++)
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
					if (array.Length >= 8)
					{
						for (int j = 7; j < array.Length; j++)
						{
							string[] array2 = array[j].Split(new string[1] { "$" }, StringSplitOptions.None);
							DataSet dataSet = new DataSet(new DataType(array2[0]));
							if (array2.Length > 1)
							{
								for (int k = 1; k < array2.Length; k++)
								{
									string[] array3 = array2[k].Split(new string[1] { "%" }, StringSplitOptions.None);
									List<string> list = new List<string>();
									list.Add(array3[0]);
									list.Add(array3[1]);
									list.Add(array3[2]);
									list.Add(array3[3]);
									DataPoint dataPoint = new DataPoint(new DataType(array3[0]), list.ToArray(), "^");
									dataSet.AddDataPoint(dataPoint);
								}
							}
							session.AddDataSet(dataSet);
						}
					}
					readSessionResult.AddSession(session);
				}
			}
			OnSessionReadFinished__BackingField(readSessionResult);
		}
	}
}
