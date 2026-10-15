using System.Collections.Generic;

namespace SA.Fitness
{
	public class DataSet
	{
		private DataType dataType;

		private List<DataPoint> dataPoints = new List<DataPoint>();

		public DataType DataType
		{
			get
			{
				return dataType;
			}
		}

		public List<DataPoint> DataPoints
		{
			get
			{
				return dataPoints;
			}
		}

		public DataSet(DataType dataType)
		{
			this.dataType = dataType;
		}

		internal void AddDataPoint(DataPoint dataPoint)
		{
			dataPoints.Add(dataPoint);
		}
	}
}
