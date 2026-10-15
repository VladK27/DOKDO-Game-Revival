using System;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class TVAppController : Singleton<TVAppController>
{
	private bool _IsRuningOnTVDevice;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action DeviceTypeChecked__BackingField = () =>
	{
	};

	public bool IsRuningOnTVDevice
	{
		get
		{
			return _IsRuningOnTVDevice;
		}
	}

	public static event Action DeviceTypeChecked
	{
		add
		{
			Action action = DeviceTypeChecked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceTypeChecked__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DeviceTypeChecked__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeviceTypeChecked__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void CheckForATVDevice()
	{
		AN_TVControllerProxy.AN_CheckForATVDevice();
	}

	private void OnDeviceStateResponce(string data)
	{
		if (data.Equals("1"))
		{
			_IsRuningOnTVDevice = true;
		}
		DeviceTypeChecked__BackingField();
	}
}
