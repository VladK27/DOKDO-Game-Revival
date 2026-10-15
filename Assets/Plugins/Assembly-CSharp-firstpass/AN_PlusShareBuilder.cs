using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using UnityEngine;

public class AN_PlusShareBuilder
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Result> OnPlusShareResult__BackingField;

	private const string LISTENER_OBJECT_NAME = "AN_PlusShareListener";

	private GameObject listenerObject;

	private string message;

	private List<Texture2D> images = new List<Texture2D>();

	public event Action<Result> OnPlusShareResult
	{
		add
		{
			Action<Result> action = OnPlusShareResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlusShareResult__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnPlusShareResult__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPlusShareResult__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public AN_PlusShareBuilder(string text)
	{
		message = text;
	}

	public void AddImage(Texture2D image)
	{
		images.Add(image);
	}

	public void Share()
	{
		listenerObject = new GameObject("AN_PlusShareListener");
		AN_PlusShareListener aN_PlusShareListener = listenerObject.AddComponent<AN_PlusShareListener>();
		aN_PlusShareListener.AttachBuilderCallback(PlusShareCallback);
		List<string> list = new List<string>();
		foreach (Texture2D image in images)
		{
			byte[] inArray = image.EncodeToPNG();
			list.Add(Convert.ToBase64String(inArray));
		}
		images.Clear();
		AN_SocialSharingProxy.GooglePlusShare(message, list.ToArray());
	}

	private void PlusShareCallback(Result result)
	{
		OnPlusShareResult__BackingField(result);
		UnityEngine.Object.Destroy(listenerObject);
		UnityEngine.Debug.Log("AN_PlusShareListener was destroyed object reference equals " + listenerObject);
	}
}
