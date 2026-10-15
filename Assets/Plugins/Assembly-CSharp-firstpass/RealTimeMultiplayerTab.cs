using System.Text;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class RealTimeMultiplayerTab : FeatureTab
{
	public Image avatar;

	private Sprite defaulttexture;

	private Sprite logo;

	public GameObject hi;

	public Text playerLabel;

	public Text gameState;

	public Text parisipants;

	public Button connectButton;

	public Text connectButtonLabel;

	public Button helloButton;

	public Button leaveRoomButton;

	public Button showRoomButton;

	public Button[] ConnectionDependedntButtons;

	public ParticipantPresenter[] patricipants;

	public FriendPresenter[] friends;

	private string inviteId;

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.sprite;
		GooglePlayInvitationManager.ActionInvitationReceived += OnInvite;
		GooglePlayInvitationManager.ActionInvitationAccepted += ActionInvitationAccepted;
		GooglePlayRTM.ActionRoomCreated += OnRoomCreated;
		GooglePlayRTM.ActionWatingRoomIntentClosed += WaitingUIClosed;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
		}
		GooglePlayRTM.ActionDataRecieved += OnGCDataReceived;
	}

	private void WaitingUIClosed(AndroidActivityResult result)
	{
		Debug.Log(string.Concat("[WaitingUIClosed] result ", result.code, " status ", result.IsSucceeded.ToString()));
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

	public void ShowWatingRoom()
	{
		Singleton<GooglePlayRTM>.Instance.ShowWaitingRoomIntent();
	}

	public void findMatch()
	{
		int minPlayers = 1;
		int maxPlayers = 2;
		Singleton<GooglePlayRTM>.Instance.FindMatch(minPlayers, maxPlayers);
	}

	public void InviteFriends()
	{
		int minPlayers = 1;
		int maxPlayers = 2;
		Singleton<GooglePlayRTM>.Instance.OpenInvitationBoxUI(minPlayers, maxPlayers);
	}

	public void SendHello()
	{
		string s = "hello world";
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		byte[] bytes = uTF8Encoding.GetBytes(s);
		Singleton<GooglePlayRTM>.Instance.SendDataToAll(bytes, GP_RTM_PackageType.RELIABLE);
	}

	public void LeaveRoomInstance()
	{
		Singleton<GooglePlayRTM>.Instance.LeaveRoom();
	}

	private void DrawParticipants()
	{
		UpdateGameState("Room State: " + Singleton<GooglePlayRTM>.Instance.currentRoom.status);
		parisipants.text = "Total Room Participants: " + Singleton<GooglePlayRTM>.Instance.currentRoom.participants.Count;
		ParticipantPresenter[] array = patricipants;
		foreach (ParticipantPresenter participantPresenter in array)
		{
			participantPresenter.gameObject.SetActive(false);
		}
		int num = 0;
		foreach (GP_Participant participant in Singleton<GooglePlayRTM>.Instance.currentRoom.participants)
		{
			patricipants[num].gameObject.SetActive(true);
			patricipants[num].SetParticipant(participant);
			num++;
		}
	}

	private void UpdateGameState(string msg)
	{
		gameState.text = msg;
	}

	private void FixedUpdate()
	{
		DrawParticipants();
		if (Singleton<GooglePlayRTM>.Instance.currentRoom.status != GP_RTM_RoomStatus.ROOM_VARIANT_DEFAULT && Singleton<GooglePlayRTM>.Instance.currentRoom.status != GP_RTM_RoomStatus.ROOM_STATUS_ACTIVE)
		{
			showRoomButton.interactable = true;
		}
		else
		{
			showRoomButton.interactable = false;
		}
		if (Singleton<GooglePlayRTM>.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_VARIANT_DEFAULT)
		{
			leaveRoomButton.interactable = false;
		}
		else
		{
			leaveRoomButton.interactable = true;
		}
		if (Singleton<GooglePlayRTM>.Instance.currentRoom.status == GP_RTM_RoomStatus.ROOM_STATUS_ACTIVE)
		{
			helloButton.interactable = true;
			hi.SetActive(true);
		}
		else
		{
			helloButton.interactable = false;
			hi.SetActive(false);
		}
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
		Singleton<GooglePlayInvitationManager>.Instance.RegisterInvitationListener();
		GooglePlayManager.ActionFriendsListLoaded += OnFriendListLoaded;
		Singleton<GooglePlayManager>.Instance.LoadFriends();
	}

	private void OnFriendListLoaded(GooglePlayResult result)
	{
		Debug.Log("OnFriendListLoaded: " + result.Message);
		GooglePlayManager.ActionFriendsListLoaded -= OnFriendListLoaded;
		if (!result.IsSucceeded)
		{
			return;
		}
		Debug.Log("Friends Load Success");
		int num = 0;
		foreach (string friends in Singleton<GooglePlayManager>.Instance.friendsList)
		{
			if (num < 3)
			{
				this.friends[num].SetFriendId(friends);
			}
			num++;
		}
	}

	private void OnConnectionResult(GooglePlayConnectionResult result)
	{
		SA_StatusBar.text = "ConnectionResul:  " + result.code;
		Debug.Log(result.code.ToString());
	}

	private void OnInvite(GP_Invite invitation)
	{
		if (invitation.InvitationType == GP_InvitationType.INVITATION_TYPE_REAL_TIME)
		{
			inviteId = invitation.Id;
			AndroidDialog androidDialog = AndroidDialog.Create("Invite", "You have new invite from: " + invitation.Participant.DisplayName, "Manage Manually", "Open Google Inbox");
			androidDialog.ActionComplete += OnInvDialogComplete;
		}
	}

	private void ActionInvitationAccepted(GP_Invite invitation)
	{
		Debug.Log("ActionInvitationAccepted called");
		if (invitation.InvitationType == GP_InvitationType.INVITATION_TYPE_REAL_TIME)
		{
			Debug.Log("Starting The Game");
			Singleton<GooglePlayRTM>.Instance.AcceptInvitation(invitation.Id);
		}
	}

	private void OnRoomCreated(GP_GamesStatusCodes code)
	{
		SA_StatusBar.text = "Room Create Result:  " + code;
	}

	private void OnInvDialogComplete(AndroidDialogResult result)
	{
		switch (result)
		{
		case AndroidDialogResult.YES:
		{
			AndroidDialog androidDialog = AndroidDialog.Create("Manage Invite", "Would you like to accept this invite?", "Accept", "Decline");
			androidDialog.ActionComplete += OnInvManageDialogComplete;
			break;
		}
		case AndroidDialogResult.NO:
			Singleton<GooglePlayRTM>.Instance.OpenInvitationInBoxUI();
			break;
		}
	}

	private void OnInvManageDialogComplete(AndroidDialogResult result)
	{
		switch (result)
		{
		case AndroidDialogResult.YES:
			Singleton<GooglePlayRTM>.Instance.AcceptInvitation(inviteId);
			break;
		case AndroidDialogResult.NO:
			Singleton<GooglePlayRTM>.Instance.DeclineInvitation(inviteId);
			break;
		}
	}

	private void OnGCDataReceived(GP_RTM_Network_Package package)
	{
		UTF8Encoding uTF8Encoding = new UTF8Encoding();
		string text = uTF8Encoding.GetString(package.buffer);
		string participantId = package.participantId;
		GP_Participant participantById = Singleton<GooglePlayRTM>.Instance.currentRoom.GetParticipantById(package.participantId);
		if (participantById != null)
		{
			GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(participantById.playerId);
			if (playerById != null)
			{
				participantId = playerById.name;
			}
		}
		AndroidMessage.Create("Data Eeceived", "player " + participantId + " \n data: " + text);
	}
}
