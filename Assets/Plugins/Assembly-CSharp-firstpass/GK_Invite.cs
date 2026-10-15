using System;

public class GK_Invite
{
	private string _Id;

	private GK_Player _Sender;

	private int _PlayerGroup;

	private int _PlayerAttributes;

	public string Id
	{
		get
		{
			return _Id;
		}
	}

	public GK_Player Sender
	{
		get
		{
			return _Sender;
		}
	}

	public int PlayerGroup
	{
		get
		{
			return _PlayerGroup;
		}
	}

	public int PlayerAttributes
	{
		get
		{
			return _PlayerAttributes;
		}
	}

	public GK_Invite(string inviteData)
	{
		string[] array = inviteData.Split('|');
		_Id = array[0];
		_Sender = GameCenterManager.GetPlayerById(array[1]);
		_PlayerGroup = Convert.ToInt32(array[2]);
		_PlayerAttributes = Convert.ToInt32(array[3]);
	}
}
