using SA.Common.Models;

public class SK_RequestStorefrontIdentifierResult : Result
{
	private string _StorefrontIdentifier = string.Empty;

	public string StorefrontIdentifier
	{
		get
		{
			return _StorefrontIdentifier;
		}
		set
		{
			_StorefrontIdentifier = value;
		}
	}

	public SK_RequestStorefrontIdentifierResult()
	{
	}

	public SK_RequestStorefrontIdentifierResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
