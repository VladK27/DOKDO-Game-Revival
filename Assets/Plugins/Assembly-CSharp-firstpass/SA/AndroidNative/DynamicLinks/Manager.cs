using System;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks
{
	public class Manager : Singleton<Manager>
	{
		public Action<ShortLinkResult> OnShortLinkReceived = delegate
		{
		};

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public string GetPendingDynamicLink()
		{
			return Proxy.GetPendingLink();
		}

		public void RequestShortDynamicLink(Link link)
		{
			Proxy.RequestLink(Json.Serialize(link.Serialize()));
		}

		public void ShortLinkReceived(string data)
		{
			string[] array = data.Split(new string[1] { "|" }, StringSplitOptions.None);
			ShortLinkResult obj = ((int.Parse(array[0]) != 0) ? new ShortLinkResult(new Error(array[1])) : new ShortLinkResult(array[1]));
			OnShortLinkReceived(obj);
		}
	}
}
