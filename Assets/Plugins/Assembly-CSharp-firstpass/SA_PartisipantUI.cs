using SA.Common.Pattern;
using UnityEngine;

public class SA_PartisipantUI : MonoBehaviour
{
	public GameObject avatar;

	public SA_Label id;

	public SA_Label status;

	public SA_Label playerId;

	public SA_Label playerName;

	private Texture defaulttexture;

	private void Awake()
	{
		defaulttexture = avatar.GetComponent<Renderer>().material.mainTexture;
	}

	public void SetParticipant(GP_Participant p)
	{
		id.text = string.Empty;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		status.text = GP_RTM_ParticipantStatus.STATUS_UNRESPONSIVE.ToString();
		avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
		GooglePlayerTemplate playerById = Singleton<GooglePlayManager>.Instance.GetPlayerById(p.playerId);
		if (playerById != null)
		{
			playerId.text = "Player Id: " + p.playerId;
			playerName.text = "Name: " + playerById.name;
			if (playerById.icon != null)
			{
				avatar.GetComponent<Renderer>().material.mainTexture = playerById.icon;
			}
		}
		id.text = "ID: " + p.id;
		status.text = "Status: " + p.Status;
	}

	public void SetParticipant(UM_RTM_Participant participant)
	{
		id.text = "ID: " + participant.Id;
		status.text = participant.Status.ToString();
		playerId.text = "Player Id: " + participant.Id;
		playerName.text = "Name: " + participant.Name;
		if (participant.SmallIcon != null)
		{
			avatar.GetComponent<Renderer>().material.mainTexture = participant.SmallIcon;
		}
		else
		{
			avatar.GetComponent<Renderer>().material.mainTexture = defaulttexture;
		}
	}
}
