using SA.Common.Pattern;
using UnityEngine;

public class ISNMediaExample : BaseIOSFeaturePreview
{
	private void Awake()
	{
		ISN_MediaController.ActionQueueUpdated += HandleActionQueueUpdated;
		ISN_MediaController.ActionMediaPickerResult += HandleActionMediaPickerResult;
		ISN_MediaController.ActionPlaybackStateChanged += HandleActionPlaybackStateChanged;
		ISN_MediaController.ActionNowPlayingItemChanged += HandleActionNowPlayingItemChanged;
	}

	private void HandleActionNowPlayingItemChanged(MP_MediaItem item)
	{
		ISN_Logger.Log("Now Playing Item Changed: " + Singleton<ISN_MediaController>.Instance.NowPlayingItem.Title);
	}

	private void HandleActionPlaybackStateChanged(MP_MusicPlaybackState state)
	{
		ISN_Logger.Log("Playback State Changed: " + Singleton<ISN_MediaController>.Instance.State);
	}

	private void HandleActionQueueUpdated(MP_MediaPickerResult res)
	{
		if (res.IsSucceeded)
		{
			foreach (MP_MediaItem item in res.Items)
			{
				ISN_Logger.Log("Item: " + item.Title + " / " + item.Id);
			}
			return;
		}
		ISN_Logger.Log("Queue Updated failed: " + res.Error.Message);
	}

	private void HandleActionMediaPickerResult(MP_MediaPickerResult res)
	{
		if (res.IsSucceeded)
		{
			ISN_Logger.Log("Media piacker Succeeded");
		}
		else
		{
			ISN_Logger.Log("Media piacker failed: " + res.Error.Message);
		}
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Selecting Songs", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Picker"))
		{
			Singleton<ISN_MediaController>.Instance.ShowMediaPicker();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Set Perviostly Picked Song"))
		{
			Singleton<ISN_MediaController>.Instance.Pause();
			ISN_Logger.Log(Singleton<ISN_MediaController>.Instance.CurrentQueue[0].Title);
			Singleton<ISN_MediaController>.Instance.SetCollection(Singleton<ISN_MediaController>.Instance.CurrentQueue[0]);
			Singleton<ISN_MediaController>.Instance.Play();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Controling Playback", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Play"))
		{
			Singleton<ISN_MediaController>.Instance.Play();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Pause"))
		{
			Singleton<ISN_MediaController>.Instance.Pause();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Next"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToNextItem();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Previous"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToPreviousItem();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Skip To Beginning"))
		{
			Singleton<ISN_MediaController>.Instance.SkipToBeginning();
		}
	}
}
