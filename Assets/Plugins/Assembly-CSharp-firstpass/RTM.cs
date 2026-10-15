using UnityEngine;

public static class RTM
{
	private static iRTM_Matchmaker _Matchmaker;

	public static iRTM_Matchmaker Matchmaker
	{
		get
		{
			if (_Matchmaker == null)
			{
				CreateMatchmaker();
			}
			return _Matchmaker;
		}
	}

	private static void CreateMatchmaker()
	{
		switch (Application.platform)
		{
		case RuntimePlatform.Android:
			_Matchmaker = new GP_RTM_Controller();
			break;
		case RuntimePlatform.IPhonePlayer:
			_Matchmaker = new GK_RTM_Controller();
			break;
		default:
			_Matchmaker = new GP_RTM_Controller();
			break;
		}
	}
}
