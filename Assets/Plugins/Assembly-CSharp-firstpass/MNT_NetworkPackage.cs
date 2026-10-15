using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MNT_NetworkPackage
{
	protected MemoryStream buffer;

	protected BinaryWriter writer;

	protected ByteByffer _ReceivedDate;

	private int _Id;

	public int Id
	{
		get
		{
			return _Id;
		}
	}

	public ByteByffer ReceivedDate
	{
		get
		{
			return _ReceivedDate;
		}
	}

	public MNT_NetworkPackage(int id)
	{
		buffer = new MemoryStream();
		writer = new BinaryWriter(buffer);
		_Id = id;
		WriteValue(id);
	}

	public MNT_NetworkPackage(byte[] data)
	{
		_ReceivedDate = new ByteByffer(data);
		_Id = ReadValue<int>();
	}

	public byte[] GetBytes()
	{
		if (buffer == null)
		{
			return _ReceivedDate.buffer;
		}
		return buffer.ToArray();
	}

	public string GetBase64DataFormat()
	{
		return Convert.ToBase64String(GetBytes());
	}

	public void WriteValue<T>(T data)
	{
		if (typeof(T).Equals(typeof(byte)))
		{
			writer.Write(Convert.ToByte(data));
			return;
		}
		if (typeof(T).Equals(typeof(int)))
		{
			writer.Write(Convert.ToInt32(data));
			return;
		}
		if (typeof(T).Equals(typeof(float)))
		{
			writer.Write(Convert.ToSingle(data));
			return;
		}
		if (typeof(T).Equals(typeof(long)))
		{
			writer.Write(Convert.ToInt64(data));
			return;
		}
		if (typeof(T).Equals(typeof(bool)))
		{
			writer.Write(Convert.ToBoolean(data));
			return;
		}
		if (typeof(T).Equals(typeof(short)))
		{
			writer.Write(Convert.ToInt16(data));
			return;
		}
		if (typeof(T).Equals(typeof(string)))
		{
			string s = Convert.ToString(data);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			WriteValue(bytes.Length);
			writer.Write(bytes);
			return;
		}
		if (typeof(T).Equals(typeof(Vector3)))
		{
			Vector3 vector = (Vector3)Convert.ChangeType(data, typeof(Vector3));
			writer.Write(vector.x);
			writer.Write(vector.y);
			writer.Write(vector.z);
			return;
		}
		for (Type type = typeof(T); type != null; type = type.BaseType)
		{
			if (type.Equals(typeof(MNT_NetworkPackage)))
			{
				WriteMNTPack(data as MNT_NetworkPackage);
				return;
			}
		}
		Debug.Log("MNT_NetworkPackage::WriteValue Unsupported Type Ignored " + typeof(T));
	}

	private void WriteMNTPack(MNT_NetworkPackage pack)
	{
		byte[] bytes = pack.GetBytes();
		WriteArray(bytes);
	}

	public Vector3 ReadVector3()
	{
		return new Vector3
		{
			x = ReadValue<float>(),
			y = ReadValue<float>(),
			z = ReadValue<float>()
		};
	}

	public MNT_NetworkPackage ReadNetworkPackage()
	{
		byte[] data = ReadArray<byte>();
		return new MNT_NetworkPackage(data);
	}

	public T ReadValue<T>()
	{
		if (typeof(T).Equals(typeof(byte)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadByte(), typeof(T));
		}
		if (typeof(T).Equals(typeof(int)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadInt(), typeof(T));
		}
		if (typeof(T).Equals(typeof(float)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadFloat(), typeof(T));
		}
		if (typeof(T).Equals(typeof(long)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadLong(), typeof(T));
		}
		if (typeof(T).Equals(typeof(bool)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadBool(), typeof(T));
		}
		if (typeof(T).Equals(typeof(short)))
		{
			return (T)Convert.ChangeType(ReceivedDate.ReadShort(), typeof(T));
		}
		if (typeof(T).Equals(typeof(string)))
		{
			int length = ReadValue<int>();
			return (T)Convert.ChangeType(ReceivedDate.ReadString(length), typeof(T));
		}
		if (typeof(T).Equals(typeof(MNT_NetworkPackage)))
		{
			byte[] data = ReadArray<byte>();
			MNT_NetworkPackage value = new MNT_NetworkPackage(data);
			return (T)Convert.ChangeType(value, typeof(T));
		}
		if (typeof(T).Equals(typeof(Vector3)))
		{
			return (T)Convert.ChangeType(new Vector3
			{
				x = ReadValue<float>(),
				y = ReadValue<float>(),
				z = ReadValue<float>()
			}, typeof(T));
		}
		return default(T);
	}

	public void WriteList<T>(List<T> data)
	{
		WriteValue(data.Count);
		foreach (T datum in data)
		{
			WriteValue(datum);
		}
	}

	public List<T> ReadList<T>()
	{
		List<T> list = new List<T>();
		int num = ReadValue<int>();
		for (int i = 0; i < num; i++)
		{
			list.Add(ReadValue<T>());
		}
		return list;
	}

	public void WriteArray<T>(T[] data)
	{
		WriteList(new List<T>(data));
	}

	public T[] ReadArray<T>()
	{
		return ReadList<T>().ToArray();
	}

	public void WriteDictionary<K, V>(Dictionary<K, V> data)
	{
		WriteValue(data.Count);
		foreach (KeyValuePair<K, V> datum in data)
		{
			WriteValue(datum.Key);
			WriteValue(datum.Value);
		}
	}

	public Dictionary<K, V> ReadDictionary<K, V>()
	{
		Dictionary<K, V> dictionary = new Dictionary<K, V>();
		int num = ReadValue<int>();
		for (int i = 0; i < num; i++)
		{
			dictionary.Add(ReadValue<K>(), ReadValue<V>());
		}
		return dictionary;
	}
}
