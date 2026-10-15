using System;

public class GooglePlayGiftRequestResult
{
	private GP_GamesActivityResultCodes _code;

	public GP_GamesActivityResultCodes code
	{
		get
		{
			return _code;
		}
	}

	public bool isSuccess
	{
		get
		{
			return _code == GP_GamesActivityResultCodes.RESULT_OK;
		}
	}

	public bool isFailure
	{
		get
		{
			return !isSuccess;
		}
	}

	public GooglePlayGiftRequestResult(string r_code)
	{
		_code = (GP_GamesActivityResultCodes)Convert.ToInt32(r_code);
	}
}
