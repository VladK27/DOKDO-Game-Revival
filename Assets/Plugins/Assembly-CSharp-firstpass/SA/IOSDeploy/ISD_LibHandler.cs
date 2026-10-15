using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_LibHandler : MonoBehaviour
	{
		public static List<Lib> AvailableLibraries
		{
			get
			{
				List<Lib> list = new List<Lib>();
				List<string> list2 = new List<string>(Enum.GetNames(typeof(iOSLibrary)));
				foreach (Lib library in ISD_Settings.Instance.Libraries)
				{
					if (list2.Contains(library.Name))
					{
						list2.Remove(library.Name);
					}
				}
				foreach (iOSLibrary value in Enum.GetValues(typeof(iOSLibrary)))
				{
					if (list2.Contains(value.ToString()))
					{
						list.Add(new Lib(value));
					}
				}
				return list;
			}
		}

		public static string[] BaseLibrariesArray()
		{
			List<string> list = new List<string>(AvailableLibraries.Capacity);
			foreach (Lib availableLibrary in AvailableLibraries)
			{
				list.Add(availableLibrary.Name);
			}
			return list.ToArray();
		}

		public static string stringValueOf(iOSLibrary value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (array.Length > 0)
			{
				return array[0].Description;
			}
			return value.ToString();
		}

		public static object enumValueOf(string value)
		{
			Type typeFromHandle = typeof(iOSLibrary);
			string[] names = Enum.GetNames(typeFromHandle);
			string[] array = names;
			foreach (string value2 in array)
			{
				if (stringValueOf((iOSLibrary)Enum.Parse(typeFromHandle, value2)).Equals(value))
				{
					return Enum.Parse(typeFromHandle, value2);
				}
			}
			throw new ArgumentException("The string is not a description or value of the specified enum...\n " + value + " is not right enum");
		}
	}
}
