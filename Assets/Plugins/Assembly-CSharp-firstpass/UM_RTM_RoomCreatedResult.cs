public class UM_RTM_RoomCreatedResult
{
	private bool _IsSuccess;

	public bool IsSuccess
	{
		get
		{
			return _IsSuccess;
		}
	}

	public UM_RTM_RoomCreatedResult(GP_GamesStatusCodes status)
	{
		_IsSuccess = status == GP_GamesStatusCodes.STATUS_OK;
	}

	public UM_RTM_RoomCreatedResult(GK_RTM_MatchStartedResult result)
	{
		_IsSuccess = result.IsSucceeded;
	}
}
