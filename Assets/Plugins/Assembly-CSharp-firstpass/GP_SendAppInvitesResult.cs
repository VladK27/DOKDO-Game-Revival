public class GP_SendAppInvitesResult : GooglePlayResult
{
	private string[] _InvitationIds;

	public string[] InvitationIds
	{
		get
		{
			return _InvitationIds;
		}
	}

	public GP_SendAppInvitesResult(string code)
		: base(code)
	{
	}

	public GP_SendAppInvitesResult(string[] invites)
		: base(GP_GamesStatusCodes.STATUS_OK)
	{
		_InvitationIds = invites;
	}
}
