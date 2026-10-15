public static class PlayServiceUtil
{
	public static GPAchievementState GetAchievementStateById(int code)
	{
		switch (code)
		{
		case 0:
			return GPAchievementState.STATE_UNLOCKED;
		case 1:
			return GPAchievementState.STATE_REVEALED;
		default:
			return GPAchievementState.STATE_HIDDEN;
		}
	}

	public static GPAchievementType GetAchievementTypeById(int code)
	{
		if (code == 0)
		{
			return GPAchievementType.TYPE_STANDARD;
		}
		return GPAchievementType.TYPE_INCREMENTAL;
	}
}
