public class GooglePlayConnectionResult
{
	public GP_ConnectionResultCode code;

	public bool HasResolution;

	public bool IsSuccess
	{
		get
		{
			if (code == GP_ConnectionResultCode.SUCCESS)
			{
				return true;
			}
			return false;
		}
	}
}
