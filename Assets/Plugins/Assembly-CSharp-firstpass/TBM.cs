using UnityEngine;

public static class TBM
{
	private static iTBM_Matchmaker _Matchmaker;

	public static iTBM_Matchmaker Matchmaker
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
			_Matchmaker = new GP_TBM_Controller();
			break;
		case RuntimePlatform.IPhonePlayer:
			_Matchmaker = new GK_TBM_Controller();
			break;
		default:
			_Matchmaker = new GK_TBM_Controller();
			break;
		}
	}
}
