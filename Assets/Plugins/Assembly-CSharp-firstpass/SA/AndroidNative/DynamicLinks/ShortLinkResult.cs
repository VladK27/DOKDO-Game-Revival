using SA.Common.Models;

namespace SA.AndroidNative.DynamicLinks
{
	public class ShortLinkResult : Result
	{
		private string shortLink = string.Empty;

		public string ShortLink
		{
			get
			{
				return shortLink;
			}
		}

		public ShortLinkResult(Error error)
			: base(error)
		{
		}

		public ShortLinkResult(string link)
		{
			shortLink = link;
		}
	}
}
