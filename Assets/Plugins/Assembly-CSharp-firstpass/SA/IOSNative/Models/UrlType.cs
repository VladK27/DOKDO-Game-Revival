using System;
using System.Collections.Generic;

namespace SA.IOSNative.Models
{
	[Serializable]
	public class UrlType
	{
		public string Identifier = string.Empty;

		public List<string> Schemes = new List<string>();

		public bool IsOpen = true;

		public UrlType(string identifier)
		{
			Identifier = identifier;
		}

		public void AddSchemes(string schemes)
		{
			Schemes.Add(schemes);
		}
	}
}
