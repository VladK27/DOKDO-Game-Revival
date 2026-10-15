using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

public class iCloudManager : Singleton<iCloudManager>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<Result> OnCloudInitAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<iCloudData> OnCloudDataReceivedAction__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<List<iCloudData>> OnStoreDidChangeExternally__BackingField = delegate
	{
	};

	public static event Action<Result> OnCloudInitAction
	{
		add
		{
			Action<Result> action = OnCloudInitAction__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloudInitAction__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Result> action = OnCloudInitAction__BackingField;
			Action<Result> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloudInitAction__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<iCloudData> OnCloudDataReceivedAction
	{
		add
		{
			Action<iCloudData> action = OnCloudDataReceivedAction__BackingField;
			Action<iCloudData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloudDataReceivedAction__BackingField, (Action<iCloudData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<iCloudData> action = OnCloudDataReceivedAction__BackingField;
			Action<iCloudData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnCloudDataReceivedAction__BackingField, (Action<iCloudData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<List<iCloudData>> OnStoreDidChangeExternally
	{
		add
		{
			Action<List<iCloudData>> action = OnStoreDidChangeExternally__BackingField;
			Action<List<iCloudData>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnStoreDidChangeExternally__BackingField, (Action<List<iCloudData>>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<List<iCloudData>> action = OnStoreDidChangeExternally__BackingField;
			Action<List<iCloudData>> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnStoreDidChangeExternally__BackingField, (Action<List<iCloudData>>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void setString(string key, string val)
	{
	}

	public void setFloat(string key, float val)
	{
	}

	public void setData(string key, byte[] val)
	{
	}

	public void requestDataForKey(string key)
	{
	}

	private void OnCloudInit()
	{
		Result obj = new Result();
		OnCloudInitAction__BackingField(obj);
	}

	private void OnCloudInitFail()
	{
		Result obj = new Result(new Error());
		OnCloudInitAction__BackingField(obj);
	}

	private void OnCloudDataChanged(string data)
	{
		List<iCloudData> list = new List<iCloudData>();
		string[] array = data.Split('|');
		for (int i = 0; i < array.Length && !(array[i] == "endofline"); i += 2)
		{
			iCloudData item = new iCloudData(array[i], array[i + 1]);
			list.Add(item);
		}
		OnStoreDidChangeExternally__BackingField(list);
	}

	private void OnCloudData(string array)
	{
		string[] array2 = array.Split('|');
		iCloudData obj = new iCloudData(array2[0], array2[1]);
		OnCloudDataReceivedAction__BackingField(obj);
	}

	private void OnCloudDataEmpty(string array)
	{
		string[] array2 = array.Split('|');
		iCloudData obj = new iCloudData(array2[0], "null");
		OnCloudDataReceivedAction__BackingField(obj);
	}
}
