using System.Collections.Generic;
using System.Text;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class TBM_Multiplayer_Example : BaseIOSFeaturePreview
{
	private static bool IsInitialized;

	public GK_TBM_Match CurrentMatch
	{
		get
		{
			if (Singleton<GameCenter_TBM>.Instance.Matches.Count > 0)
			{
				return Singleton<GameCenter_TBM>.Instance.MatchesList[0];
			}
			return null;
		}
	}

	private void Awake()
	{
		if (!IsInitialized)
		{
			GameCenterManager.Init();
			GameCenterManager.OnAuthFinished += OnAuthFinished;
			IsInitialized = true;
		}
		int playerAttributes = 4;
		Singleton<GameCenter_RTM>.Instance.SetPlayerAttributes(playerAttributes);
		int minPlayers = 2;
		int maxPlayers = 2;
		string msg = "Come play with me, bro.";
		string[] playersToInvite = new string[1] { GameCenterManager.FriendsList[0] };
		Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(minPlayers, maxPlayers, msg, playersToInvite);
		GK_Player player = GameCenterManager.Player;
		player.OnPlayerPhotoLoaded += HandleOnPlayerPhotoLoaded;
		player.LoadPhoto(GK_PhotoSize.GKPhotoSizeNormal);
		GameCenter_RTM.ActionMatchStarted += HandleActionMatchStarted;
		GameCenterInvitations.ActionPlayerRequestedMatchWithRecipients += HandleActionPlayerRequestedMatchWithRecipients;
		GameCenterInvitations.ActionPlayerAcceptedInvitation += HandleActionPlayerAcceptedInvitation;
		GameCenter_RTM.ActionNearbyPlayerStateUpdated += HandleActionNearbyPlayerStateUpdated;
		Singleton<GameCenter_RTM>.Instance.StartBrowsingForNearbyPlayers();
	}

	private void HandleActionNearbyPlayerStateUpdated(GK_Player player, bool IsAvaliable)
	{
		ISN_Logger.Log("Player: " + player.DisplayName + "IsAvaliable: " + IsAvaliable);
		ISN_Logger.Log("Nearby Players Count: " + Singleton<GameCenter_RTM>.Instance.NearbyPlayers.Count);
	}

	private void HandleActionPlayerAcceptedInvitation(GK_MatchType matchType, GK_Invite invite)
	{
		if (matchType == GK_MatchType.RealTime)
		{
			bool useNativeUI = true;
			Singleton<GameCenter_RTM>.Instance.StartMatchWithInvite(invite, useNativeUI);
		}
	}

	private void HandleActionPlayerRequestedMatchWithRecipients(GK_MatchType matchType, string[] recepientIds, GK_Player[] recepients)
	{
		if (matchType == GK_MatchType.RealTime)
		{
			string msg = "Come play with me, bro.";
			Singleton<GameCenter_RTM>.Instance.FindMatchWithNativeUI(recepientIds.Length, recepientIds.Length, msg, recepientIds);
		}
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log("Match is successfully created");
			if (result.Match.ExpectedPlayerCount != 0)
			{
			}
		}
		else
		{
			ISN_Logger.Log("Match is creation failed with error: " + result.Error.Message);
		}
	}

	private void HandleOnPlayerPhotoLoaded(GK_UserPhotoLoadResult result)
	{
		if (result.IsSucceeded)
		{
			ISN_Logger.Log(result.Photo);
			ISN_Logger.Log(GameCenterManager.Player.BigPhoto);
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		if (!GameCenterManager.IsPlayerAuthenticated)
		{
			GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "TBM Multiplayer Example Scene, Authentication....", style);
			GUI.enabled = false;
		}
		else
		{
			GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "TBM Multiplayer Example Scene.", style);
			GUI.enabled = true;
		}
		GUI.enabled = true;
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Matches Info"))
		{
			Singleton<GameCenter_TBM>.Instance.LoadMatchesInfo();
			GameCenter_TBM.ActionMatchesInfoLoaded += ActionMatchesResultLoaded;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Create Match"))
		{
			Singleton<GameCenter_TBM>.Instance.FindMatch(2, 2, string.Empty);
			GameCenter_TBM.ActionMatchFound += ActionMatchFound;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Create Match With Native UI"))
		{
			Singleton<GameCenter_TBM>.Instance.FindMatchWithNativeUI(2, 2, string.Empty);
			GameCenter_TBM.ActionMatchFound += ActionMatchFound;
		}
		if (CurrentMatch == null)
		{
			GUI.enabled = false;
		}
		else
		{
			GUI.enabled = true;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Update Match Data"))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(CurrentMatch.UTF8StringData + "X");
			Singleton<GameCenter_TBM>.Instance.SaveCurrentTurn(CurrentMatch.Id, bytes);
			GameCenter_TBM.ActionMatchDataUpdated += ActionMatchDataUpdated;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Make A Trunn"))
		{
			byte[] bytes2 = Encoding.UTF8.GetBytes("Some trun data");
			CurrentMatch.CurrentParticipant.SetOutcome(GK_TurnBasedMatchOutcome.First);
			foreach (GK_TBM_Participant participant in CurrentMatch.Participants)
			{
				if (!participant.PlayerId.Equals(CurrentMatch.CurrentParticipant.PlayerId))
				{
					Singleton<GameCenter_TBM>.Instance.EndTurn(CurrentMatch.Id, bytes2, participant.PlayerId);
					GameCenter_TBM.ActionTrunEnded += ActionTrunEnded;
					return;
				}
			}
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "End Match"))
		{
			byte[] bytes3 = Encoding.UTF8.GetBytes("End match data");
			CurrentMatch.Participants[0].SetOutcome(GK_TurnBasedMatchOutcome.Won);
			CurrentMatch.Participants[1].SetOutcome(GK_TurnBasedMatchOutcome.Lost);
			Singleton<GameCenter_TBM>.Instance.EndMatch(CurrentMatch.Id, bytes3);
			GameCenter_TBM.ActionMacthEnded += ActionMacthEnded;
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Remove Match"))
		{
			Singleton<GameCenter_TBM>.Instance.RemoveMatch(CurrentMatch.Id);
			GameCenter_TBM.ActionMatchRemoved += ActionMacthRemoved;
		}
	}

	private void OnAuthFinished(Result res)
	{
		ISN_Logger.Log("Auth IsSucceeded: " + res.IsSucceeded);
	}

	public void ActionMatchesResultLoaded(GK_TBM_LoadMatchesResult res)
	{
		GameCenter_TBM.ActionMatchesInfoLoaded -= ActionMatchesResultLoaded;
		ISN_Logger.Log("ActionMatchesResultLoaded: " + res.IsSucceeded);
		if (res.IsFailed || res.LoadedMatches.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<string, GK_TBM_Match> loadedMatch in res.LoadedMatches)
		{
			GK_TBM_Match value = loadedMatch.Value;
			GameCenter_TBM.PrintMatchInfo(value);
		}
	}

	private void ActionMatchDataUpdated(GK_TBM_MatchDataUpdateResult res)
	{
		GameCenter_TBM.ActionMatchDataUpdated -= ActionMatchDataUpdated;
		ISN_Logger.Log("ActionMatchDataUpdated: " + res.IsSucceeded);
		if (res.IsFailed)
		{
			ISN_Logger.Log(res.Error.Message);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(res.Match);
		}
	}

	private void ActionTrunEnded(GK_TBM_EndTrunResult result)
	{
		GameCenter_TBM.ActionTrunEnded -= ActionTrunEnded;
		ISN_Logger.Log("ActionTrunEnded IsSucceeded: " + result.IsSucceeded);
		if (result.IsFailed)
		{
			IOSMessage.Create("ActionTrunEnded", result.Error.Message);
			ISN_Logger.Log(result.Error.Message);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}

	private void ActionMacthEnded(GK_TBM_MatchEndResult result)
	{
		GameCenter_TBM.ActionMacthEnded -= ActionMacthEnded;
		ISN_Logger.Log("ActionMacthEnded IsSucceeded: " + result.IsSucceeded);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}

	private void ActionMacthRemoved(GK_TBM_MatchRemovedResult result)
	{
		GameCenter_TBM.ActionMatchRemoved -= ActionMacthRemoved;
		ISN_Logger.Log("ActionMacthRemoved IsSucceeded: " + result.IsSucceeded);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message);
		}
		else
		{
			ISN_Logger.Log("Match Id: " + result.MatchId);
		}
	}

	private void ActionMatchFound(GK_TBM_MatchInitResult result)
	{
		GameCenter_TBM.ActionMatchFound -= ActionMatchFound;
		ISN_Logger.Log("ActionMatchFound IsSucceeded: " + result.IsSucceeded);
		if (result.IsFailed)
		{
			ISN_Logger.Log(result.Error.Message);
		}
		else
		{
			GameCenter_TBM.PrintMatchInfo(result.Match);
		}
	}
}
