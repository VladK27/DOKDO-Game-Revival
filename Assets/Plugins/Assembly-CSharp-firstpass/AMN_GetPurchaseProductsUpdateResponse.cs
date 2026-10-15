public class AMN_GetPurchaseProductsUpdateResponse : AMN_Result
{
	private string _requestId = string.Empty;

	private string _userId = string.Empty;

	private string _marketplace = string.Empty;

	private string _status = string.Empty;

	private bool _hasMore;

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

	public bool HasMore
	{
		get
		{
			return _hasMore;
		}
	}

	public AMN_GetPurchaseProductsUpdateResponse()
		: base(true)
	{
	}
}
