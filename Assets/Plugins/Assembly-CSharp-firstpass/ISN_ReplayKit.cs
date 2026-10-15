using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_ReplayKit : Singleton<ISN_ReplayKit>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> ActionRecordStarted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> ActionRecordStoped__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<ReplayKitVideoShareResult> ActionShareDialogFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Error> ActionRecordInterrupted__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<bool> ActionRecorderDidChangeAvailability__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action ActionRecordDiscard__BackingField = () =>
	{
	};

	private bool _IsRecodingAvailableToShare;

	public bool IsRecording
	{
		get
		{
			return false;
		}
	}

	public bool IsRecodingAvailableToShare
	{
		get
		{
			return _IsRecodingAvailableToShare;
		}
	}

	public bool IsAvailable
	{
		get
		{
			return false;
		}
	}

	public bool IsMicEnabled
	{
		get
		{
			return false;
		}
	}

	public static event Action<Result> ActionRecordStarted
	{
		add
		{
			Action<Result> action = ActionRecordStarted__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordStarted__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = ActionRecordStarted__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordStarted__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Result> ActionRecordStoped
	{
		add
		{
			Action<Result> action = ActionRecordStoped__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordStoped__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = ActionRecordStoped__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordStoped__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<ReplayKitVideoShareResult> ActionShareDialogFinished
	{
		add
		{
			Action<ReplayKitVideoShareResult> action = ActionShareDialogFinished__BackingField;
			Action<ReplayKitVideoShareResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionShareDialogFinished__BackingField, (Action<ReplayKitVideoShareResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ReplayKitVideoShareResult> action = ActionShareDialogFinished__BackingField;
			Action<ReplayKitVideoShareResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionShareDialogFinished__BackingField, (Action<ReplayKitVideoShareResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<Error> ActionRecordInterrupted
	{
		add
		{
			Action<Error> action = ActionRecordInterrupted__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordInterrupted__BackingField, (Action<Error>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Error> action = ActionRecordInterrupted__BackingField;
			Action<Error> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordInterrupted__BackingField, (Action<Error>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<bool> ActionRecorderDidChangeAvailability
	{
		add
		{
			Action<bool> action = ActionRecorderDidChangeAvailability__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecorderDidChangeAvailability__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = ActionRecorderDidChangeAvailability__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecorderDidChangeAvailability__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action ActionRecordDiscard
	{
		add
		{
			Action action = ActionRecordDiscard__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordDiscard__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ActionRecordDiscard__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRecordDiscard__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void StartRecording(bool microphoneEnabled = true)
	{
		_IsRecodingAvailableToShare = false;
	}

	public void StopRecording()
	{
	}

	public void DiscardRecording()
	{
		_IsRecodingAvailableToShare = false;
	}

	public void ShowVideoShareDialog()
	{
		_IsRecodingAvailableToShare = false;
	}

	private void OnRecorStartSuccess(string data)
	{
		Result obj = new Result();
		ActionRecordStarted__BackingField(obj);
	}

	private void OnRecorStartFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		ActionRecordStarted__BackingField(obj);
	}

	private void OnRecorStopFailed(string errorData)
	{
		Result obj = new Result(new Error(errorData));
		ActionRecordStoped__BackingField(obj);
	}

	private void OnRecorStopSuccess()
	{
		_IsRecodingAvailableToShare = true;
		Result obj = new Result();
		ActionRecordStoped__BackingField(obj);
	}

	private void OnRecordInterrupted(string errorData)
	{
		_IsRecodingAvailableToShare = false;
		Error obj = new Error(errorData);
		ActionRecordInterrupted__BackingField(obj);
	}

	private void OnRecorderDidChangeAvailability(string data)
	{
		ActionRecorderDidChangeAvailability__BackingField(IsAvailable);
	}

	private void OnSaveResult(string sourcesData)
	{
		string[] sourcesArray = Converter.ParseArray(sourcesData);
		ReplayKitVideoShareResult obj = new ReplayKitVideoShareResult(sourcesArray);
		ActionShareDialogFinished__BackingField(obj);
	}

	public void OnRecordDiscard(string data)
	{
		_IsRecodingAvailableToShare = false;
		ActionRecordDiscard__BackingField();
	}
}
