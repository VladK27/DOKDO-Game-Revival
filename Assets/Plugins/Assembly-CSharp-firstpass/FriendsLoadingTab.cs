using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class FriendsLoadingTab : FeatureTab
{
	public Image avatar;

	private Sprite logo;

	public Text playerLabel;

	public Button connectButton;

	public Text connectButtonLabel;

	private Sprite defaulttexture;

	public Button[] ConnectionDependedntButtons;

	public FriendInfoPresenter[] rows;

	private void Awake()
	{
		playerLabel.text = "Player Disconnected";
		defaulttexture = avatar.sprite;
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		GooglePlayConnection.ActionConnectionResultReceived += OnConnectionResult;
		GooglePlayManager.ActionFriendsListLoaded += OnFriendListLoaded;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
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

	private void Update()
	{
		if (GooglePlayConnection.State != GPConnectionState.STATE_CONNECTED)
		{
			return;
		}
		int num = 0;
		foreach (string friends in Singleton<GooglePlayManager>.Instance.friendsList)
		{
			GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(friends);
			if (playerById != null)
			{
				rows[num].SetInfo(playerById.playerId, playerById.name, playerById.hasIconImage && playerById.icon != null, playerById.hasHiResImage && playerById.image != null, playerById.icon);
			}
			num++;
			if (num > 7)
			{
				break;
			}
		}
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
	}

	public void LoadFriendsList()
	{
		Singleton<GooglePlayManager>.Instance.LoadFriends();
	}

	private void OnFriendListLoaded(GooglePlayResult result)
	{
		GooglePlayManager.ActionFriendsListLoaded -= OnFriendListLoaded;
		SA_StatusBar.text = "Load Friends Result:  " + result.Response;
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
		SA_StatusBar.text = "ConnectionResul:  " + result.code;
		Debug.Log(result.code.ToString());
	}
}
