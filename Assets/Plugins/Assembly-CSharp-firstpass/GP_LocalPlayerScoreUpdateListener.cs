using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

public class GP_LocalPlayerScoreUpdateListener
{
	private int _RequestId;

	private string _leaderboardId;

	private string _ErrorData;

	private List<GPScore> Scores = new List<GPScore>();

	public int RequestId
	{
		get
		{
			return _RequestId;
		}
	}

	public GP_LocalPlayerScoreUpdateListener(int requestId, string leaderboardId)
	{
		_RequestId = requestId;
		_leaderboardId = leaderboardId;
	}

	public void ReportScoreUpdate(GPScore score)
	{
		Scores.Add(score);
		DispatchUpdate();
	}

	public void ReportScoreUpdateFail(string errorData)
	{
		Debug.Log("ReportScoreUpdateFail");
		_ErrorData = errorData;
		Scores.Add(null);
		DispatchUpdate();
	}

	private void DispatchUpdate()
	{
		if (Scores.Count == 6)
		{
			GPLeaderBoard leaderBoard = Singleton<GooglePlayManager>.Instance.GetLeaderBoard(_leaderboardId);
			GP_LeaderboardResult result;
			if (_ErrorData != null)
			{
				result = new GP_LeaderboardResult(leaderBoard, _ErrorData);
			}
			else
			{
				leaderBoard.UpdateCurrentPlayerScore(Scores);
				result = new GP_LeaderboardResult(leaderBoard, _ErrorData);
			}
			Singleton<GooglePlayManager>.Instance.DispatchLeaderboardUpdateEvent(result);
		}
	}
}
