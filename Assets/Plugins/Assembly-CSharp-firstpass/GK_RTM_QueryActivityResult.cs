using SA.Common.Models;

public class GK_RTM_QueryActivityResult : Result
{
	private int _Activity;

	public int Activity
	{
		get
		{
			return _Activity;
		}
	}

	public GK_RTM_QueryActivityResult(int activity)
	{
		_Activity = activity;
	}

	public GK_RTM_QueryActivityResult(string errorData)
		: base(new Error(errorData))
	{
	}
}
