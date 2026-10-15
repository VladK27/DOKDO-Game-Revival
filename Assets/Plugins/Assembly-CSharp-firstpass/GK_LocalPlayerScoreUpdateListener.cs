using System.Collections.Generic;
using SA.Common.Models;

public class GK_LocalPlayerScoreUpdateListener
{
	private int _RequestId;

	private bool _IsInternal;

	private string _leaderboardId;

	private string _ErrorData;

	private List<GK_Score> Scores = new List<GK_Score>();

	public int RequestId
	{
		get
		{
			return _RequestId;
		}
	}

	public GK_LocalPlayerScoreUpdateListener(int requestId, string leaderboardId, bool isInternal)
	{
		_RequestId = requestId;
		_leaderboardId = leaderboardId;
		_IsInternal = isInternal;
	}

	public void ReportScoreUpdate(GK_Score score)
	{
		Scores.Add(score);
		DispatchUpdate();
	}

	public void ReportScoreUpdateFail(string errorData)
	{
		ISN_Logger.Log("ReportScoreUpdateFail");
		_ErrorData = errorData;
		Scores.Add(null);
		DispatchUpdate();
	}

	private void DispatchUpdate()
	{
		if (Scores.Count == 6)
		{
			GK_Leaderboard leaderboard = GameCenterManager.GetLeaderboard(_leaderboardId);
			GK_LeaderboardResult result;
			if (_ErrorData != null)
			{
				result = new GK_LeaderboardResult(leaderboard, new Error(_ErrorData));
			}
			else
			{
				leaderboard.UpdateCurrentPlayerScore(Scores);
				result = new GK_LeaderboardResult(leaderboard);
			}
			GameCenterManager.DispatchLeaderboardUpdateEvent(result, _IsInternal);
		}
	}
}
