internal static class UM_TimeSpanMethods
{
	public static GK_TimeSpan Get_GK_TimeSpan(this UM_TimeSpan type)
	{
		switch (type)
		{
		case UM_TimeSpan.ALL_TIME:
			return GK_TimeSpan.ALL_TIME;
		case UM_TimeSpan.TODAY:
			return GK_TimeSpan.TODAY;
		case UM_TimeSpan.WEEK:
			return GK_TimeSpan.WEEK;
		default:
			return GK_TimeSpan.ALL_TIME;
		}
	}

	public static GPBoardTimeSpan Get_GP_TimeSpan(this UM_TimeSpan type)
	{
		switch (type)
		{
		case UM_TimeSpan.ALL_TIME:
			return GPBoardTimeSpan.ALL_TIME;
		case UM_TimeSpan.TODAY:
			return GPBoardTimeSpan.TODAY;
		case UM_TimeSpan.WEEK:
			return GPBoardTimeSpan.WEEK;
		default:
			return GPBoardTimeSpan.ALL_TIME;
		}
	}

	public static GC_ScoreTimeSpan Get_GC_TimeSpan(this UM_TimeSpan type)
	{
		switch (type)
		{
		case UM_TimeSpan.ALL_TIME:
			return GC_ScoreTimeSpan.ALL_TIME;
		case UM_TimeSpan.WEEK:
			return GC_ScoreTimeSpan.WEEK;
		case UM_TimeSpan.TODAY:
			return GC_ScoreTimeSpan.TODAY;
		default:
			return GC_ScoreTimeSpan.ALL_TIME;
		}
	}
}
