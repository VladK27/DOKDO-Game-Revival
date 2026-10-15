using SA.Common.Models;

public class ISN_RemoteNotificationsRegistrationResult : Result
{
	private ISN_DeviceToken _Token;

	public ISN_DeviceToken Token
	{
		get
		{
			return _Token;
		}
	}

	public ISN_RemoteNotificationsRegistrationResult(ISN_DeviceToken token)
	{
		_Token = token;
	}

	public ISN_RemoteNotificationsRegistrationResult(Error error)
		: base(error)
	{
	}
}
