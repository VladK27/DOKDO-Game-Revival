using SA.Common.Pattern;
using UnityEngine;

public class CSharpAPIHelper : MonoBehaviour
{
	public void ConnectToPlaySertivce()
	{
		GooglePlayConnection.ActionPlayerConnected += OnPlayerConnected;
		GooglePlayConnection.ActionPlayerDisconnected += OnPlayerDisconnected;
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			OnPlayerConnected();
			return;
		}
		Debug.Log("Connecting....");
		Singleton<GooglePlayConnection>.Instance.Connect();
	}

	private void OnPlayerConnected()
	{
		base.gameObject.SendMessage("PlayerConnectd");
	}

	private void OnPlayerDisconnected()
	{
		base.gameObject.SendMessage("PlayerDisconected");
	}
}
