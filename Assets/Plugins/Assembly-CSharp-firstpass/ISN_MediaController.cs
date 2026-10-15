using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_MediaController : Singleton<ISN_MediaController>
{
	private MP_MediaItem _NowPlayingItem;

	private MP_MusicPlaybackState _State;

	private List<MP_MediaItem> _CurrentQueue = new List<MP_MediaItem>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<MP_MediaPickerResult> ActionMediaPickerResult__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<MP_MediaPickerResult> ActionQueueUpdated__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<MP_MediaItem> ActionNowPlayingItemChanged__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<MP_MusicPlaybackState> ActionPlaybackStateChanged__BackingField = delegate
	{
	};

	public MP_MediaItem NowPlayingItem
	{
		get
		{
			return _NowPlayingItem;
		}
	}

	public List<MP_MediaItem> CurrentQueue
	{
		get
		{
			return _CurrentQueue;
		}
	}

	public MP_MusicPlaybackState State
	{
		get
		{
			return _State;
		}
	}

	public static event Action<MP_MediaPickerResult> ActionMediaPickerResult
	{
		add
		{
			Action<MP_MediaPickerResult> action = ActionMediaPickerResult__BackingField;
			Action<MP_MediaPickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMediaPickerResult__BackingField, (Action<MP_MediaPickerResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<MP_MediaPickerResult> action = ActionMediaPickerResult__BackingField;
			Action<MP_MediaPickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionMediaPickerResult__BackingField, (Action<MP_MediaPickerResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<MP_MediaPickerResult> ActionQueueUpdated
	{
		add
		{
			Action<MP_MediaPickerResult> action = ActionQueueUpdated__BackingField;
			Action<MP_MediaPickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionQueueUpdated__BackingField, (Action<MP_MediaPickerResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<MP_MediaPickerResult> action = ActionQueueUpdated__BackingField;
			Action<MP_MediaPickerResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionQueueUpdated__BackingField, (Action<MP_MediaPickerResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<MP_MediaItem> ActionNowPlayingItemChanged
	{
		add
		{
			Action<MP_MediaItem> action = ActionNowPlayingItemChanged__BackingField;
			Action<MP_MediaItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNowPlayingItemChanged__BackingField, (Action<MP_MediaItem>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<MP_MediaItem> action = ActionNowPlayingItemChanged__BackingField;
			Action<MP_MediaItem> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionNowPlayingItemChanged__BackingField, (Action<MP_MediaItem>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<MP_MusicPlaybackState> ActionPlaybackStateChanged
	{
		add
		{
			Action<MP_MusicPlaybackState> action = ActionPlaybackStateChanged__BackingField;
			Action<MP_MusicPlaybackState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlaybackStateChanged__BackingField, (Action<MP_MusicPlaybackState>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<MP_MusicPlaybackState> action = ActionPlaybackStateChanged__BackingField;
			Action<MP_MusicPlaybackState> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionPlaybackStateChanged__BackingField, (Action<MP_MusicPlaybackState>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void SetRepeatMode(MP_MusicRepeatMode mode)
	{
	}

	public void SetShuffleMode(MP_MusicShuffleMode mode)
	{
	}

	public void Play()
	{
	}

	public void Pause()
	{
	}

	public void SkipToNextItem()
	{
	}

	public void SkipToBeginning()
	{
	}

	public void SkipToPreviousItem()
	{
	}

	public void ShowMediaPicker()
	{
	}

	public void SetCollection(params MP_MediaItem[] items)
	{
		List<string> list = new List<string>();
		foreach (MP_MediaItem mP_MediaItem in items)
		{
			list.Add(mP_MediaItem.Id);
		}
		SetCollection(list.ToArray());
	}

	public void AddItemWithProductID(string productID)
	{
	}

	public void SetCollection(params string[] itemIds)
	{
	}

	private List<MP_MediaItem> ParseMediaItemsList(string[] data, int index = 0)
	{
		List<MP_MediaItem> list = new List<MP_MediaItem>();
		for (int i = index; i < data.Length && !(data[i] == "endofline"); i += 8)
		{
			MP_MediaItem item = ParseMediaItemData(data, i);
			list.Add(item);
		}
		return list;
	}

	private MP_MediaItem ParseMediaItemData(string[] data, int index)
	{
		return new MP_MediaItem(data[index], data[index + 1], data[index + 2], data[index + 3], data[index + 4], data[index + 5], data[index + 6], data[index + 7]);
	}

	private void OnQueueUpdate(string data)
	{
		string[] data2 = data.Split('|');
		_CurrentQueue = ParseMediaItemsList(data2);
		MP_MediaPickerResult obj = new MP_MediaPickerResult(_CurrentQueue);
		ActionQueueUpdated__BackingField(obj);
	}

	private void OnQueueUpdateFailed(string errorData)
	{
		MP_MediaPickerResult obj = new MP_MediaPickerResult(errorData);
		ActionQueueUpdated__BackingField(obj);
	}

	private void OnMediaPickerResult(string data)
	{
		string[] data2 = data.Split('|');
		_CurrentQueue = ParseMediaItemsList(data2);
		MP_MediaPickerResult obj = new MP_MediaPickerResult(_CurrentQueue);
		ActionMediaPickerResult__BackingField(obj);
		ActionQueueUpdated__BackingField(obj);
	}

	private void OnMediaPickerFailed(string errorData)
	{
		MP_MediaPickerResult obj = new MP_MediaPickerResult(errorData);
		ActionMediaPickerResult__BackingField(obj);
	}

	private void OnNowPlayingItemchanged(string data)
	{
		string[] data2 = data.Split('|');
		_NowPlayingItem = ParseMediaItemData(data2, 0);
		ActionNowPlayingItemChanged__BackingField(_NowPlayingItem);
	}

	private void OnPlaybackStateChanged(string state)
	{
		int state2 = Convert.ToInt32(state);
		_State = (MP_MusicPlaybackState)state2;
		ActionPlaybackStateChanged__BackingField(_State);
	}
}
