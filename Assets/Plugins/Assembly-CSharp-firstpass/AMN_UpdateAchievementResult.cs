public class AMN_UpdateAchievementResult : AMN_Result
{
	private string error;

	private string achievementID;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public string AchievementID
	{
		get
		{
			return achievementID;
		}
	}

	public AMN_UpdateAchievementResult(bool success)
		: base(success)
	{
	}

	public AMN_UpdateAchievementResult(string id, string err)
		: base(false)
	{
		achievementID = id;
		error = err;
	}

	public AMN_UpdateAchievementResult(string id)
		: base(true)
	{
		achievementID = id;
	}
}
