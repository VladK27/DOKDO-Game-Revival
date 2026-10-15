using UnityEngine;

public class IOSNativeFeaturesPreview : BaseIOSFeaturePreview
{
	public static IOSNativePreviewBackButton back;

	private void Awake()
	{
		if (back == null)
		{
			back = IOSNativePreviewBackButton.Create();
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Game Center Examples", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Basic Features"))
		{
			LoadLevel("GameCenterGeneral");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Friends Load Example"))
		{
			LoadLevel("FriendsLoadExample");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Custom Leaderboard GUI"))
		{
			LoadLevel("CustomLeaderboardGUIExample");
		}
		StartX = XStartPos;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Main Features", style);
		StartX = XStartPos;
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Billing"))
		{
			LoadLevel("BillingExample");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "iCloud"))
		{
			LoadLevel("iCloudExampleScene");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Social Posting"))
		{
			LoadLevel("SocialPostingExample");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Local And Push Notifications"))
		{
			LoadLevel("NotificationExample");
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Replay Kit"))
		{
			LoadLevel("ReplayKitExampleScene");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Cloud Kit"))
		{
			LoadLevel("CloudKitExampleScene");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Game Saves"))
		{
			LoadLevel("GameSavesExample");
		}
		StartX = XStartPos;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Networking", style);
		StartX = XStartPos;
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "TBM Multiplayer Example"))
		{
			LoadLevel("TMB_Multiplayer_Example");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "RTM Multiplayer Example"))
		{
			LoadLevel("RTM_Multiplayer_Example");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "P2P Game Example"))
		{
			LoadLevel("Peer-To-PeerGameExample");
		}
		StartX = XStartPos;
		StartY += YLableStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Additional Features Features", style);
		StartX = XStartPos;
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Native Popups and Events"))
		{
			LoadLevel("PopUpsAndAppEventsHandler");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Media Player API"))
		{
			LoadLevel("MediaExample");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "IOS Native Actions"))
		{
			LoadLevel("NativeIOSActionsExample");
		}
	}
}
