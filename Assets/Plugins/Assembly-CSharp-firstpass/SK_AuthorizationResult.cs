using SA.Common.Models;

public class SK_AuthorizationResult : Result
{
	private SK_CloudServiceAuthorizationStatus _AuthorizationStatus;

	public SK_CloudServiceAuthorizationStatus AuthorizationStatus
	{
		get
		{
			return _AuthorizationStatus;
		}
	}

	public SK_AuthorizationResult(SK_CloudServiceAuthorizationStatus status)
	{
		_AuthorizationStatus = status;
	}
}
