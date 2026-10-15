using System;

public class GooglePlayResult
{
	private GP_GamesStatusCodes _response;

	private string _message;

	[Obsolete("response is deprecated, please use Response instead.")]
	public GP_GamesStatusCodes response
	{
		get
		{
			return Response;
		}
	}

	public GP_GamesStatusCodes Response
	{
		get
		{
			return _response;
		}
	}

	[Obsolete("message is deprecated, please use Message instead.")]
	public string message
	{
		get
		{
			return Message;
		}
	}

	public string Message
	{
		get
		{
			return _message;
		}
	}

	[Obsolete("isSuccess is deprecated, please use IsSucceeded instead.")]
	public bool isSuccess
	{
		get
		{
			return IsSucceeded;
		}
	}

	public bool IsSucceeded
	{
		get
		{
			return _response == GP_GamesStatusCodes.STATUS_OK;
		}
	}

	[Obsolete("isFailure is deprecated, please use IsFailed instead.")]
	public bool isFailure
	{
		get
		{
			return IsFailed;
		}
	}

	public bool IsFailed
	{
		get
		{
			return !IsSucceeded;
		}
	}

	public GooglePlayResult(GP_GamesStatusCodes code)
	{
		SetCode(code);
	}

	public GooglePlayResult(string code)
	{
		SetCode((GP_GamesStatusCodes)Convert.ToInt32(code));
	}

	private void SetCode(GP_GamesStatusCodes code)
	{
		_response = code;
		_message = _response.ToString();
	}
}
