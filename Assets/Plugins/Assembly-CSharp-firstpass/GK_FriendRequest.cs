using System.Collections.Generic;
using SA.Common.Util;

public class GK_FriendRequest
{
	private int _Id;

	private List<string> _PlayersIds = new List<string>();

	private List<string> _Emails = new List<string>();

	public int Id
	{
		get
		{
			return _Id;
		}
	}

	public GK_FriendRequest()
	{
		_Id = IdFactory.NextId;
	}

	public void addRecipientsWithEmailAddresses(params string[] emailAddresses)
	{
		foreach (string item in emailAddresses)
		{
			if (!_Emails.Contains(item))
			{
				_Emails.Add(item);
			}
		}
	}

	public void addRecipientPlayers(params GK_Player[] players)
	{
		foreach (GK_Player gK_Player in players)
		{
			if (!_PlayersIds.Contains(gK_Player.Id))
			{
				_PlayersIds.Add(gK_Player.Id);
			}
		}
	}

	public void Send()
	{
		GameCenterManager.SendFriendRequest(this, _Emails, _PlayersIds);
	}
}
