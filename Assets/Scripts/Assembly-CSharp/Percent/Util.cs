using System.Text.RegularExpressions;
using Percent.View;
using UnityEngine;

namespace Percent
{
	public class Util
	{
		internal static string getPlatform()
		{
			return "android";
		}

		internal static string getRegion()
		{
			string region = PreciseLocale.GetRegion();
			string pattern = "^[A-Za-z]{2}$";
			if (Regex.IsMatch(region, pattern))
			{
				return region.ToUpper();
			}
			return "ZZ";
		}

		internal static Percent.View.ScreenOrientation getScreenOrientation()
		{
			Percent.View.ScreenOrientation result = Percent.View.ScreenOrientation.VERTICAL;
			switch (Screen.orientation)
			{
			case UnityEngine.ScreenOrientation.LandscapeLeft:
				result = Percent.View.ScreenOrientation.HORIZONTAL;
				break;
			case UnityEngine.ScreenOrientation.LandscapeRight:
				result = Percent.View.ScreenOrientation.HORIZONTAL;
				break;
			case UnityEngine.ScreenOrientation.Portrait:
				result = Percent.View.ScreenOrientation.VERTICAL;
				break;
			case UnityEngine.ScreenOrientation.PortraitUpsideDown:
				result = Percent.View.ScreenOrientation.VERTICAL;
				break;
			}
			return result;
		}

		internal static void goMarket(string marketUrl)
		{
			Application.OpenURL(marketUrl);
		}
	}
}
