using UnityEngine;

namespace Percent
{
	public class Interstitial : MonoBehaviour
	{
		private int interval = Config.VALUE_DEFAULT_INTERSTITIAL_INTERVAL_SEC;

		private float lastShowTimeSec;

		private const int NOT_SHOW = -1;

		private const int EVERYTIME = 0;

		private readonly string PREF_LAST_INTERVAL_FROM_SERVER = "percentLastInterval";

		internal int Interval
		{
			get
			{
				return interval;
			}
			set
			{
				if (value < -1)
				{
					Logger.error("Interstitial interval has NOT valid range.");
					return;
				}
				interval = value;
				PlayerPrefs.SetInt(PREF_LAST_INTERVAL_FROM_SERVER, interval);
			}
		}

		private void Awake()
		{
			lastShowTimeSec = Time.realtimeSinceStartup;
			tryToLoadLastInterval();
		}

		private void tryToLoadLastInterval()
		{
			if (PlayerPrefs.HasKey(PREF_LAST_INTERVAL_FROM_SERVER))
			{
				interval = PlayerPrefs.GetInt(PREF_LAST_INTERVAL_FROM_SERVER);
			}
			else
			{
				interval = Config.VALUE_DEFAULT_INTERSTITIAL_INTERVAL_SEC;
			}
		}

		internal bool isShowable()
		{
			bool flag = true;
			switch (interval)
			{
			case -1:
				return false;
			case 0:
				return true;
			default:
				return isTimeToShow();
			}
		}

		private bool isTimeToShow()
		{
			float num = Time.realtimeSinceStartup - lastShowTimeSec;
			if (num >= (float)interval)
			{
				return true;
			}
			return false;
		}

		internal void stampLastShowTime()
		{
			lastShowTimeSec = Time.realtimeSinceStartup;
		}
	}
}
