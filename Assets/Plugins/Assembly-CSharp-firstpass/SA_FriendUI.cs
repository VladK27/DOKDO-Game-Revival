using SA.Common.Pattern;
using UnityEngine;

public class SA_FriendUI : MonoBehaviour
{
	private string _pId = string.Empty;

	public GameObject avatar;

	public SA_Label playerId;

	public SA_Label playerName;

	private Texture defaulttexture;

	private void Awake()
	{
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
	}

	public void SetFriendId(string pId)
	{
		_pId = pId;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
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
