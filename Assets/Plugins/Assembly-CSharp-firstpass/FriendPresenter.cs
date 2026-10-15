using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class FriendPresenter : MonoBehaviour
{
	private string _pId = string.Empty;

	public Image avatar;

	public Text playerId;

	public Text playerName;

	private Sprite defaulttexture;

	private void Awake()
	{
		defaulttexture = avatar.sprite;
	}

	public void SetFriendId(string pId)
	{
		_pId = pId;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		avatar.sprite = defaulttexture;
		GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(pId);
		if (playerById != null)
		{
			playerId.text = "Player Id: " + _pId;
			playerName.text = "Name: " + playerById.name;
			if (playerById.icon != null)
			{
				avatar.GetComponent<Renderer>().material.mainTexture = playerById.icon;
			}
		}
	}

	public void PlayWithFried()
	{
		Singleton<GooglePlayRTM>.Instance.FindMatch(1, 1, _pId);
	}

	private void FixedUpdate()
	{
		if (_pId != string.Empty)
		{
			SetFriendId(_pId);
		}
	}
}
