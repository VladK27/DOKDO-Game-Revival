using System;

namespace SA.IOSNative.Models
{
	public class UniversalLink
	{
		private Uri _URI;

		private string _AbsoluteUrl = string.Empty;

		public bool IsEmpty
		{
			get
			{
				return _AbsoluteUrl.Equals(string.Empty);
			}
		}

		public Uri URI
		{
			get
			{
				return _URI;
			}
		}

		public string Host
		{
			get
			{
				return _URI.Host;
			}
		}

		public string AbsoluteUrl
		{
			get
			{
				return _AbsoluteUrl;
			}
		}

		public UniversalLink(string absoluteUrl)
		{
			_AbsoluteUrl = absoluteUrl;
			if (_AbsoluteUrl.Length > 0)
			{
				_URI = new Uri(_AbsoluteUrl);
			}
		}
	}
}
