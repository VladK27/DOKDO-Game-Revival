public class WP8_Result
{
	protected bool _IsSucceeded = true;

	public bool IsSucceeded
	{
		get
		{
			return _IsSucceeded;
		}
	}

	public bool IsFailed
	{
		get
		{
			return !_IsSucceeded;
		}
	}

	public WP8_Result(bool IsResultSucceeded)
	{
		_IsSucceeded = IsResultSucceeded;
	}
}
