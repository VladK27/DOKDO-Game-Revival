using System;
using System.Collections.Generic;

public class MNT_BinaryReader
{
	protected ByteByffer _ReceivedDate;

	public ByteByffer ReceivedDate
	{
		get
		{
			return _ReceivedDate;
		}
	}

	public MNT_BinaryReader(byte[] data)
	{
		_ReceivedDate = new ByteByffer(data);
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
		return default(T);
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

	public T[] ReadArray<T>()
	{
		return ReadList<T>().ToArray();
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
