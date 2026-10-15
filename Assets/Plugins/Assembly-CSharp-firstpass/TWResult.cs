public class TWResult
{
	private bool _IsSucceeded;

	private string _data = string.Empty;

	public bool IsSucceeded
	{
		get
		{
			return _IsSucceeded;
		}
	}

	public string data
	{
		get
		{
			return _data;
		}
	}

	public TWResult(bool IsResSucceeded, string resData)
	{
		_IsSucceeded = IsResSucceeded;
		_data = resData;
	}
}
