using System;
using UnityEngine;

public class WP8PopupBase : MonoBehaviour
{
	public string title;

	public string message;

	public Action<WP8DialogResult> OnComplete = delegate
	{
	};
}
