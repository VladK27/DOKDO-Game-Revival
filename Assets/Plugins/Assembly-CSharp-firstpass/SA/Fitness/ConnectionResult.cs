using SA.Common.Models;

namespace SA.Fitness
{
	public class ConnectionResult : Result
	{
		public ConnectionResult()
		{
		}

		public ConnectionResult(int resultCode, string message)
			: base(new Error(resultCode, message))
		{
		}
	}
}
