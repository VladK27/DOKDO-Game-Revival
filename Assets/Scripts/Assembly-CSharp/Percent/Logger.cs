using System.Diagnostics;
using UnityEngine;

namespace Percent
{
	public class Logger
	{
		internal static bool isDebugMode = true;

		internal static readonly string PREFIX_INFO = "[PERCENT] I : ";

		internal static readonly string PREFIX_ERROR = "[PERCENT] E : ";

		internal static readonly string PREFIX_WARNING = "[PERCENT] W : ";

		internal static readonly string PREFIX_DEBUG = "[PERCENT] D : ";

		[Conditional("PERCENT_INFO")]
		internal static void info(string message)
		{
			UnityEngine.Debug.Log(PREFIX_INFO + message);
		}

		internal static void warning(string message)
		{
			UnityEngine.Debug.LogWarning(PREFIX_WARNING + message);
		}

		internal static void error(string message)
		{
			UnityEngine.Debug.LogError(PREFIX_ERROR + message);
		}

		[Conditional("PERCENT_DEBUG")]
		internal static void debug(string message)
		{
			UnityEngine.Debug.Log(PREFIX_DEBUG + message);
		}
	}
}
