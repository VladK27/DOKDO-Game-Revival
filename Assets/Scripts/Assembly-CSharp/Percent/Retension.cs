using UnityEngine;

namespace Percent
{
	public class Retension : MonoBehaviour
	{
		private string PREF_RETENSION_AFTER = "ShowRetensionAfter";

		private void Start()
		{
			if (!isAllowToShow())
			{
				decreaseRetensionAfterPref();
			}
		}

		internal bool isAllowToShow()
		{
			if (getwillShowAfterRetensionPref() <= 0)
			{
				return true;
			}
			return false;
		}

		private int getwillShowAfterRetensionPref()
		{
			if (!PlayerPrefs.HasKey(PREF_RETENSION_AFTER))
			{
				PlayerPrefs.SetInt(PREF_RETENSION_AFTER, Config.VALUE_SHOW_POPUP_AFTER_RETENSION);
			}
			return PlayerPrefs.GetInt(PREF_RETENSION_AFTER);
		}

		private void decreaseRetensionAfterPref()
		{
			if (!PlayerPrefs.HasKey(PREF_RETENSION_AFTER))
			{
				PlayerPrefs.SetInt(PREF_RETENSION_AFTER, Config.VALUE_SHOW_POPUP_AFTER_RETENSION);
			}
			int value = PlayerPrefs.GetInt(PREF_RETENSION_AFTER) - 1;
			PlayerPrefs.SetInt(PREF_RETENSION_AFTER, value);
			if (value.Equals(0))
			{
			}
		}
	}
}
