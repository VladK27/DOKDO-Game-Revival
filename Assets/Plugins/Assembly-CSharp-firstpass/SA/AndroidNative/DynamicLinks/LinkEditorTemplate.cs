using System;
using UnityEngine;

namespace SA.AndroidNative.DynamicLinks
{
	[Serializable]
	public class LinkEditorTemplate
	{
		[SerializeField]
		public string Host = string.Empty;

		[SerializeField]
		public string Scheme = string.Empty;
	}
}
