using System;

namespace Facebook.Unity.Example
{
	internal class AppInvites : MenuBase
	{
		protected override void GetGui()
		{
			if (Button("Android Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), null, base.HandleResult);
			}
			if (Button("Android Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/892708710750483"), new Uri("http://i.imgur.com/zkYlB.jpg"), base.HandleResult);
			}
			if (Button("iOS Invite"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), null, base.HandleResult);
			}
			if (Button("iOS Invite With Custom Image"))
			{
				base.Status = "Logged FB.AppEvent";
				FB.Mobile.AppInvite(new Uri("https://fb.me/810530068992919"), new Uri("http://i.imgur.com/zkYlB.jpg"), base.HandleResult);
			}
		}
	}
}
