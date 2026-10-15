namespace Percent
{
	public class Config
	{
		internal static readonly int GAME_ID = 24204120;

		internal static readonly string VERSION = "v3.0.4";

		internal static readonly int VALUE_SHOW_POPUP_AFTER_RETENSION = 2;

		internal static readonly int VALUE_TARGET_FRAME_RATE = 60;

		internal static readonly int VALUE_DEFAULT_INTERSTITIAL_INTERVAL_SEC = 111;

		internal static bool didCrossPromotionIDSet()
		{
			if (GAME_ID.Equals(-1))
			{
				return false;
			}
			return true;
		}
	}
}
