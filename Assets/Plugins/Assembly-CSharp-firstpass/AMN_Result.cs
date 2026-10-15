public class AMN_Result
{
	private bool _isSuccess;

	public bool isSuccess
	{
		get
		{
			return _isSuccess;
		}
	}

	public bool isFailure
	{
		get
		{
			return !isSuccess;
		}
	}

	public AMN_Result(bool success)
	{
		_isSuccess = success;
	}
}
