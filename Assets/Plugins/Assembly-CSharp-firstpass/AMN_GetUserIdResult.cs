public class AMN_GetUserIdResult : AMN_Result
{
	private string _user_id;

	public string UserId
	{
		get
		{
			return _user_id;
		}
	}

	public AMN_GetUserIdResult(string user_id)
		: base(true)
	{
		_user_id = user_id;
	}
}
