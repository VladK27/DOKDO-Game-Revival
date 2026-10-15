using System.Collections.Generic;
using SA.Common.Util;

public class GP_AppInviteBuilder
{
	private int _Id;

	public int Id
	{
		get
		{
			return _Id;
		}
	}

	public GP_AppInviteBuilder(string title)
	{
		_Id = IdFactory.NextId;
		AN_AppInvitesProxy.CreateBuilder(_Id, title);
	}

	public void SetMessage(string msg)
	{
		AN_AppInvitesProxy.SetMessage(_Id, msg);
	}

	public void SetDeepLink(string url)
	{
		AN_AppInvitesProxy.SetDeepLink(_Id, url);
	}

	public void SetCallToActionText(string actionText)
	{
		AN_AppInvitesProxy.SetCallToActionText(_Id, actionText);
	}

	public void SetGoogleAnalyticsTrackingId(string trackingId)
	{
		AN_AppInvitesProxy.SetGoogleAnalyticsTrackingId(_Id, trackingId);
	}

	public void SetAndroidMinimumVersionCode(int versionCode)
	{
		AN_AppInvitesProxy.SetAndroidMinimumVersionCode(_Id, versionCode);
	}

	public void SetAdditionalReferralParameters(Dictionary<string, string> referralParameters)
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, string> referralParameter in referralParameters)
		{
			list.Add(referralParameter.Value);
			list2.Add(referralParameter.Key);
		}
		string values = AndroidNative.ArrayToString(list.ToArray());
		string keys = AndroidNative.ArrayToString(list2.ToArray());
		AN_AppInvitesProxy.SetAdditionalReferralParameters(_Id, keys, values);
	}
}
