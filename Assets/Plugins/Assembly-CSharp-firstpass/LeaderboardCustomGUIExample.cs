using SA.Common.Models;
using UnityEngine;

public class LeaderboardCustomGUIExample : MonoBehaviour
{
	private string leaderboardId_1 = "your.ios.leaderbord1.id";

	public int hiScore = 100;

	public GUIStyle headerStyle;

	public GUIStyle boardStyle;

	private GK_Leaderboard loadedLeaderboard;

	private GK_CollectionType displayCollection;

	private void Awake()
	{
		GameCenterManager.OnAuthFinished += OnAuthFinished;
		GameCenterManager.OnScoresListLoaded += OnScoresListLoaded;
		GameCenterManager.Init();
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 20f, 400f, 40f), "Custom Leaderboard GUI Example", headerStyle);
		if (GUI.Button(new Rect(400f, 10f, 150f, 50f), "Load Friends Scores"))
		{
			GameCenterManager.LoadScore(leaderboardId_1, 1, 10, GK_TimeSpan.ALL_TIME, GK_CollectionType.FRIENDS);
		}
		if (GUI.Button(new Rect(600f, 10f, 150f, 50f), "Load Global Scores"))
		{
			GameCenterManager.LoadScore(leaderboardId_1, 1, 20);
		}
		Color color = GUI.color;
		if (displayCollection == GK_CollectionType.GLOBAL)
		{
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(800f, 10f, 170f, 50f), "Displaying Global Scores"))
		{
			displayCollection = GK_CollectionType.GLOBAL;
		}
		GUI.color = color;
		if (displayCollection == GK_CollectionType.FRIENDS)
		{
			GUI.color = Color.green;
		}
		if (GUI.Button(new Rect(800f, 70f, 170f, 50f), "Displaying Friends Scores"))
		{
			displayCollection = GK_CollectionType.FRIENDS;
		}
		GUI.color = color;
		GUI.Label(new Rect(10f, 90f, 100f, 40f), "rank", boardStyle);
		GUI.Label(new Rect(100f, 90f, 100f, 40f), "score", boardStyle);
		GUI.Label(new Rect(200f, 90f, 100f, 40f), "playerId", boardStyle);
		GUI.Label(new Rect(400f, 90f, 100f, 40f), "name ", boardStyle);
		GUI.Label(new Rect(550f, 90f, 100f, 40f), "avatar ", boardStyle);
		if (loadedLeaderboard == null)
		{
			return;
		}
		for (int i = 1; i < 10; i++)
		{
			GK_Score score = loadedLeaderboard.GetScore(i, GK_TimeSpan.ALL_TIME, displayCollection);
			if (score == null)
			{
				continue;
			}
			GUI.Label(new Rect(10f, 90 + 70 * i, 100f, 40f), i.ToString(), boardStyle);
			GUI.Label(new Rect(100f, 90 + 70 * i, 100f, 40f), score.LongScore.ToString(), boardStyle);
			GUI.Label(new Rect(200f, 90 + 70 * i, 100f, 40f), score.PlayerId, boardStyle);
			GK_Player playerById = GameCenterManager.GetPlayerById(score.PlayerId);
			if (playerById != null)
			{
				GUI.Label(new Rect(400f, 90 + 70 * i, 100f, 40f), playerById.Alias, boardStyle);
				if (playerById.SmallPhoto != null)
				{
					GUI.DrawTexture(new Rect(550f, 75 + 70 * i, 50f, 50f), playerById.SmallPhoto);
				}
				else
				{
					GUI.Label(new Rect(550f, 90 + 70 * i, 100f, 40f), "no photo ", boardStyle);
				}
			}
			if (GUI.Button(new Rect(650f, 90 + 70 * i, 100f, 30f), "Challenge"))
			{
				GameCenterManager.IssueLeaderboardChallenge(leaderboardId_1, "Your message here", score.PlayerId);
			}
		}
	}

	private void OnScoresListLoaded(Result res)
	{
		if (res.IsSucceeded)
		{
			loadedLeaderboard = GameCenterManager.GetLeaderboard(leaderboardId_1);
			IOSMessage.Create("Success", "Scores loaded");
		}
		else
		{
			IOSMessage.Create("Fail", "Failed to load scores");
		}
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}
}
