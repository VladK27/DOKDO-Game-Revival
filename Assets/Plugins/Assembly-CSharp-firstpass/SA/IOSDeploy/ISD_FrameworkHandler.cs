using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_FrameworkHandler : MonoBehaviour
	{
		private static List<Framework> _DefaultFrameworks;

		public static List<Framework> AvailableFrameworks
		{
			get
			{
				List<Framework> list = new List<Framework>();
				List<string> list2 = new List<string>(Enum.GetNames(typeof(iOSFramework)));
				foreach (Framework framework in ISD_Settings.Instance.Frameworks)
				{
					if (list2.Contains(framework.Type.ToString()))
					{
						list2.Remove(framework.Type.ToString());
					}
				}
				foreach (Framework defaultFramework in DefaultFrameworks)
				{
					if (list2.Contains(defaultFramework.Type.ToString()))
					{
						list2.Remove(defaultFramework.Type.ToString());
					}
				}
				foreach (iOSFramework value in Enum.GetValues(typeof(iOSFramework)))
				{
					if (list2.Contains(value.ToString()))
					{
						list.Add(new Framework(value));
					}
				}
				return list;
			}
		}

		public static List<Framework> DefaultFrameworks
		{
			get
			{
				if (_DefaultFrameworks == null)
				{
					_DefaultFrameworks = new List<Framework>();
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreText));
					_DefaultFrameworks.Add(new Framework(iOSFramework.AudioToolbox));
					_DefaultFrameworks.Add(new Framework(iOSFramework.AVFoundation));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CFNetwork));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreGraphics));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreLocation));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreMedia));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreMotion));
					_DefaultFrameworks.Add(new Framework(iOSFramework.CoreVideo));
					_DefaultFrameworks.Add(new Framework(iOSFramework.Foundation));
					_DefaultFrameworks.Add(new Framework(iOSFramework.iAd));
					_DefaultFrameworks.Add(new Framework(iOSFramework.MediaPlayer));
					_DefaultFrameworks.Add(new Framework(iOSFramework.OpenAL));
					_DefaultFrameworks.Add(new Framework(iOSFramework.OpenGLES));
					_DefaultFrameworks.Add(new Framework(iOSFramework.QuartzCore));
					_DefaultFrameworks.Add(new Framework(iOSFramework.SystemConfiguration));
					_DefaultFrameworks.Add(new Framework(iOSFramework.UIKit));
				}
				return _DefaultFrameworks;
			}
		}

		public static List<string> GetImportedFrameworks()
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
			string[] dirrExtensions = new string[1] { ".framework" };
			FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			DirectoryInfo[] directories = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
			files = files.Where((FileInfo f) => dirrExtensions.Contains(f.Extension.ToLower())).ToArray();
			directories = directories.Where((DirectoryInfo f) => dirrExtensions.Contains(f.Extension.ToLower())).ToArray();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				string item = fileInfo.Name;
				list.Add(item);
			}
			DirectoryInfo[] array2 = directories;
			foreach (DirectoryInfo directoryInfo2 in array2)
			{
				string item2 = directoryInfo2.Name;
				list.Add(item2);
			}
			return list;
		}

		public static List<string> GetImportedLibraries()
		{
			List<string> list = new List<string>();
			DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
			string[] fileExtensions = new string[2] { ".a", ".dylib" };
			FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			files = files.Where((FileInfo f) => fileExtensions.Contains(f.Extension.ToLower())).ToArray();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				string item = fileInfo.Name;
				list.Add(item);
			}
			return list;
		}

		public static string[] BaseFrameworksArray()
		{
			List<string> list = new List<string>(AvailableFrameworks.Capacity);
			foreach (Framework availableFramework in AvailableFrameworks)
			{
				list.Add(availableFramework.Type.ToString());
			}
			return list.ToArray();
		}
	}
}
