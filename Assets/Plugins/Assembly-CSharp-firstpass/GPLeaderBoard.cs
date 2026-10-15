using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GPLeaderBoard
{
	public bool IsOpen = true;

	[SerializeField]
	private string _id = string.Empty;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _description = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	private bool _CurrentPlayerScoreLoaded;

	public GPScoreCollection SocsialCollection = new GPScoreCollection();

	public GPScoreCollection GlobalCollection = new GPScoreCollection();

	public List<GPScore> CurrentPlayerScore = new List<GPScore>();

	private Dictionary<int, GP_LocalPlayerScoreUpdateListener> _ScoreUpdateListners;

	public string Id
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	public Texture2D Texture
	{
		get
		{
			return _Texture;
		}
		set
		{
			_Texture = value;
		}
	}

	public bool CurrentPlayerScoreLoaded
	{
		get
		{
			return _CurrentPlayerScoreLoaded;
		}
	}

	private Dictionary<int, GP_LocalPlayerScoreUpdateListener> ScoreUpdateListners
	{
		get
		{
			if (_ScoreUpdateListners == null)
			{
				_ScoreUpdateListners = new Dictionary<int, GP_LocalPlayerScoreUpdateListener>();
			}
			return _ScoreUpdateListners;
		}
	}

	public GPLeaderBoard(string lId, string lName)
	{
		_id = lId;
		_name = lName;
	}

	public void UpdateName(string lName)
	{
		_name = lName;
	}

	public List<GPScore> GetScoresList(GPBoardTimeSpan timeSpan, GPCollectionType collection)
	{
		GPScoreCollection gPScoreCollection = GlobalCollection;
		switch (collection)
		{
		case GPCollectionType.GLOBAL:
			gPScoreCollection = GlobalCollection;
			break;
		case GPCollectionType.FRIENDS:
			gPScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GPScore> dictionary = gPScoreCollection.AllTimeScores;
		switch (timeSpan)
		{
		case GPBoardTimeSpan.ALL_TIME:
			dictionary = gPScoreCollection.AllTimeScores;
			break;
		case GPBoardTimeSpan.TODAY:
			dictionary = gPScoreCollection.TodayScores;
			break;
		case GPBoardTimeSpan.WEEK:
			dictionary = gPScoreCollection.WeekScores;
			break;
		}
		List<GPScore> list = new List<GPScore>();
		list.AddRange(dictionary.Values);
		return list;
	}

	public GPScore GetScoreByPlayerId(string playerId, GPBoardTimeSpan timeSpan, GPCollectionType collection)
	{
		List<GPScore> scoresList = GetScoresList(timeSpan, collection);
		foreach (GPScore item in scoresList)
		{
			if (item.PlayerId.Equals(playerId))
			{
				return item;
			}
		}
		return null;
	}

	public GPScore GetScore(int rank, GPBoardTimeSpan timeSpan, GPCollectionType collection)
	{
		GPScoreCollection gPScoreCollection = GlobalCollection;
		switch (collection)
		{
		case GPCollectionType.GLOBAL:
			gPScoreCollection = GlobalCollection;
			break;
		case GPCollectionType.FRIENDS:
			gPScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GPScore> dictionary = gPScoreCollection.AllTimeScores;
		switch (timeSpan)
		{
		case GPBoardTimeSpan.ALL_TIME:
			dictionary = gPScoreCollection.AllTimeScores;
			break;
		case GPBoardTimeSpan.TODAY:
			dictionary = gPScoreCollection.TodayScores;
			break;
		case GPBoardTimeSpan.WEEK:
			dictionary = gPScoreCollection.WeekScores;
			break;
		}
		if (dictionary.ContainsKey(rank))
		{
			return dictionary[rank];
		}
		return null;
	}

	public GPScore GetCurrentPlayerScore(GPBoardTimeSpan timeSpan, GPCollectionType collection)
	{
		foreach (GPScore item in CurrentPlayerScore)
		{
			if (item.TimeSpan == timeSpan && item.Collection == collection)
			{
				return item;
			}
		}
		return null;
	}

	public void CreateScoreListener(int requestId)
	{
		GP_LocalPlayerScoreUpdateListener gP_LocalPlayerScoreUpdateListener = new GP_LocalPlayerScoreUpdateListener(requestId, Id);
		ScoreUpdateListners.Add(gP_LocalPlayerScoreUpdateListener.RequestId, gP_LocalPlayerScoreUpdateListener);
	}

	public void ReportLocalPlayerScoreUpdate(GPScore score, int requestId)
	{
		GP_LocalPlayerScoreUpdateListener gP_LocalPlayerScoreUpdateListener = _ScoreUpdateListners[requestId];
		gP_LocalPlayerScoreUpdateListener.ReportScoreUpdate(score);
	}

	public void ReportLocalPlayerScoreUpdateFail(string errorData, int requestId)
	{
		GP_LocalPlayerScoreUpdateListener gP_LocalPlayerScoreUpdateListener = _ScoreUpdateListners[requestId];
		gP_LocalPlayerScoreUpdateListener.ReportScoreUpdateFail(errorData);
	}

	public void UpdateCurrentPlayerScore(List<GPScore> newScores)
	{
		CurrentPlayerScore.Clear();
		foreach (GPScore newScore in newScores)
		{
			CurrentPlayerScore.Add(newScore);
		}
		_CurrentPlayerScoreLoaded = true;
	}

	public void UpdateCurrentPlayerScore(GPScore score)
	{
		GPScore currentPlayerScore = GetCurrentPlayerScore(score.TimeSpan, score.Collection);
		if (currentPlayerScore != null)
		{
			CurrentPlayerScore.Remove(currentPlayerScore);
		}
		CurrentPlayerScore.Add(score);
		_CurrentPlayerScoreLoaded = true;
	}

	public void UpdateScore(GPScore score)
	{
		GPScoreCollection gPScoreCollection = GlobalCollection;
		switch (score.Collection)
		{
		case GPCollectionType.GLOBAL:
			gPScoreCollection = GlobalCollection;
			break;
		case GPCollectionType.FRIENDS:
			gPScoreCollection = SocsialCollection;
			break;
		}
		Dictionary<int, GPScore> dictionary = gPScoreCollection.AllTimeScores;
		switch (score.TimeSpan)
		{
		case GPBoardTimeSpan.ALL_TIME:
			dictionary = gPScoreCollection.AllTimeScores;
			break;
		case GPBoardTimeSpan.TODAY:
			dictionary = gPScoreCollection.TodayScores;
			break;
		case GPBoardTimeSpan.WEEK:
			dictionary = gPScoreCollection.WeekScores;
			break;
		}
		if (dictionary.ContainsKey(score.Rank))
		{
			dictionary[score.Rank] = score;
		}
		else
		{
			dictionary.Add(score.Rank, score);
		}
	}
}
