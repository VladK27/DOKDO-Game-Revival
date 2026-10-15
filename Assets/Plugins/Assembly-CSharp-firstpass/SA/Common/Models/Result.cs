namespace SA.Common.Models
{
	public class Result
	{
		protected Error _Error;

		public Error Error
		{
			get
			{
				return _Error;
			}
		}

		public bool HasError
		{
			get
			{
				if (_Error == null)
				{
					return false;
				}
				return true;
			}
		}

		public bool IsSucceeded
		{
			get
			{
				return !HasError;
			}
		}

		public bool IsFailed
		{
			get
			{
				return HasError;
			}
		}

		public Result()
		{
		}

		public Result(Error error)
		{
			_Error = error;
		}
	}
}
