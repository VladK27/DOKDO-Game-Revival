using System.Collections.Generic;

namespace SA.Fitness
{
	public sealed class Bucket
	{
		public enum Type
		{
			ActivitySegment = 4,
			ActivityType = 3,
			Session = 2,
			Time = 1
		}

		private List<DataSet> dataSets = new List<DataSet>();

		private Type type = Type.Time;

		private long startTime;

		private long endTime = 10L;

		public Type Bucketing
		{
			get
			{
				return type;
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

		public List<DataSet> DataSets
		{
			get
			{
				return dataSets;
			}
		}

		public Bucket(Type type)
		{
			this.type = type;
		}

		public void SetTimeRange(long startTime, long endTime)
		{
			this.startTime = startTime;
			this.endTime = endTime;
		}

		public void AddDataSet(DataSet dataSet)
		{
			dataSets.Add(dataSet);
		}
	}
}
