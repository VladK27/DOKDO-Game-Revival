using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SA.IOSDeploy
{
	public class ISD_Settings : ScriptableObject
	{
		public const string VERSION_NUMBER = "3.4/21";

		public bool IsfwSettingOpen;

		public bool IsLibSettingOpen;

		public bool IslinkerSettingOpne;

		public bool IscompilerSettingsOpen;

		public bool IsPlistSettingsOpen;

		public bool IsLanguageSettingOpen = true;

		public bool IsDefFrameworksOpen;

		public bool IsDefLibrariesOpen;

		public bool IsBuildSettingsOpen;

		public int ToolbarIndex;

		public bool enableBitCode;

		public bool enableTestability;

		public bool generateProfilingCode;

		public List<Framework> Frameworks = new List<Framework>();

		public List<Lib> Libraries = new List<Lib>();

		public List<Flag> Flags = new List<Flag>();

		public List<Variable> PlistVariables = new List<Variable>();

		public List<VariableId> VariableDictionary = new List<VariableId>();

		public List<string> langFolders = new List<string>();

		private const string ISDAssetName = "ISD_Settings";

		private const string ISDAssetExtension = ".asset";

		private static ISD_Settings instance;

		public static ISD_Settings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = Resources.Load("ISD_Settings") as ISD_Settings;
					if (instance == null)
					{
						instance = ScriptableObject.CreateInstance<ISD_Settings>();
					}
				}
				return instance;
			}
		}

		public void AddVariable(Variable var)
		{
			foreach (Variable item in PlistVariables.ToList())
			{
				if (item.Name.Equals(var.Name))
				{
					PlistVariables.Remove(item);
				}
			}
			PlistVariables.Add(var);
		}

		public void AddVariableToDictionary(string uniqueIdKey, Variable var)
		{
			VariableId variableId = new VariableId();
			variableId.uniqueIdKey = uniqueIdKey;
			variableId.VariableValue = var;
			VariableDictionary.Add(variableId);
		}

		public void RemoveVariable(Variable v, IList ListWithThisVariable)
		{
			if (Instance.PlistVariables.Contains(v))
			{
				Instance.PlistVariables.Remove(v);
				return;
			}
			foreach (VariableId item in VariableDictionary)
			{
				if (item.VariableValue.Equals(v))
				{
					VariableDictionary.Remove(item);
					string uniqueIdKey = item.uniqueIdKey;
					if (ListWithThisVariable.Contains(uniqueIdKey))
					{
						ListWithThisVariable.Remove(item.uniqueIdKey);
					}
					break;
				}
			}
		}

		public Variable getVariableByKey(string uniqueIdKey)
		{
			foreach (VariableId item in VariableDictionary)
			{
				if (item.uniqueIdKey.Equals(uniqueIdKey))
				{
					return item.VariableValue;
				}
			}
			return null;
		}

		public Variable GetVariableByName(string name)
		{
			foreach (Variable plistVariable in Instance.PlistVariables)
			{
				if (plistVariable.Name.Equals(name))
				{
					return plistVariable;
				}
			}
			return null;
		}

		public string getKeyFromVariable(Variable var)
		{
			foreach (VariableId item in VariableDictionary)
			{
				if (item.VariableValue.Equals(var))
				{
					return item.uniqueIdKey;
				}
			}
			return null;
		}

		public bool ContainsPlistVarWithName(string name)
		{
			foreach (Variable plistVariable in Instance.PlistVariables)
			{
				if (plistVariable.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public bool ContainsFramework(iOSFramework framework)
		{
			foreach (Framework framework2 in Instance.Frameworks)
			{
				if (framework2.Type.Equals(framework))
				{
					return true;
				}
			}
			return false;
		}

		public Framework GetFramework(iOSFramework framework)
		{
			foreach (Framework framework2 in Instance.Frameworks)
			{
				if (framework2.Type.Equals(framework))
				{
					return framework2;
				}
			}
			return null;
		}

		public Framework AddFramework(iOSFramework framework, bool embaded = false)
		{
			Framework framework2 = GetFramework(framework);
			if (framework2 == null)
			{
				framework2 = new Framework(framework, embaded);
				Instance.Frameworks.Add(framework2);
			}
			return framework2;
		}

		public bool ContainsLibWithName(string name)
		{
			foreach (Lib library in Instance.Libraries)
			{
				if (library.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public Lib GetLibrary(iOSLibrary library)
		{
			foreach (Lib library2 in instance.Libraries)
			{
				if (library2.Type.Equals(library))
				{
					return library2;
				}
			}
			return null;
		}

		public Lib AddLibrary(iOSLibrary library)
		{
			Lib lib = GetLibrary(library);
			if (lib == null)
			{
				lib = new Lib(library);
				Instance.Libraries.Add(lib);
			}
			return lib;
		}

		public void AddLinkerFlag(string s)
		{
			Flag flag = new Flag();
			flag.Name = s;
			flag.Type = FlagType.LinkerFlag;
			foreach (Flag flag2 in Flags)
			{
				if (flag2.Type.Equals(FlagType.LinkerFlag) && flag2.Name.Equals(s))
				{
					break;
				}
			}
			Flags.Add(flag);
		}
	}
}
