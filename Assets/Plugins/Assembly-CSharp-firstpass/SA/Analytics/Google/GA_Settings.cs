using System.Collections.Generic;
using UnityEngine;

namespace SA.Analytics.Google
{
	public class GA_Settings : ScriptableObject
	{
		public static string VERSION_NUMBER = "4.7/21";

		[SerializeField]
		public List<Profile> accounts = new List<Profile>();

		[SerializeField]
		public List<PlatfromBound> platfromBounds = new List<PlatfromBound>();

		public bool showAdditionalParams;

		public bool showAdvancedParams;

		public bool showCParams;

		public bool showAccounts = true;

		public bool showPlatfroms;

		public bool showTestingMode;

		public string AppName = "My App";

		public string AppVersion = "0.0.1";

		public bool UseHTTPS;

		public bool StringEscaping = true;

		public bool EditorAnalytics = true;

		public bool IsDisabled;

		public bool IsTestingModeEnabled;

		public int TestingModeAccIndex;

		public bool IsRequetsCachingEnabled = true;

		public bool IsQueueTimeEnabled = true;

		public bool AutoLevelTracking = true;

		public string LevelPrefix = "Level_";

		public string LevelPostfix = string.Empty;

		public bool AutoAppQuitTracking = true;

		public bool AutoCampaignTracking;

		public bool AutoExceptionTracking = true;

		public bool AutoAppResumeTracking = true;

		public bool SubmitSystemInfoOnFirstLaunch = true;

		public bool UsePlayerSettingsForAppInfo = true;

		public bool EnableFirebase;

		public string FirebaseAppId = string.Empty;

		private const string AnalyticsSettingsAssetName = "GA_Settings";

		private const string AnalyticsSettingsAssetExtension = ".asset";

		private static GA_Settings instance;

		public static GA_Settings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Resources.Load("GA_Settings") as GA_Settings;
					if (instance == null)
					{
						instance = ScriptableObject.CreateInstance<GA_Settings>();
					}
				}
				return instance;
			}
		}

		public void UpdateVersionAndName()
		{
		}

		public void AddProfile(Profile p)
		{
			accounts.Add(p);
		}

		public void RemoveProfile(Profile p)
		{
			accounts.Remove(p);
		}

		public void SetProfileIndexForPlatfrom(RuntimePlatform platfrom, int index, bool IsTesting)
		{
			foreach (PlatfromBound platfromBound2 in platfromBounds)
			{
				if (platfromBound2.platfrom.Equals(platfrom))
				{
					if (IsTesting)
					{
						platfromBound2.profileIndexTestMode = index;
					}
					else
					{
						platfromBound2.profileIndex = index;
					}
					return;
				}
			}
			PlatfromBound platfromBound = new PlatfromBound();
			platfromBound.platfrom = platfrom;
			platfromBound.profileIndex = 0;
			platfromBound.profileIndexTestMode = 0;
			if (IsTesting)
			{
				platfromBound.profileIndexTestMode = index;
			}
			else
			{
				platfromBound.profileIndex = index;
			}
			platfromBounds.Add(platfromBound);
		}

		public int GetProfileIndexForPlatfrom(RuntimePlatform platfrom, bool IsTestMode)
		{
			foreach (PlatfromBound platfromBound in platfromBounds)
			{
				if (platfromBound.platfrom.Equals(platfrom))
				{
					int num = platfromBound.profileIndex;
					if (IsTestMode)
					{
						num = platfromBound.profileIndexTestMode;
					}
					if (num < accounts.Count)
					{
						return num;
					}
					return 0;
				}
			}
			return 0;
		}

		public string[] GetProfileNames()
		{
			List<string> list = new List<string>();
			foreach (Profile account in accounts)
			{
				list.Add(account.Name);
			}
			return list.ToArray();
		}

		public int GetProfileIndex(Profile p)
		{
			int num = 0;
			string[] profileNames = GetProfileNames();
			string[] array = profileNames;
			foreach (string text in array)
			{
				if (text.Equals(p.Name))
				{
					return num;
				}
				num++;
			}
			return 0;
		}

		public Profile GetCurentProfile()
		{
			return GetPrfileForPlatfrom(Application.platform, IsTestingModeEnabled);
		}

		public Profile GetPrfileForPlatfrom(RuntimePlatform platfrom, bool IsTestMode)
		{
			if (accounts.Count == 0)
			{
				return new Profile();
			}
			return accounts[GetProfileIndexForPlatfrom(platfrom, IsTestMode)];
		}
	}
}
