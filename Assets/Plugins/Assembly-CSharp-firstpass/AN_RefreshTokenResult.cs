using SA.Common.Models;

public class AN_RefreshTokenResult : Result
{
	private string _accessToken = string.Empty;

	private string _refreshToken = string.Empty;

	private string _tokenType = string.Empty;

	private long _expiresIn;

	public string AccessToken
	{
		get
		{
			return _accessToken;
		}
	}

	public string RefreshToken
	{
		get
		{
			return _refreshToken;
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

	public AN_RefreshTokenResult(string errorMessage)
		: base(new Error(errorMessage))
	{
	}

	public AN_RefreshTokenResult(string accessToken, string refreshToken, string tokenType, long expiresIn)
	{
		_accessToken = accessToken;
		_refreshToken = refreshToken;
		_tokenType = tokenType;
		_expiresIn = expiresIn;
	}
}
