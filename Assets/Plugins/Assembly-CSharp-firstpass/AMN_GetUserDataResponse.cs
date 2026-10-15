public class AMN_GetUserDataResponse : AMN_Result
{
	private string _requestId = string.Empty;

	private string _userId = string.Empty;

	private string _marketplace = string.Empty;

	private string _status = string.Empty;

	public string RequestId
	{
		get
		{
			return _requestId;
		}
	}

	public string UserId
	{
		get
		{
			return _userId;
		}
	}

	public string Marketplace
	{
		get
		{
			return _marketplace;
		}
	}

	public string Status
	{
		get
		{
			return _status;
		}
	}

	public AMN_GetUserDataResponse()
		: base(true)
	{
	}
}
