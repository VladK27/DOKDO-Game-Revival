using System.Collections.Generic;
using SA.Common.Data;
using SA.Common.Util;

public class CK_Record
{
	private static Dictionary<int, CK_Record> _Records = new Dictionary<int, CK_Record>();

	private CK_RecordID _Id;

	private string _Type = string.Empty;

	private Dictionary<string, string> _Data = new Dictionary<string, string>();

	private int _internalId;

	public CK_RecordID Id
	{
		get
		{
			return _Id;
		}
	}

	public string Type
	{
		get
		{
			return _Type;
		}
	}

	public int Internal_Id
	{
		get
		{
			return _internalId;
		}
	}

	public CK_Record(CK_RecordID id, string type)
	{
		_Id = id;
		_Type = type;
		IndexRecord();
	}

	public CK_Record(string name, string template)
	{
		_Id = new CK_RecordID(name);
		string[] array = template.Split('|');
		_Type = array[0];
		for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 2)
		{
			SetObject(array[i], array[i + 1]);
		}
		IndexRecord();
	}

	public void SetObject(string key, string value)
	{
		if (_Data.ContainsKey(key))
		{
			_Data[key] = value;
		}
		else
		{
			_Data.Add(key, value);
		}
	}

	public string GetObject(string key)
	{
		if (_Data.ContainsKey(key))
		{
			return _Data[key];
		}
		return string.Empty;
	}

	private void IndexRecord()
	{
		_internalId = IdFactory.NextId;
		_Records.Add(_internalId, this);
	}

	public void UpdateRecord()
	{
		List<string> list = new List<string>();
		List<string> list2 = new List<string>();
		foreach (KeyValuePair<string, string> datum in _Data)
		{
			list.Add(datum.Key);
			list2.Add(datum.Value);
		}
		ISN_CloudKit.UpdateRecord_Object(Internal_Id, Type, Converter.SerializeArray(list.ToArray()), Converter.SerializeArray(list2.ToArray()), Id.Internal_Id);
	}

	public static CK_Record GetRecordByInternalId(int id)
	{
		return _Records[id];
	}
}
