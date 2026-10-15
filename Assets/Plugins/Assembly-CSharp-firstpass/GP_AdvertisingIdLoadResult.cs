using SA.Common.Models;

public class GP_AdvertisingIdLoadResult : Result
{
	public string id = string.Empty;

	public bool isLimitAdTrackingEnabled;

	public GP_AdvertisingIdLoadResult()
	{
	}

	public GP_AdvertisingIdLoadResult(Error error)
		: base(error)
	{
	}
}
