using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;

public class GK_SavedGame
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<GK_SaveDataLoaded> ActionDataLoaded__BackingField = delegate
	{
	};

	private string _Id;

	private string _Name;

	private string _DeviceName;

	private DateTime _ModificationDate;

	private byte[] _Data;

	private bool _IsDataLoaded;

	public string Id
	{
		get
		{
			return _Id;
		}
	}

	public string Name
	{
		get
		{
			return _Name;
		}
	}

	public string DeviceName
	{
		get
		{
			return _DeviceName;
		}
	}

	public DateTime ModificationDate
	{
		get
		{
			return _ModificationDate;
		}
	}

	public byte[] Data
	{
		get
		{
			return _Data;
		}
	}

	public bool IsDataLoaded
	{
		get
		{
			return _IsDataLoaded;
		}
	}

	public event Action<GK_SaveDataLoaded> ActionDataLoaded
	{
		add
		{
			Action<GK_SaveDataLoaded> action = ActionDataLoaded__BackingField;
			Action<GK_SaveDataLoaded> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataLoaded__BackingField, (Action<GK_SaveDataLoaded>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GK_SaveDataLoaded> action = ActionDataLoaded__BackingField;
			Action<GK_SaveDataLoaded> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionDataLoaded__BackingField, (Action<GK_SaveDataLoaded>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public GK_SavedGame(string id, string name, string device, string dateString)
	{
		_Id = id;
		_Name = name;
		_DeviceName = device;
		_ModificationDate = DateTime.Parse(dateString);
	}

	public void LoadData()
	{
		Singleton<ISN_GameSaves>.Instance.LoadSaveData(this);
	}

	public void GenerateDataLoadEvent(string base64Data)
	{
		_Data = Convert.FromBase64String(base64Data);
		_IsDataLoaded = true;
		GK_SaveDataLoaded obj = new GK_SaveDataLoaded(this);
		ActionDataLoaded__BackingField(obj);
	}

	public void GenerateDataLoadFailedEvent(string erorrData)
	{
		Error error = new Error(erorrData);
		GK_SaveDataLoaded obj = new GK_SaveDataLoaded(error);
		ActionDataLoaded__BackingField(obj);
	}
}
