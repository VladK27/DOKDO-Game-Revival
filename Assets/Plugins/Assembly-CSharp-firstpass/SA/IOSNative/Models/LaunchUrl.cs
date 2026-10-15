using System;

namespace SA.IOSNative.Models
{
	public class LaunchUrl
	{
		private Uri _URI;

		private string _AbsoluteUrl = string.Empty;

		private string _SourceApplication = string.Empty;

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

		public string SourceApplication
		{
			get
			{
				return _SourceApplication;
			}
		}

		public LaunchUrl(string data)
		{
			string[] array = data.Split('|');
			_AbsoluteUrl = array[0];
			_SourceApplication = array[1];
			if (_AbsoluteUrl.Length > 0)
			{
				_URI = new Uri(_AbsoluteUrl);
			}
		}

		public LaunchUrl(string absoluteUrl, string sourceApplication)
		{
			_AbsoluteUrl = absoluteUrl;
			_SourceApplication = sourceApplication;
			if (_AbsoluteUrl.Length > 0)
			{
				_URI = new Uri(_AbsoluteUrl);
			}
		}
	}
}
