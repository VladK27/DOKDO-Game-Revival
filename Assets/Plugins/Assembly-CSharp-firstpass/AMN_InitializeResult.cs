public class AMN_InitializeResult : AMN_Result
{
	private string error;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public AMN_InitializeResult(bool success)
		: base(success)
	{
	}

	public AMN_InitializeResult(string err)
		: base(false)
	{
		error = err;
	}
}
