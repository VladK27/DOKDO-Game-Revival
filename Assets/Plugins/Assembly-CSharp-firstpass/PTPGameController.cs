using System.Collections.Generic;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class PTPGameController : MonoBehaviour
{
	public GameObject pref;

	private DisconnectButton d;

	private ConnectionButton b;

	private ClickManagerExample m;

	public static PTPGameController instance;

	private List<GameObject> spheres = new List<GameObject>();

	private void Awake()
	{
		instance = this;
		GameCenterManager.OnAuthFinished += OnAuthFinished;
		GameCenterManager.Init();
		b = base.gameObject.AddComponent<ConnectionButton>();
		b.enabled = false;
		d = base.gameObject.AddComponent<DisconnectButton>();
		d.enabled = false;
		m = base.gameObject.GetComponent<ClickManagerExample>();
		m.enabled = false;
		GameCenter_RTM.ActionPlayerStateChanged += HandleActionPlayerStateChanged;
		GameCenter_RTM.ActionMatchStarted += HandleActionMatchStarted;
	}

	public void createRedSphere(Vector3 pos)
	{
		GameObject gameObject = Object.Instantiate(pref);
		gameObject.transform.position = pos;
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<Renderer>().material = new Material(gameObject.GetComponent<Renderer>().material);
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		spheres.Add(gameObject);
	}

	public void createGreenSphere(Vector3 pos)
	{
		GameObject gameObject = Object.Instantiate(pref);
		gameObject.transform.position = pos;
		gameObject.GetComponent<Renderer>().enabled = true;
		gameObject.GetComponent<Renderer>().material = new Material(gameObject.GetComponent<Renderer>().material);
		gameObject.GetComponent<Renderer>().material.color = Color.green;
		spheres.Add(gameObject);
	}

	private void OnAuthFinished(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSNativePopUpManager.showMessage("Player Authed ", "ID: " + GameCenterManager.Player.Id + "\nName: " + GameCenterManager.Player.DisplayName);
			cleanUpScene();
		}
	}

	private void HandleActionPlayerStateChanged(GK_Player player, GK_PlayerConnectionState state, GK_RTM_Match macth)
	{
		if (state == GK_PlayerConnectionState.Disconnected)
		{
			IOSNativePopUpManager.showMessage("Disconnect", "Game finished");
			Singleton<GameCenter_RTM>.Instance.Disconnect();
			cleanUpScene();
		}
		else
		{
			CheckMatchState(macth);
		}
	}

	private void HandleActionMatchStarted(GK_RTM_MatchStartedResult result)
	{
		if (result.IsSucceeded)
		{
			CheckMatchState(result.Match);
		}
		else
		{
			IOSNativePopUpManager.showMessage("Match Start Error", result.Error.Message);
		}
	}

	private void CheckMatchState(GK_RTM_Match macth)
	{
		IOSNativePopUpManager.dismissCurrentAlert();
		if (macth != null)
		{
			if (macth.ExpectedPlayerCount == 0)
			{
				IOSNativePopUpManager.showMessage("Match Started", "let's play now\n   Macth.ExpectedPlayerCount): " + macth.ExpectedPlayerCount);
				m.enabled = true;
				b.enabled = false;
				d.enabled = true;
				ISN_Logger.Log("Sending HelloPackage ");
				HelloPackage helloPackage = new HelloPackage();
				helloPackage.send();
			}
			else
			{
				IOSNativePopUpManager.showMessage("Match Created", "Macth.ExpectedPlayerCount): " + macth.ExpectedPlayerCount);
			}
		}
	}

	private void cleanUpScene()
	{
		b.enabled = true;
		m.enabled = false;
		d.enabled = false;
		foreach (GameObject sphere in spheres)
		{
			Object.Destroy(sphere);
		}
		spheres.Clear();
	}
}
