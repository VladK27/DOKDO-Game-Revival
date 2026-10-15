public class AMN_SubmitLeaderboardResult : AMN_Result
{
	private string error;

	private string leaderboardID;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public string LeaderboardID
	{
		get
		{
			return leaderboardID;
		}
	}

	public AMN_SubmitLeaderboardResult(bool success)
		: base(success)
	{
	}

	public AMN_SubmitLeaderboardResult(string id, string err)
		: base(false)
	{
		leaderboardID = id;
		error = err;
	}

	public AMN_SubmitLeaderboardResult(string id)
		: base(true)
	{
		leaderboardID = id;
	}
}
