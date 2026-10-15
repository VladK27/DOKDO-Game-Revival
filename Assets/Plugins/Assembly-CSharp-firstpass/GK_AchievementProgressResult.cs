using SA.Common.Models;

public class GK_AchievementProgressResult : Result
{
	private GK_AchievementTemplate _tpl;

	public GK_AchievementTemplate info
	{
		get
		{
			return _tpl;
		}
	}

	public GK_AchievementTemplate Achievement
	{
		get
		{
			return _tpl;
		}
	}

	public GK_AchievementProgressResult(GK_AchievementTemplate tpl)
	{
		_tpl = tpl;
	}
}
