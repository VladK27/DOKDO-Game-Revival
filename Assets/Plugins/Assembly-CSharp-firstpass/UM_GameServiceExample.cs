using SA.Common.Pattern;
using UnityEngine;

public class UM_GameServiceExample : BaseIOSFeaturePreview
{
	public int hiScore = 100;

	private string leaderBoardId = "LeaderBoardSample_1";

	private string leaderBoardId2 = "LeaderBoardSample_2";

	private string TEST_ACHIEVEMENT_1_ID = "AchievementSample_1";

	private string TEST_ACHIEVEMENT_2_ID = "AchievementSample_2";

	private bool _startedToLoadAvatar;

	private void Awake()
	{
		UM_GameServiceManager.OnPlayerConnected += OnPlayerConnected;
		UM_GameServiceManager.OnPlayerDisconnected += OnPlayerDisconnected;
		if (Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			OnPlayerConnected();
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		if (Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.CONNECTED && GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Disconnect"))
		{
			Singleton<UM_GameServiceManager>.Instance.Disconnect();
		}
		if ((Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.DISCONNECTED || Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.UNDEFINED) && GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Connect"))
		{
			Singleton<UM_GameServiceManager>.Instance.Connect();
		}
		if (Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			GUI.enabled = true;
		}
		else
		{
			GUI.enabled = false;
		}
		if (Singleton<UM_GameServiceManager>.Instance.Player != null)
		{
			GUI.Label(new Rect(320f, 10f, Screen.width, 40f), "ID: " + Singleton<UM_GameServiceManager>.Instance.Player.PlayerId);
			GUI.Label(new Rect(320f, 25f, Screen.width, 40f), "Name: " + Singleton<UM_GameServiceManager>.Instance.Player.Name);
			if (Singleton<UM_GameServiceManager>.Instance.Player.SmallPhoto != null)
			{
				GUI.DrawTexture(new Rect(225f, 10f, 75f, 75f), Singleton<UM_GameServiceManager>.Instance.Player.SmallPhoto);
			}
			else if (!_startedToLoadAvatar)
			{
				_startedToLoadAvatar = true;
				Singleton<UM_GameServiceManager>.Instance.Player.LoadSmallPhoto();
			}
		}
		StartY += YLableStep;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "GameCneter Leaderboards", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leaderboards"))
		{
			Singleton<UM_GameServiceManager>.Instance.ShowLeaderBoardsUI();
		}
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board1"))
		{
			Singleton<UM_GameServiceManager>.Instance.ShowLeaderBoardUI(leaderBoardId);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB 1"))
		{
			hiScore++;
			UM_GameServiceManager.ActionScoreSubmitted += HandleActionScoreSubmitted;
			Singleton<UM_GameServiceManager>.Instance.SubmitScore(leaderBoardId, hiScore, 0L);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 1"))
		{
			long longScore = Singleton<UM_GameServiceManager>.Instance.GetCurrentPlayerScore(leaderBoardId).LongScore;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Leader Board2"))
		{
			Singleton<UM_GameServiceManager>.Instance.ShowLeaderBoardUI(leaderBoardId2);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Score LB2"))
		{
			hiScore++;
			Singleton<UM_GameServiceManager>.Instance.SubmitScore(leaderBoardId2, hiScore, 0L);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Get Score LB 2"))
		{
			long longScore2 = Singleton<UM_GameServiceManager>.Instance.GetCurrentPlayerScore(leaderBoardId2).LongScore;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Achievements", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Achievements"))
		{
			Singleton<UM_GameServiceManager>.Instance.ShowAchievementsUI();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Reset Achievements"))
		{
			Singleton<UM_GameServiceManager>.Instance.ResetAchievements();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Report Achievements1"))
		{
			Singleton<UM_GameServiceManager>.Instance.UnlockAchievement(TEST_ACHIEVEMENT_1_ID);
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Increment Achievements2"))
		{
			Singleton<UM_GameServiceManager>.Instance.IncrementAchievement(TEST_ACHIEVEMENT_2_ID, 20f);
		}
	}

	private void HandleActionScoreSubmitted(UM_LeaderboardResult res)
	{
		if (res.IsSucceeded)
		{
			UM_Score currentPlayerScore = res.Leaderboard.GetCurrentPlayerScore(UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL);
			if (currentPlayerScore != null)
			{
				Debug.Log("Score submitted, new player high score: " + currentPlayerScore.LongScore);
			}
		}
		else
		{
			Debug.Log("Score submission failed: " + res.Error.Code + " / " + res.Error.Description);
		}
	}

	private void OnPlayerConnected()
	{
	}

	private void OnPlayerDisconnected()
	{
	}
}
