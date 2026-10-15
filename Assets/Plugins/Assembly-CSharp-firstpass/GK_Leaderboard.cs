using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GK_Leaderboard
{
	public bool IsOpen = true;

	private bool _CurrentPlayerScoreLoaded;

	public GK_ScoreCollection SocsialCollection = new GK_ScoreCollection();

	public GK_ScoreCollection GlobalCollection = new GK_ScoreCollection();

	private List<GK_Score> CurrentPlayerScore = new List<GK_Score>();

	private Dictionary<int, GK_LocalPlayerScoreUpdateListener> ScoreUpdateListners = new Dictionary<int, GK_LocalPlayerScoreUpdateListener>();

	[SerializeField]
	private GK_LeaderBoardInfo _info;

	public GK_LeaderBoardInfo Info
	{
		get
		{
			return _info;
		}
	}

	public string Id
	{
		get
		{
			return _info.Identifier;
		}
	}

	public bool CurrentPlayerScoreLoaded
	{
		get
		{
			return _CurrentPlayerScoreLoaded;
		}
	}

	[Obsolete("id is depreciated, plase use Id instead")]
	public string id
	{
		get
		{
			return _info.Identifier;
		}
	}

	public GK_Leaderboard(string leaderboardId)
	{
		_info = new GK_LeaderBoardInfo();
		_info.Identifier = leaderboardId;
	}

	public void Refresh()
	{
		SocsialCollection = new GK_ScoreCollection();
		GlobalCollection = new GK_ScoreCollection();
		CurrentPlayerScore = new List<GK_Score>();
		ScoreUpdateListners = new Dictionary<int, GK_LocalPlayerScoreUpdateListener>();
	}

	public GK_Score GetCurrentPlayerScore(GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		foreach (GK_Score item in CurrentPlayerScore)
		{
			if (item.TimeSpan == timeSpan && item.Collection == collection)
			{
				return item;
			}
		}
		return null;
	}

	public GK_Score GetScoreByPlayerId(string playerId, GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		if (playerId.Equals(GameCenterManager.Player.Id))
		{
			return GetCurrentPlayerScore(timeSpan, collection);
		}
		List<GK_Score> scoresList = GetScoresList(timeSpan, collection);
		foreach (GK_Score item in scoresList)
		{
			if (item.PlayerId.Equals(playerId))
			{
				return item;
			}
		}
		return null;
	}

	public List<GK_Score> GetScoresList(GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		GK_ScoreCollection gK_ScoreCollection = GlobalCollection;
		switch (collection)
		{
		case GK_CollectionType.GLOBAL:
			gK_ScoreCollection = GlobalCollection;
			break;
		case GK_CollectionType.FRIENDS:
			gK_ScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GK_Score> dictionary = gK_ScoreCollection.AllTimeScores;
		switch (timeSpan)
		{
		case GK_TimeSpan.ALL_TIME:
			dictionary = gK_ScoreCollection.AllTimeScores;
			break;
		case GK_TimeSpan.TODAY:
			dictionary = gK_ScoreCollection.TodayScores;
			break;
		case GK_TimeSpan.WEEK:
			dictionary = gK_ScoreCollection.WeekScores;
			break;
		}
		List<GK_Score> list = new List<GK_Score>();
		list.AddRange(dictionary.Values);
		return list;
	}

	public GK_Score GetScore(int rank, GK_TimeSpan timeSpan, GK_CollectionType collection)
	{
		GK_ScoreCollection gK_ScoreCollection = GlobalCollection;
		switch (collection)
		{
		case GK_CollectionType.GLOBAL:
			gK_ScoreCollection = GlobalCollection;
			break;
		case GK_CollectionType.FRIENDS:
			gK_ScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GK_Score> dictionary = gK_ScoreCollection.AllTimeScores;
		switch (timeSpan)
		{
		case GK_TimeSpan.ALL_TIME:
			dictionary = gK_ScoreCollection.AllTimeScores;
			break;
		case GK_TimeSpan.TODAY:
			dictionary = gK_ScoreCollection.TodayScores;
			break;
		case GK_TimeSpan.WEEK:
			dictionary = gK_ScoreCollection.WeekScores;
			break;
		}
		if (dictionary.ContainsKey(rank))
		{
			return dictionary[rank];
		}
		return null;
	}

	public void CreateScoreListener(int requestId, bool isInternal)
	{
		GK_LocalPlayerScoreUpdateListener gK_LocalPlayerScoreUpdateListener = new GK_LocalPlayerScoreUpdateListener(requestId, Id, isInternal);
		ScoreUpdateListners.Add(gK_LocalPlayerScoreUpdateListener.RequestId, gK_LocalPlayerScoreUpdateListener);
	}

	public void ReportLocalPlayerScoreUpdate(GK_Score score, int requestId)
	{
		GK_LocalPlayerScoreUpdateListener gK_LocalPlayerScoreUpdateListener = ScoreUpdateListners[requestId];
		gK_LocalPlayerScoreUpdateListener.ReportScoreUpdate(score);
	}

	public void UpdateCurrentPlayerScore(List<GK_Score> newScores)
	{
		CurrentPlayerScore.Clear();
		foreach (GK_Score newScore in newScores)
		{
			CurrentPlayerScore.Add(newScore);
		}
		_CurrentPlayerScoreLoaded = true;
	}

	public void UpdateCurrentPlayerScore(GK_Score score)
	{
		GK_Score currentPlayerScore = GetCurrentPlayerScore(score.TimeSpan, score.Collection);
		if (currentPlayerScore != null)
		{
			CurrentPlayerScore.Remove(currentPlayerScore);
		}
		CurrentPlayerScore.Add(score);
		_CurrentPlayerScoreLoaded = true;
	}

	public void ReportLocalPlayerScoreUpdateFail(string errorData, int requestId)
	{
		GK_LocalPlayerScoreUpdateListener gK_LocalPlayerScoreUpdateListener = ScoreUpdateListners[requestId];
		gK_LocalPlayerScoreUpdateListener.ReportScoreUpdateFail(errorData);
	}

	public void UpdateScore(GK_Score s)
	{
		GK_ScoreCollection gK_ScoreCollection = GlobalCollection;
		switch (s.Collection)
		{
		case GK_CollectionType.GLOBAL:
			gK_ScoreCollection = GlobalCollection;
			break;
		case GK_CollectionType.FRIENDS:
			gK_ScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GK_Score> dictionary = gK_ScoreCollection.AllTimeScores;
		switch (s.TimeSpan)
		{
		case GK_TimeSpan.ALL_TIME:
			dictionary = gK_ScoreCollection.AllTimeScores;
			break;
		case GK_TimeSpan.TODAY:
			dictionary = gK_ScoreCollection.TodayScores;
			break;
		case GK_TimeSpan.WEEK:
			dictionary = gK_ScoreCollection.WeekScores;
			break;
		}
		if (dictionary.ContainsKey(s.Rank))
		{
			dictionary[s.Rank] = s;
		}
		else
		{
			dictionary.Add(s.Rank, s);
		}
	}

	public void UpdateMaxRange(int MR)
	{
		_info.MaxRange = MR;
	}
}
