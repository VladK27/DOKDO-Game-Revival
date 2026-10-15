internal static class GC_ScoreTimeSpanMethods
{
	public static UM_TimeSpan GetUMScore(this GC_ScoreTimeSpan timeSpan)
	{
		switch (timeSpan)
		{
		case GC_ScoreTimeSpan.ALL_TIME:
			return UM_TimeSpan.ALL_TIME;
		case GC_ScoreTimeSpan.WEEK:
			return UM_TimeSpan.WEEK;
		case GC_ScoreTimeSpan.TODAY:
			return UM_TimeSpan.TODAY;
		default:
			return UM_TimeSpan.ALL_TIME;
		}
	}
}
