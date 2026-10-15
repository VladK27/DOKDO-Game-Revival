using System.Collections.Generic;
using UnityEngine;

public class GC_Leaderboard
{
	public bool IsOpen = true;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _id = string.Empty;

	[SerializeField]
	private string _displayText = string.Empty;

	[SerializeField]
	private string _scoreFormat = string.Empty;

	[SerializeField]
	private string _imageUrl = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	private bool _CurrentPlayerScoreLoaded;

	private List<GC_Score> _CurrentPlayerScores = new List<GC_Score>();

	private Dictionary<int, GC_Score> _AllTimeCollection = new Dictionary<int, GC_Score>();

	private Dictionary<int, GC_Score> _WeekCollection = new Dictionary<int, GC_Score>();

	private Dictionary<int, GC_Score> _TodayCollection = new Dictionary<int, GC_Score>();

	public string Identifier
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

	public string Title
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
			return _displayText;
		}
		set
		{
			_displayText = value;
		}
	}

	public string ScoreFormat
	{
		get
		{
			return _scoreFormat;
		}
	}

	public string ImageUrl
	{
		get
		{
			return _imageUrl;
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

	public GC_Leaderboard()
	{
		_name = "New Leaderboard";
	}

	public List<GC_Score> GetScoresList(GC_ScoreTimeSpan timeSpan)
	{
		List<GC_Score> list = new List<GC_Score>();
		switch (timeSpan)
		{
		case GC_ScoreTimeSpan.ALL_TIME:
			list.AddRange(_AllTimeCollection.Values);
			break;
		case GC_ScoreTimeSpan.WEEK:
			list.AddRange(_WeekCollection.Values);
			break;
		case GC_ScoreTimeSpan.TODAY:
			list.AddRange(_TodayCollection.Values);
			break;
		}
		return list;
	}

	public GC_Score GetScoreByPlayerId(string id, GC_ScoreTimeSpan timeSpan)
	{
		foreach (GC_Score scores in GetScoresList(timeSpan))
		{
			if (scores.PlayerId.Equals(id))
			{
				return scores;
			}
		}
		return null;
	}

	public GC_Score GetScore(int rank, GC_ScoreTimeSpan timeSpan)
	{
		switch (timeSpan)
		{
		case GC_ScoreTimeSpan.ALL_TIME:
			if (_AllTimeCollection.ContainsKey(rank))
			{
				return _AllTimeCollection[rank];
			}
			break;
		case GC_ScoreTimeSpan.WEEK:
			if (_WeekCollection.ContainsKey(rank))
			{
				return _WeekCollection[rank];
			}
			break;
		case GC_ScoreTimeSpan.TODAY:
			if (_TodayCollection.ContainsKey(rank))
			{
				return _TodayCollection[rank];
			}
			break;
		}
		return null;
	}

	public GC_Score GetCurrentPlayerScore(GC_ScoreTimeSpan timeSpan)
	{
		foreach (GC_Score currentPlayerScore in _CurrentPlayerScores)
		{
			if (currentPlayerScore.TimeSpan == timeSpan)
			{
				return currentPlayerScore;
			}
		}
		return null;
	}

	public void UpdateCurrentPlayerScore(GC_Score newScore)
	{
		GC_Score currentPlayerScore = GetCurrentPlayerScore(newScore.TimeSpan);
		if (currentPlayerScore != null)
		{
			_CurrentPlayerScores.Remove(currentPlayerScore);
		}
		_CurrentPlayerScores.Add(newScore);
		_CurrentPlayerScoreLoaded = true;
		Debug.Log(string.Format("[Current Player Score Updated] {0}|{1}|{2}", newScore.PlayerId, newScore.Rank, newScore.Score));
	}

	public void UpdateScore(GC_Score newScore)
	{
		Dictionary<int, GC_Score> dictionary;
		switch (newScore.TimeSpan)
		{
		case GC_ScoreTimeSpan.ALL_TIME:
			dictionary = _AllTimeCollection;
			break;
		case GC_ScoreTimeSpan.WEEK:
			dictionary = _WeekCollection;
			break;
		case GC_ScoreTimeSpan.TODAY:
			dictionary = _TodayCollection;
			break;
		default:
			dictionary = _AllTimeCollection;
			break;
		}
		if (dictionary.ContainsKey(newScore.Rank))
		{
			dictionary[newScore.Rank] = newScore;
		}
		else
		{
			dictionary.Add(newScore.Rank, newScore);
		}
	}
}
