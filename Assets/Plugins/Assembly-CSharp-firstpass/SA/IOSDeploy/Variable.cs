using System;
using System.Collections.Generic;
using SA.Common.Util;

namespace SA.IOSDeploy
{
	[Serializable]
	public class Variable
	{
		public bool IsOpen = true;

		public bool IsListOpen = true;

		public string Name = string.Empty;

		public PlistValueTypes Type;

		public string StringValue = string.Empty;

		public int IntegerValue;

		public float FloatValue;

		public bool BooleanValue = true;

		public List<string> ChildrensIds = new List<string>();

		public void AddChild(Variable v)
		{
			if (Type.Equals(PlistValueTypes.Dictionary))
			{
				foreach (string childrensId in ChildrensIds)
				{
					Variable variableByKey = ISD_Settings.Instance.getVariableByKey(childrensId);
					if (variableByKey.Name.Equals(v.Name))
					{
						ISD_Settings.Instance.RemoveVariable(variableByKey, ChildrensIds);
						break;
					}
				}
			}
			else if (Type.Equals(PlistValueTypes.Array) && v.Type.Equals(PlistValueTypes.String))
			{
				foreach (string childrensId2 in ChildrensIds)
				{
					Variable variableByKey2 = ISD_Settings.Instance.getVariableByKey(childrensId2);
					if (variableByKey2.Type.Equals(PlistValueTypes.String) && v.StringValue.Equals(variableByKey2.StringValue))
					{
						ISD_Settings.Instance.RemoveVariable(variableByKey2, ChildrensIds);
						break;
					}
				}
			}
			string text = IdFactory.NextId.ToString();
			ISD_Settings.Instance.AddVariableToDictionary(text, v);
			ChildrensIds.Add(text);
		}
	}
}
