using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardsUITab : FeatureTab
{
	private const string LEADERBOARD_ID = "CgkIipfs2qcGEAIQAA";

	public Image avatar;

	private Sprite defaulttexture;

	private Sprite logo;

	public Button connectButton;

	public Text connectButtonLabel;

	public Text playerLabel;

	public Toggle GlobalButton;

	public Toggle FriendsButton;

	public Toggle AllTimeButton;

	public Toggle WeekButton;

	public Toggle TodayButton;

	public Button SubmitScoreButton;

	public Text SubmitScoreLabel;

	public Selectable[] ConnectionDependedntButtons;

	public LeaderboardInfoPresenter[] lines;

	private GPLeaderBoard loadedLeaderBoard;

	private GPCollectionType displayCollection;

	private GPBoardTimeSpan displayTime = GPBoardTimeSpan.ALL_TIME;

	private int score = 100;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.sprite;
		SA_StatusBar.text = "Custom Leader-board example scene loaded";
		LeaderboardInfoPresenter[] array = lines;
		foreach (LeaderboardInfoPresenter leaderboardInfoPresenter in array)
		{
			leaderboardInfoPresenter.Disable();
		}
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlayManager.ActionScoreSubmited += OnScoreSbumitted;
		GooglePlayManager.ActionScoresListLoaded += ActionScoreRequestReceived;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
		GlobalButton.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				displayCollection = GPCollectionType.GLOBAL;
				UpdateScoresDisaplay();
			}
		});
		FriendsButton.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				displayCollection = GPCollectionType.FRIENDS;
				UpdateScoresDisaplay();
			}
		});
		AllTimeButton.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				displayTime = GPBoardTimeSpan.ALL_TIME;
				UpdateScoresDisaplay();
			}
		});
		WeekButton.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				displayTime = GPBoardTimeSpan.WEEK;
				UpdateScoresDisaplay();
			}
		});
		TodayButton.onValueChanged.AddListener((bool b) =>
		{
			if (b)
			{
				displayTime = GPBoardTimeSpan.TODAY;
				UpdateScoresDisaplay();
			}
		});
	}

	public void LoadScore()
	{
		Singleton<GooglePlayManager>.Instance.LoadPlayerCenteredScores("CgkIipfs2qcGEAIQAA", displayTime, displayCollection, 10);
	}

	public void OpenUI()
	{
		Singleton<GooglePlayManager>.Instance.ShowLeaderBoardById("CgkIipfs2qcGEAIQAA");
	}

	public void ShowGlobal()
	{
		displayCollection = GPCollectionType.GLOBAL;
		UpdateScoresDisaplay();
	}

	public void ShowLocal()
	{
		displayCollection = GPCollectionType.FRIENDS;
		UpdateScoresDisaplay();
	}

	public void ShowAllTime()
	{
		displayTime = GPBoardTimeSpan.ALL_TIME;
		UpdateScoresDisaplay();
	}

	public void ShowWeek()
	{
		displayTime = GPBoardTimeSpan.WEEK;
		UpdateScoresDisaplay();
	}

	public void ShowDay()
	{
		displayTime = GPBoardTimeSpan.TODAY;
		UpdateScoresDisaplay();
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

	private void UpdateScoresDisaplay()
	{
		if (loadedLeaderBoard != null)
		{
			GPScore currentPlayerScore = loadedLeaderBoard.GetCurrentPlayerScore(displayTime, displayCollection);
			int i;
			if (currentPlayerScore == null)
			{
				i = 1;
			}
			else
			{
				for (i = Mathf.Clamp(currentPlayerScore.Rank - 5, 1, currentPlayerScore.Rank); loadedLeaderBoard.GetScore(i, displayTime, displayCollection) == null; i++)
				{
				}
			}
			Debug.Log("Start Display at rank: " + i);
			int num = i;
			LeaderboardInfoPresenter[] array = lines;
			foreach (LeaderboardInfoPresenter leaderboardInfoPresenter in array)
			{
				GPScore gPScore = loadedLeaderBoard.GetScore(num, displayTime, displayCollection);
				if (gPScore != null)
				{
					GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(gPScore.PlayerId);
					leaderboardInfoPresenter.SetInfo(num.ToString(), gPScore.LongScore.ToString(), gPScore.PlayerId, (playerById == null) ? "[Empty]" : playerById.name, (playerById == null || !playerById.hasIconImage) ? defaulttexture.texture : playerById.icon);
				}
				else
				{
					leaderboardInfoPresenter.Disable();
				}
				num++;
			}
		}
		else
		{
			LeaderboardInfoPresenter[] array2 = lines;
			foreach (LeaderboardInfoPresenter leaderboardInfoPresenter2 in array2)
			{
				leaderboardInfoPresenter2.Disable();
			}
		}
	}

	private void FixedUpdate()
	{
		SubmitScoreLabel.text = "Submit Score: " + score;
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
			Selectable[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (Selectable selectable in connectionDependedntButtons)
			{
				selectable.interactable = true;
			}
			switch (displayTime)
			{
			case GPBoardTimeSpan.ALL_TIME:
				AllTimeButton.Select();
				break;
			case GPBoardTimeSpan.WEEK:
				WeekButton.Select();
				break;
			case GPBoardTimeSpan.TODAY:
				TodayButton.Select();
				break;
			}
			switch (displayCollection)
			{
			case GPCollectionType.GLOBAL:
				GlobalButton.Select();
				break;
			case GPCollectionType.FRIENDS:
				FriendsButton.Select();
				break;
			}
		}
		else
		{
			Selectable[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (Selectable selectable2 in connectionDependedntButtons2)
			{
				selectable2.interactable = false;
			}
			text = ((GooglePlayConnection.State != GPConnectionState.STATE_DISCONNECTED && GooglePlayConnection.State != GPConnectionState.STATE_UNCONFIGURED) ? "Connecting.." : "Connect");
		}
		connectButtonLabel.text = text;
	}

	public void SubmitScore()
	{
		Singleton<GooglePlayManager>.Instance.SubmitScoreById("CgkIipfs2qcGEAIQAA", score, string.Empty);
		SA_StatusBar.text = "Submitiong score: " + (score + 1);
		score++;
	}

	private void OnPlayerDisconnected()
	{
		SA_StatusBar.text = "Player Disconnected";
		playerLabel.text = "Player Disconnected";
	}

	private void OnPlayerConnected()
	{
		SA_StatusBar.text = "Player Connected";
		playerLabel.text = Singleton<GooglePlayManager>.Instance.player.name;
	}

	private void OnConnectionResult(GooglePlayConnectionResult result)
	{
		SA_StatusBar.text = "Connection Resul:  " + result.code;
		Debug.Log(result.code.ToString());
	}

	private void ActionScoreRequestReceived(GooglePlayResult obj)
	{
		SA_StatusBar.text = "Scores Load Finished";
		loadedLeaderBoard = Singleton<GooglePlayManager>.Instance.GetLeaderBoard("CgkIipfs2qcGEAIQAA");
		if (loadedLeaderBoard == null)
		{
			Debug.Log("No Leaderboard found");
			return;
		}
		List<GPScore> scoresList = loadedLeaderBoard.GetScoresList(GPBoardTimeSpan.ALL_TIME, GPCollectionType.GLOBAL);
		foreach (GPScore item in scoresList)
		{
			Debug.Log("OnScoreUpdated " + item.Rank + " " + item.PlayerId + " " + item.LongScore);
		}
		GPScore currentPlayerScore = loadedLeaderBoard.GetCurrentPlayerScore(displayTime, displayCollection);
		Debug.Log("currentPlayerScore: " + currentPlayerScore.LongScore + " rank:" + currentPlayerScore.Rank);
		UpdateScoresDisaplay();
	}

	private void OnScoreSbumitted(GP_LeaderboardResult result)
	{
		SA_StatusBar.text = "Score Submit Resul:  " + result.Message;
		LoadScore();
	}

	private void OnDestroy()
	{
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlayManager.ActionScoreSubmited -= OnScoreSbumitted;
		GooglePlayManager.ActionScoresListLoaded -= ActionScoreRequestReceived;
	}
}
