using System;
using UnityEngine;

[Serializable]
public class JSUseExample : MonoBehaviour
{
	public virtual void OnGUI()
	{
		if (GUI.Button(new Rect(10f, 70f, 200f, 70f), "Connect"))
		{
			gameObject.SendMessage("ConnectToPlaySertivce");
		}
	}

	public virtual void PlayerConnectd()
	{
		Debug.Log("Player Connected Event received");
	}

	public virtual void PlayerDisconected()
	{
		Debug.Log("Player Disconected Event received");
	}

	public virtual void Main()
	{
	}
}
