using SA.Common.Models;
using UnityEngine;

public class GameCenterFriendLoadExample : MonoBehaviour
{
	private string ChallengeLeaderboard = "your.leaderboard2.id.here";

	private string ChallengeAchievement = "your.achievement.id.here ";

	public GUIStyle headerStyle;

	public GUIStyle boardStyle;

	private bool renderFriendsList;

	private void Awake()
	{
		GameCenterManager.OnAuthFinished += HandleOnAuthFinished;
		GameCenterManager.Init();
	}

	private void OnGUI()
	{
		GUI.Label(new Rect(10f, 20f, 400f, 40f), "Friend List Load Example", headerStyle);
		if (GUI.Button(new Rect(300f, 10f, 150f, 50f), "Load Friends"))
		{
			GameCenterManager.OnFriendsListLoaded += OnFriendsListLoaded;
			GameCenterManager.RetrieveFriends();
		}
		if (GUI.Button(new Rect(500f, 10f, 150f, 50f), "Invite Friends"))
		{
			GK_FriendRequest gK_FriendRequest = new GK_FriendRequest();
			gK_FriendRequest.Send();
		}
		if (GUI.Button(new Rect(700f, 10f, 150f, 50f), "Invite with Emails"))
		{
			GK_FriendRequest gK_FriendRequest2 = new GK_FriendRequest();
			gK_FriendRequest2.addRecipientsWithEmailAddresses("support@stansassets.com", "test@test.com");
			gK_FriendRequest2.Send();
		}
		if (!renderFriendsList)
		{
			return;
		}
		if (GUI.Button(new Rect(500f, 10f, 180f, 50f), "Leaberboard Challenge All"))
		{
			GameCenterManager.IssueLeaderboardChallenge(ChallengeLeaderboard, "Your message here", GameCenterManager.FriendsList.ToArray());
		}
		if (GUI.Button(new Rect(730f, 10f, 180f, 50f), "Achievement Challenge All"))
		{
			GameCenterManager.IssueAchievementChallenge(ChallengeAchievement, "Your message here", GameCenterManager.FriendsList.ToArray());
		}
		GUI.Label(new Rect(10f, 90f, 100f, 40f), "id", boardStyle);
		GUI.Label(new Rect(150f, 90f, 100f, 40f), "name", boardStyle);
		GUI.Label(new Rect(300f, 90f, 100f, 40f), "avatar ", boardStyle);
		int num = 1;
		foreach (string friends in GameCenterManager.FriendsList)
		{
			GK_Player playerById = GameCenterManager.GetPlayerById(friends);
			if (playerById != null)
			{
				GUI.Label(new Rect(10f, 90 + 70 * num, 100f, 40f), playerById.Id, boardStyle);
				GUI.Label(new Rect(150f, 90 + 70 * num, 100f, 40f), playerById.Alias, boardStyle);
				if (playerById.SmallPhoto != null)
				{
					GUI.DrawTexture(new Rect(300f, 75 + 70 * num, 50f, 50f), playerById.SmallPhoto);
				}
				else
				{
					GUI.Label(new Rect(300f, 90 + 70 * num, 100f, 40f), "no photo ", boardStyle);
				}
				if (GUI.Button(new Rect(450f, 90 + 70 * num, 150f, 30f), "Challenge Leaderboard"))
				{
					GameCenterManager.IssueLeaderboardChallenge(ChallengeLeaderboard, "Your message here", friends);
				}
				if (GUI.Button(new Rect(650f, 90 + 70 * num, 150f, 30f), "Challenge Achievement"))
				{
					GameCenterManager.IssueAchievementChallenge(ChallengeAchievement, "Your message here", friends);
				}
				num++;
			}
		}
	}

	private void HandleOnAuthFinished(Result result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Player Authed");
		}
		else
		{
			IOSNativePopUpManager.showMessage("Game Center ", "Player authentication failed");
		}
	}

	private void OnFriendsListLoaded(Result result)
	{
		GameCenterManager.OnFriendsListLoaded -= OnFriendsListLoaded;
		if (result.IsSucceeded)
		{
			renderFriendsList = true;
		}
	}
}
