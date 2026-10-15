using System.Collections.Generic;

public class AMN_RequestAchievementsResult : AMN_Result
{
	private string error;

	private List<GC_Achievement> achievementList;

	public string Error
	{
		get
		{
			return error;
		}
	}

	public List<GC_Achievement> AchievementList
	{
		get
		{
			return achievementList;
		}
	}

	public AMN_RequestAchievementsResult(bool success)
		: base(success)
	{
	}

	public AMN_RequestAchievementsResult(string err)
		: base(false)
	{
		error = err;
	}

	public AMN_RequestAchievementsResult(List<GC_Achievement> list)
		: base(true)
	{
		achievementList = list;
	}
}
