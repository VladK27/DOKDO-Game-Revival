public class AMN_InterstitialDismissedResult : AMN_Result
{
	private string _error_message = "no_error";

	public string Error_message
	{
		get
		{
			return _error_message;
		}
		set
		{
			_error_message = value;
		}
	}

	public AMN_InterstitialDismissedResult(string error_message)
		: base(false)
	{
		Error_message = error_message;
	}
}
