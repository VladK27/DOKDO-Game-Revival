using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_FilePicker : Singleton<ISN_FilePicker>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<ISN_FilePickerResult> MediaPickFinished__BackingField = delegate
	{
	};

	public static event Action<ISN_FilePickerResult> MediaPickFinished
	{
		add
		{
			Action<ISN_FilePickerResult> action = MediaPickFinished__BackingField;
			Action<ISN_FilePickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MediaPickFinished__BackingField, (Action<ISN_FilePickerResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ISN_FilePickerResult> action = MediaPickFinished__BackingField;
			Action<ISN_FilePickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref MediaPickFinished__BackingField, (Action<ISN_FilePickerResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void PickFromCameraRoll(int maxItemsCount = 0)
	{
	}

	private void OnSelectImagesComplete(string data)
	{
		string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		ISN_FilePickerResult iSN_FilePickerResult = new ISN_FilePickerResult();
		if (data.Equals(string.Empty))
		{
			MediaPickFinished__BackingField(iSN_FilePickerResult);
			return;
		}
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
		{
			string s = array[i];
			byte[] data2 = Convert.FromBase64String(s);
			Texture2D texture2D = new Texture2D(1, 1);
			texture2D.LoadImage(data2);
			texture2D.hideFlags = HideFlags.DontSave;
			iSN_FilePickerResult.PickedImages.Add(texture2D);
		}
		MediaPickFinished__BackingField(iSN_FilePickerResult);
	}
}
