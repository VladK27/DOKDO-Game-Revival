using System.Collections.Generic;

public class AMN_GetProductDataResponse : AMN_Result
{
	private string _requestId = string.Empty;

	private List<string> _unavailableSkus;

	private string _status = string.Empty;

	public string RequestId
	{
		get
		{
			return _requestId;
		}
	}

	public List<string> UnavailableSkus
	{
		get
		{
			return _unavailableSkus;
		}
	}

	public string Status
	{
		get
		{
			return _status;
		}
	}

	public AMN_GetProductDataResponse()
		: base(true)
	{
	}
}
