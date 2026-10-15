using System.Collections.Generic;
using SA.Common.Models;

namespace SA.Fitness
{
	public class SensorRequestResult : Result
	{
		private int id;

		private List<DataSource> dataSources = new List<DataSource>();

		public int Id
		{
			get
			{
				return id;
			}
		}

		public List<DataSource> DataSources
		{
			get
			{
				return dataSources;
			}
		}

		public SensorRequestResult(int id)
		{
			this.id = id;
		}

		public SensorRequestResult(int id, int resultCode, string message)
			: base(new Error(resultCode, message))
		{
			this.id = id;
		}

		public void AddDataSource(DataSource source)
		{
			dataSources.Add(source);
		}
	}
}
