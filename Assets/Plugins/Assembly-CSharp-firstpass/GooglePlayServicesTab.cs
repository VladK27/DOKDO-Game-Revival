using System;
using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayServicesTab : FeatureTab
{
	private int score = 100;

	private const string LEADERBOARD_NAME = "leaderboard_best_scores";

	private const string PIE_GIFT_ID = "Pie";

	private const string LEADERBOARD_ID = "CgkIipfs2qcGEAIQAA";

	private const string INCREMENTAL_ACHIEVEMENT_ID = "CgkIipfs2qcGEAIQCg";

	public Image avatar;

	private Sprite defaulttexture;

	private Sprite logo;

	public Texture2D pieIcon;

	public Button connectButton;

	public Text connectButtonLabel;

	public Button scoreSubmit;

	public Text scoreSubmitLabel;

	public Text playerLabel;

	public Button[] ConnectionDependedntButtons;

	public Text a_id;

	public Text a_name;

	public Text a_descr;

	public Text a_type;

	public Text a_state;

	public Text a_steps;

	public Text a_total;

	public Text b_id;

	public Text b_name;

	public Text b_all_time;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.sprite;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += ActionConnectionResultReceived;
		GooglePlayManager.ActionAchievementUpdated += OnAchievementUpdated;
		GooglePlayManager.ActionScoreSubmited += OnScoreSubmited;
		GooglePlayManager.ActionScoresListLoaded += OnScoreUpdated;
		GooglePlayManager.ActionSendGiftResultReceived += OnGiftResult;
		GooglePlayManager.ActionPendingGameRequestsDetected += OnPendingGiftsDetected;
		GooglePlayManager.ActionGameRequestsAccepted += OnGameRequestAccepted;
		GooglePlayManager.ActionOAuthTokenLoaded += ActionOAuthTokenLoaded;
		GooglePlayManager.ActionAvailableDeviceAccountsLoaded += ActionAvailableDeviceAccountsLoaded;
		GooglePlayManager.ActionAchievementsLoaded += OnAchievmnetsLoadedInfoListner;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
	}

	private void OnDestroy()
	{
		if (!Singleton<GooglePlayConnection>.IsDestroyed)
		{
			GooglePlayConnection.ActionPlayerConnected -= OnPlayerConnected;
			GooglePlayConnection.ActionPlayerDisconnected -= OnPlayerDisconnected;
			GooglePlayConnection.ActionConnectionResultReceived -= ActionConnectionResultReceived;
		}
		if (!Singleton<GooglePlayManager>.IsDestroyed)
		{
			GooglePlayManager.ActionAchievementUpdated -= OnAchievementUpdated;
			GooglePlayManager.ActionScoreSubmited -= OnScoreSubmited;
			GooglePlayManager.ActionScoresListLoaded -= OnScoreUpdated;
			GooglePlayManager.ActionSendGiftResultReceived -= OnGiftResult;
			GooglePlayManager.ActionPendingGameRequestsDetected -= OnPendingGiftsDetected;
			GooglePlayManager.ActionGameRequestsAccepted -= OnGameRequestAccepted;
			GooglePlayManager.ActionAvailableDeviceAccountsLoaded -= ActionAvailableDeviceAccountsLoaded;
			GooglePlayManager.ActionOAuthTokenLoaded -= ActionOAuthTokenLoaded;
			GooglePlayManager.ActionAchievementsLoaded -= OnAchievmnetsLoadedInfoListner;
		}
	}

	public void ConncetButtonPress()
	{
		Debug.Log("GooglePlayManager State  -> " + GooglePlayConnection.State);
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			SA_StatusBar.text = "Disconnecting from Play Service...";
			Singleton<GooglePlayConnection>.Instance.Disconnect();
		}
		else
		{
			SA_StatusBar.text = "Connecting to Play Service...";
			Singleton<GooglePlayConnection>.Instance.Connect();
		}
	}

	public void GetAccs()
	{
		Singleton<GooglePlayManager>.Instance.RetrieveDeviceGoogleAccounts();
	}

	public void RetrieveToken()
	{
		Singleton<GooglePlayManager>.Instance.LoadToken();
	}

	public void showLeaderBoardsUI()
	{
		Singleton<GooglePlayManager>.Instance.ShowLeaderBoardsUI();
		SA_StatusBar.text = "Showing Leader Boards UI";
	}

	public void loadLeaderBoards()
	{
		if (Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA").GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS) == null)
		{
			GooglePlayManager.ActionLeaderboardsLoaded += OnLeaderBoardsLoaded;
			Singleton<GooglePlayManager>.Instance.LoadLeaderBoards();
			SA_StatusBar.text = "Loading Leader Boards Data...";
		}
		else
		{
			SA_StatusBar.text = "leaderboard_best_scores  score  " + Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA").GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS).LongScore;
			AN_PoupsProxy.showMessage("leaderboard_best_scores  score", Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA").GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS).LongScore.ToString());
			UpdateBoardInfo();
		}
	}

	public void showLeaderBoard()
	{
		Singleton<GooglePlayManager>.Instance.ShowLeaderBoardById("CgkIipfs2qcGEAIQAA");
		SA_StatusBar.text = "Shwoing Leader Board UI for CgkIipfs2qcGEAIQAA";
	}

	public void submitScore()
	{
		score++;
		Singleton<GooglePlayManager>.Instance.SubmitScore("leaderboard_best_scores", score, string.Empty);
		SA_StatusBar.text = "Score " + score + " Submited for leaderboard_best_scores";
	}

	public void ResetBoard()
	{
		Singleton<GooglePlayManager>.Instance.ResetLeaderBoard("CgkIipfs2qcGEAIQAA");
		UpdateBoardInfo();
	}

	public void showAchievementsUI()
	{
		Singleton<GooglePlayManager>.Instance.ShowAchievementsUI();
		SA_StatusBar.text = "Showing Achievements UI";
	}

	public void loadAchievements()
	{
		GooglePlayManager.ActionAchievementsLoaded += OnAchievementsLoaded;
		Singleton<GooglePlayManager>.Instance.LoadAchievements();
		SA_StatusBar.text = "Loading Achievements Data...";
	}

	public void reportAchievement()
	{
		Singleton<GooglePlayManager>.Instance.UnlockAchievement("achievement_simple_achievement_example");
		SA_StatusBar.text = "Reporting achievement_prime...";
	}

	public void incrementAchievement()
	{
		Singleton<GooglePlayManager>.Instance.IncrementAchievementById("CgkIipfs2qcGEAIQCg", 1);
		SA_StatusBar.text = "Incrementing achievement_bored...";
	}

	public void revealAchievement()
	{
		Singleton<GooglePlayManager>.Instance.RevealAchievement("achievement_hidden_achievement_example");
		SA_StatusBar.text = "Revealing achievement_humble...";
	}

	public void ResetAchievement()
	{
		Singleton<GooglePlayManager>.Instance.ResetAchievement("CgkIipfs2qcGEAIQCg");
		AN_PoupsProxy.showMessage("Reset Complete: ", "Reset Complete, but since this is feature for testing only, achievement data cache will be updated after next interaction with acheivment");
	}

	public void ResetAllAchievements()
	{
		Singleton<GooglePlayManager>.Instance.ResetAllAchievements();
		AN_PoupsProxy.showMessage("Reset Complete: ", "Reset Complete, but since this is feature for testing only, achievement data cache will be updated after next interaction with acheivment");
	}

	public void SendGiftRequest()
	{
		Singleton<GooglePlayManager>.Instance.SendGiftRequest(GPGameRequestType.TYPE_GIFT, 1, pieIcon, "Here is some pie", "Pie");
	}

	public void OpenInbox()
	{
		Singleton<GooglePlayManager>.Instance.ShowRequestsAccepDialog();
	}

	public void clearDefaultAccount()
	{
		Singleton<GooglePlusAPI>.Instance.ClearDefaultAccount();
	}

	private void FixedUpdate()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			if (Singleton<GooglePlayManager>.Instance.player.icon != null)
			{
				Texture2D icon = Singleton<GooglePlayManager>.Instance.player.icon;
				if (logo == null)
				{
					logo = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
				}
				avatar.sprite = logo;
			}
		}
		else
		{
			avatar.sprite = defaulttexture;
		}
		string text = "Connect";
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			text = "Disconnect";
			Button[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (Button button in connectionDependedntButtons)
			{
				button.interactable = true;
			}
		}
		else
		{
			Button[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (Button button2 in connectionDependedntButtons2)
			{
				button2.interactable = false;
			}
			text = ((GooglePlayConnection.State != GPConnectionState.STATE_DISCONNECTED && GooglePlayConnection.State != GPConnectionState.STATE_UNCONFIGURED) ? "Connecting.." : "Connect");
		}
		connectButtonLabel.text = text;
		scoreSubmitLabel.text = "Submit Score: " + score;
	}

	public void RequestAdvertisingId()
	{
		GooglePlayUtils.ActionAdvertisingIdLoaded = (Action<GP_AdvertisingIdLoadResult>)Delegate.Combine(GooglePlayUtils.ActionAdvertisingIdLoaded, new Action<GP_AdvertisingIdLoadResult>(ActionAdvertisingIdLoaded));
		Singleton<GooglePlayUtils>.Instance.GetAdvertisingId();
	}

	private void ActionAdvertisingIdLoaded(GP_AdvertisingIdLoadResult res)
	{
		GooglePlayUtils.ActionAdvertisingIdLoaded = (Action<GP_AdvertisingIdLoadResult>)Delegate.Remove(GooglePlayUtils.ActionAdvertisingIdLoaded, new Action<GP_AdvertisingIdLoadResult>(ActionAdvertisingIdLoaded));
		if (res.IsSucceeded)
		{
			AndroidMessage.Create("Succeeded", "Advertising Id: " + res.id);
		}
		else
		{
			AndroidMessage.Create("Failed", "Advertising Id failed to loaed");
		}
	}

	private void OnAchievmnetsLoadedInfoListner(GooglePlayResult res)
	{
		GPAchievement achievement = Singleton<GooglePlayManager>.Instance.GetAchievement("CgkIipfs2qcGEAIQCg");
		if (achievement != null)
		{
			a_id.text = "Id: " + achievement.Id;
			a_name.text = "Name: " + achievement.Name;
			a_descr.text = "Description: " + achievement.Description;
			a_type.text = "Type: " + achievement.Type;
			a_state.text = "State: " + achievement.State;
			a_steps.text = "CurrentSteps: " + achievement.CurrentSteps;
			a_total.text = "TotalSteps: " + achievement.TotalSteps;
		}
	}

	private void OnAchievementsLoaded(GooglePlayResult result)
	{
		GooglePlayManager.ActionAchievementsLoaded -= OnAchievementsLoaded;
		if (result.IsSucceeded)
		{
			foreach (GPAchievement achievement in Singleton<GooglePlayManager>.Instance.Achievements)
			{
				Debug.Log(achievement.Id);
				Debug.Log(achievement.Name);
				Debug.Log(achievement.Description);
				Debug.Log(achievement.Type);
				Debug.Log(achievement.State);
				Debug.Log(achievement.CurrentSteps);
				Debug.Log(achievement.TotalSteps);
			}
			SA_StatusBar.text = "Total Achievement: " + Singleton<GooglePlayManager>.Instance.Achievements.Count;
			AN_PoupsProxy.showMessage("Achievments Loaded", "Total Achievements: " + Singleton<GooglePlayManager>.Instance.Achievements.Count);
		}
		else
		{
			SA_StatusBar.text = result.Message;
			AN_PoupsProxy.showMessage("Achievments Loaded error: ", result.Message);
		}
	}

	private void OnAchievementUpdated(GP_AchievementResult result)
	{
		SA_StatusBar.text = "Achievment Updated: Id: " + result.achievementId + "\n status: " + result.Message;
		AN_PoupsProxy.showMessage("Achievment Updated ", "Id: " + result.achievementId + "\n status: " + result.Message);
	}

	private void OnLeaderBoardsLoaded(GooglePlayResult result)
	{
		GooglePlayManager.ActionLeaderboardsLoaded -= OnLeaderBoardsLoaded;
		if (result.IsSucceeded)
		{
			if (Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA") == null)
			{
				AN_PoupsProxy.showMessage("Leader boards loaded", "CgkIipfs2qcGEAIQAA not found in leader boards list");
				return;
			}
			SA_StatusBar.text = "leaderboard_best_scores  score  " + Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA").GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS).LongScore;
			AN_PoupsProxy.showMessage("leaderboard_best_scores  score", Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA").GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS).LongScore.ToString());
		}
		else
		{
			SA_StatusBar.text = result.Message;
			AN_PoupsProxy.showMessage("Leader-Boards Loaded error: ", result.Message);
		}
		UpdateBoardInfo();
	}

	private void UpdateBoardInfo()
	{
		GPLeaderBoard leaderBoard = Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA");
		if (leaderBoard != null)
		{
			b_id.text = "Id: " + leaderBoard.Id;
			b_name.text = "Name: " + leaderBoard.Name;
			GPScore currentPlayerScore = leaderBoard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS);
			if (currentPlayerScore != null)
			{
				b_all_time.text = "All Time Score: " + currentPlayerScore.LongScore;
			}
			else
			{
				b_all_time.text = "All Time Score: EMPTY";
			}
		}
		else
		{
			b_all_time.text = "All Time Score:  -1";
		}
	}

	private void OnScoreSubmited(GP_LeaderboardResult result)
	{
		if (result.IsSucceeded)
		{
			SA_StatusBar.text = "Score Submited:  " + result.Message + " LeaderboardId: " + result.Leaderboard.Id + " LongScore: " + result.Leaderboard.GetCurrentPlayerScore(GPBoardTimeSpan.ALL_TIME, GPCollectionType.FRIENDS).LongScore;
		}
		else
		{
			SA_StatusBar.text = "Score Submit Fail:  " + result.Message;
		}
	}

	private void OnScoreUpdated(GooglePlayResult res)
	{
		UpdateBoardInfo();
	}

	private void OnPlayerDisconnected()
	{
		SA_StatusBar.text = "Player Disconnected";
		playerLabel.text = "Player Disconnected";
	}

	private void OnPlayerConnected()
	{
		SA_StatusBar.text = "Player Connected";
		playerLabel.text = Singleton<GooglePlayManager>.Instance.player.name + "(" + Singleton<GooglePlayManager>.Instance.currentAccount + ")";
	}

	private void ActionConnectionResultReceived(GooglePlayConnectionResult result)
	{
		if (result.IsSuccess)
		{
			Debug.Log("Connected!");
		}
		else
		{
			Debug.Log("Cnnection failed with code: " + result.code);
		}
		SA_StatusBar.text = "ConnectionResul:  " + result.code;
	}

	private void OnGiftResult(GooglePlayGiftRequestResult result)
	{
		SA_StatusBar.text = "Gift Send Result:  " + result.code;
		AN_PoupsProxy.showMessage("Gfit Send Complete", "Gift Send Result: " + result.code);
	}

	private void OnPendingGiftsDetected(List<GPGameRequest> gifts)
	{
		AndroidDialog androidDialog = AndroidDialog.Create("Pending Gifts Detected", "You got few gifts from your friends, do you whant to take a look?");
		androidDialog.ActionComplete += OnPromtGiftDialogClose;
	}

	private void OnPromtGiftDialogClose(AndroidDialogResult res)
	{
		if (res == AndroidDialogResult.YES)
		{
			Singleton<GooglePlayManager>.Instance.ShowRequestsAccepDialog();
		}
	}

	private void OnGameRequestAccepted(List<GPGameRequest> gifts)
	{
		foreach (GPGameRequest gift in gifts)
		{
			AN_PoupsProxy.showMessage("Gfit Accepted", gift.playload + " is excepted");
		}
	}

	private void ActionAvailableDeviceAccountsLoaded(List<string> accounts)
	{
		string text = "Device contains following google accounts:\n";
		foreach (string deviceGoogleAccount in Singleton<GooglePlayManager>.Instance.deviceGoogleAccountList)
		{
			text = text + deviceGoogleAccount + "\n";
		}
		AndroidDialog androidDialog = AndroidDialog.Create("Accounts Loaded", text, "Sign With Fitst one", "Do Nothing");
		androidDialog.ActionComplete += SighDialogComplete;
	}

	private void SighDialogComplete(AndroidDialogResult res)
	{
		if (res == AndroidDialogResult.YES)
		{
			Singleton<GooglePlayConnection>.Instance.Connect(Singleton<GooglePlayManager>.Instance.deviceGoogleAccountList[0]);
		}
	}

	private void ActionOAuthTokenLoaded(string token)
	{
		AN_PoupsProxy.showMessage("Token Loaded", Singleton<GooglePlayManager>.Instance.loadedAuthToken);
	}
}
