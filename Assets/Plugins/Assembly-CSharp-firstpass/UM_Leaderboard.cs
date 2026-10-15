using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UM_Leaderboard
{
	[SerializeField]
	public string id = "new leaderboard";

	public bool IsOpen = true;

	[SerializeField]
	public string IOSId = string.Empty;

	[SerializeField]
	public string AndroidId = string.Empty;

	[SerializeField]
	public string AmazonId = string.Empty;

	[SerializeField]
	private string _Description = string.Empty;

	[SerializeField]
	private Texture2D _Texture;

	private GK_Leaderboard gk_Leaderboard;

	private GPLeaderBoard gp_Leaderboard;

	private GC_Leaderboard gc_Leaderboard;

	public bool IsValid
	{
		get
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					return gp_Leaderboard != null;
				}
				return gc_Leaderboard != null;
			case RuntimePlatform.IPhonePlayer:
				return gk_Leaderboard != null;
			default:
				return true;
			}
		}
	}

	public string Id
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
					{
						return gp_Leaderboard.Id;
					}
					return gc_Leaderboard.Identifier;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.Id;
				}
			}
			return string.Empty;
		}
	}

	public string Name
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
					{
						return gp_Leaderboard.Name;
					}
					return gc_Leaderboard.Title;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.Info.Title;
				}
			}
			return string.Empty;
		}
	}

	public bool CurrentPlayerScoreLoaded
	{
		get
		{
			if (IsValid)
			{
				switch (Application.platform)
				{
				case RuntimePlatform.Android:
					if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
					{
						return gp_Leaderboard.CurrentPlayerScoreLoaded;
					}
					return gc_Leaderboard.CurrentPlayerScoreLoaded;
				case RuntimePlatform.IPhonePlayer:
					return gk_Leaderboard.CurrentPlayerScoreLoaded;
				}
			}
			return false;
		}
	}

	public GK_Leaderboard GameCenterLeaderboard
	{
		get
		{
			return gk_Leaderboard;
		}
	}

	public GPLeaderBoard GooglePlayLeaderboard
	{
		get
		{
			return gp_Leaderboard;
		}
	}

	public GC_Leaderboard AmazonLeaderboard
	{
		get
		{
			return gc_Leaderboard;
		}
	}

	public string Description
	{
		get
		{
			return _Description;
		}
		set
		{
			_Description = value;
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

	public void Setup(GPLeaderBoard gpLeaderboard)
	{
		gp_Leaderboard = gpLeaderboard;
	}

	public void Setup(GK_Leaderboard gkLeaderboard)
	{
		gk_Leaderboard = gkLeaderboard;
	}

	public void Setup(GC_Leaderboard gcLeaderboard)
	{
		gc_Leaderboard = gcLeaderboard;
	}

	public UM_Score GetScore(int rank, UM_TimeSpan scope, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					GPScore score2 = gp_Leaderboard.GetScore(rank, scope.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
					if (score2 != null)
					{
						result = new UM_Score(null, score2, null);
					}
				}
				else
				{
					GC_Score score3 = gc_Leaderboard.GetScore(rank, scope.Get_GC_TimeSpan());
					if (score3 != null)
					{
						result = new UM_Score(null, null, score3);
					}
				}
				break;
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score score = gk_Leaderboard.GetScore(rank, scope.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (score != null)
				{
					result = new UM_Score(score, null, null);
				}
				break;
			}
			}
		}
		return result;
	}

	public List<UM_Score> GetScoresList(UM_TimeSpan span, UM_CollectionType collection)
	{
		List<UM_Score> list = new List<UM_Score>();
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
			{
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					List<GPScore> scoresList2 = gp_Leaderboard.GetScoresList(span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
					{
						foreach (GPScore item in scoresList2)
						{
							list.Add(new UM_Score(null, item, null));
						}
						return list;
					}
				}
				List<GC_Score> scoresList3 = gc_Leaderboard.GetScoresList(span.Get_GC_TimeSpan());
				{
					foreach (GC_Score item2 in scoresList3)
					{
						list.Add(new UM_Score(null, null, item2));
					}
					return list;
				}
			}
			case RuntimePlatform.IPhonePlayer:
			{
				List<GK_Score> scoresList = gk_Leaderboard.GetScoresList(span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				{
					foreach (GK_Score item3 in scoresList)
					{
						list.Add(new UM_Score(item3, null, null));
					}
					return list;
				}
			}
			}
		}
		return list;
	}

	public UM_Score GetScoreByPlayerId(string playerId, UM_TimeSpan span, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					GPScore scoreByPlayerId2 = gp_Leaderboard.GetScoreByPlayerId(playerId, span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
					if (scoreByPlayerId2 != null)
					{
						result = new UM_Score(null, scoreByPlayerId2, null);
					}
				}
				else
				{
					GC_Score scoreByPlayerId3 = gc_Leaderboard.GetScoreByPlayerId(playerId, span.Get_GC_TimeSpan());
					if (scoreByPlayerId3 != null)
					{
						result = new UM_Score(null, null, scoreByPlayerId3);
					}
				}
				break;
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score scoreByPlayerId = gk_Leaderboard.GetScoreByPlayerId(playerId, span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (scoreByPlayerId != null)
				{
					result = new UM_Score(scoreByPlayerId, null, null);
				}
				break;
			}
			}
		}
		return result;
	}

	public UM_Score GetCurrentPlayerScore(UM_TimeSpan span, UM_CollectionType collection)
	{
		UM_Score result = null;
		if (IsValid)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine.Equals(UM_PlatformDependencies.Android))
				{
					GPScore currentPlayerScore2 = gp_Leaderboard.GetCurrentPlayerScore(span.Get_GP_TimeSpan(), collection.Get_GP_CollectionType());
					if (currentPlayerScore2 != null)
					{
						result = new UM_Score(null, currentPlayerScore2, null);
					}
				}
				else
				{
					GC_Score currentPlayerScore3 = gc_Leaderboard.GetCurrentPlayerScore(span.Get_GC_TimeSpan());
					if (currentPlayerScore3 != null)
					{
						result = new UM_Score(null, null, currentPlayerScore3);
					}
				}
				break;
			case RuntimePlatform.IPhonePlayer:
			{
				GK_Score currentPlayerScore = gk_Leaderboard.GetCurrentPlayerScore(span.Get_GK_TimeSpan(), collection.Get_GK_CollectionType());
				if (currentPlayerScore != null)
				{
					result = new UM_Score(currentPlayerScore, null, null);
				}
				break;
			}
			}
		}
		return result;
	}
}
