using System;
using SA.Common.Pattern;
using UnityEngine;

public class ISN_Logger : Singleton<ISN_Logger>
{
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Create()
	{
	}

	public static void Log(object message, LogType logType = LogType.Log)
	{
		Singleton<ISN_Logger>.Instance.Create();
		if (message != null && !IOSNativeSettings.Instance.DisablePluginLogs && Application.isEditor)
		{
			ISNEditorLog(logType, message);
		}
	}

	private static void ISNEditorLog(LogType logType, object message)
	{
		switch (logType)
		{
		case LogType.Error:
			Debug.LogError(message);
			break;
		case LogType.Exception:
			Debug.LogException((Exception)message);
			break;
		case LogType.Warning:
			Debug.LogWarning(message);
			break;
		default:
			Debug.Log(message);
			break;
		}
	}
}
