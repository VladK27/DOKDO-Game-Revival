using System;
using System.Collections.Generic;

[Serializable]
public class GPScoreCollection
{
	public Dictionary<int, GPScore> AllTimeScores = new Dictionary<int, GPScore>();

	public Dictionary<int, GPScore> WeekScores = new Dictionary<int, GPScore>();

	public Dictionary<int, GPScore> TodayScores = new Dictionary<int, GPScore>();
}
