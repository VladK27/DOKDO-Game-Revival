using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class ParticipantPresenter : MonoBehaviour
{
	public Image avatar;

	public Text id;

	public Text status;

	public Text playerId;

	public Text playerName;

	private string _id = string.Empty;

	private Sprite defaulttexture;

	private Sprite icon;

	private void Awake()
	{
		defaulttexture = avatar.sprite;
	}

	public void SetParticipant(GP_Participant p)
	{
		id.text = string.Empty;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		status.text = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE.ToString();
		GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(p.playerId);
		if (playerById != null)
		{
			playerId.text = "Player Id: " + p.playerId;
			playerName.text = "Name: " + playerById.name;
			if (playerById.icon != null && !_id.Equals(p.playerId))
			{
				icon = Sprite.Create(playerById.icon, new Rect(0f, 0f, playerById.icon.width, playerById.icon.height), new Vector2(0.5f, 0.5f));
				avatar.sprite = icon;
				_id = p.playerId;
			}
		}
		else
		{
			avatar.sprite = defaulttexture;
			icon = null;
			_id = string.Empty;
		}
		id.text = "ID: " + p.id;
		status.text = "Status: " + p.Status;
	}
}
