namespace SA.Fitness
{
	public class Subscription
	{
		private DataType dataType;

		public DataType DataType
		{
			get
			{
				return dataType;
			}
		}

		public Subscription(DataType dataType)
		{
			this.dataType = dataType;
		}
	}
}
