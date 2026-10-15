using System;
using System.Collections.Generic;
using System.Text;

public class GK_TBM_Match
{
	public string Id;

	public string Message;

	public GK_TBM_Participant CurrentParticipant;

	public DateTime CreationTimestamp;

	public byte[] Data;

	public GK_TurnBasedMatchStatus Status;

	public List<GK_TBM_Participant> Participants;

	public string UTF8StringData
	{
		get
		{
			if (Data != null)
			{
				return Encoding.UTF8.GetString(Data);
			}
			return string.Empty;
		}
	}

	public GK_TBM_Participant LocalParticipant
	{
		get
		{
			foreach (GK_TBM_Participant participant in Participants)
			{
				if (participant.Player != null && participant.PlayerId.Equals(GameCenterManager.Player.Id))
				{
					return participant;
				}
			}
			return null;
		}
	}

	public void SetData(string val)
	{
		byte[] data = Convert.FromBase64String(val);
		Data = data;
	}

	public GK_TBM_Participant GetParticipantByPlayerId(string playerId)
	{
		foreach (GK_TBM_Participant participant in Participants)
		{
			if (participant.Player == null)
			{
				if (playerId.Length == 0)
				{
					return participant;
				}
			}
			else if (playerId.Equals(participant.Player.Id))
			{
				return participant;
			}
		}
		return null;
	}
}
