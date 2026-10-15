using System.Collections.Generic;

namespace SA.Fitness
{
	public class Session
	{
		public string Id = string.Empty;

		public long StartTime;

		public long EndTime;

		public string Name = string.Empty;

		public string Description = string.Empty;

		public string AppPackageName = string.Empty;

		public Activity Activity = Activity.UNKNOWN;

		private List<DataSet> dataSets = new List<DataSet>();

		public List<DataSet> DataSets
		{
			get
			{
				return dataSets;
			}
		}

		public void AddDataSet(DataSet data)
		{
			dataSets.Add(data);
		}
	}
}
