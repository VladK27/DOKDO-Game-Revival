using SA.Common.Models;

public class AN_AccessTokenResult : Result
{
	private string _accessToken = string.Empty;

	private string _tokenType = string.Empty;

	private long _expiresIn;

	public string AccessToken
	{
		get
		{
			return _accessToken;
		}
	}

	public string TokenType
	{
		get
		{
			return _tokenType;
		}
	}

	public long ExpiresIn
	{
		get
		{
			return _expiresIn;
		}
	}

	public AN_AccessTokenResult(string errorMessage)
		: base(new Error(0, errorMessage))
	{
	}

	public AN_AccessTokenResult(string accessToken, string tokenType, long expiresIn)
	{
		_accessToken = accessToken;
		_tokenType = tokenType;
		_expiresIn = expiresIn;
	}
}
