using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MNT_BinaryWriter
{
	protected BinaryWriter writer;

	protected MemoryStream buffer;

	public MNT_BinaryWriter()
	{
		buffer = new MemoryStream();
		writer = new BinaryWriter(buffer);
	}

	public byte[] GetBytes()
	{
		return buffer.ToArray();
	}

	public void WriteValue<T>(T data)
	{
		if (typeof(T).Equals(typeof(byte)))
		{
			writer.Write(Convert.ToByte(data));
		}
		else if (typeof(T).Equals(typeof(int)))
		{
			writer.Write(Convert.ToInt32(data));
		}
		else if (typeof(T).Equals(typeof(float)))
		{
			writer.Write(Convert.ToSingle(data));
		}
		else if (typeof(T).Equals(typeof(long)))
		{
			writer.Write(Convert.ToInt64(data));
		}
		else if (typeof(T).Equals(typeof(bool)))
		{
			writer.Write(Convert.ToBoolean(data));
		}
		else if (typeof(T).Equals(typeof(short)))
		{
			writer.Write(Convert.ToInt16(data));
		}
		else if (typeof(T).Equals(typeof(string)))
		{
			string s = Convert.ToString(data);
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			WriteValue(bytes.Length);
			writer.Write(bytes);
		}
	}

	public void WriteList<T>(List<T> data)
	{
		WriteValue(data.Count);
		foreach (T datum in data)
		{
			WriteValue(datum);
		}
	}

	public void WriteArray<T>(T[] data)
	{
		WriteList(new List<T>(data));
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
}
