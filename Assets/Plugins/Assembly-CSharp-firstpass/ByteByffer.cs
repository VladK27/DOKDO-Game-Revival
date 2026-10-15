using System;
using System.Text;

public class ByteByffer
{
	private byte[] _buffer;

	public int pointer;

	public byte[] buffer
	{
		get
		{
			return _buffer;
		}
	}

	public ByteByffer(byte[] buf)
	{
		_buffer = buf;
	}

	public byte ReadByte()
	{
		byte result = _buffer[pointer];
		pointer++;
		return result;
	}

	public int ReadInt()
	{
		int result = BitConverter.ToInt32(_buffer, pointer);
		pointer += 4;
		return result;
	}

	public float ReadFloat()
	{
		float result = BitConverter.ToSingle(_buffer, pointer);
		pointer += 4;
		return result;
	}

	public long ReadLong()
	{
		long result = BitConverter.ToInt64(_buffer, pointer);
		pointer += 8;
		return result;
	}

	public bool ReadBool()
	{
		bool result = BitConverter.ToBoolean(_buffer, pointer);
		pointer++;
		return result;
	}

	public short ReadShort()
	{
		short result = BitConverter.ToInt16(_buffer, pointer);
		pointer += 2;
		return result;
	}

	public string ReadString(int length)
	{
		byte[] array = new byte[length];
		Buffer.BlockCopy(_buffer, pointer, array, 0, length);
		pointer += length;
		return Encoding.UTF8.GetString(array);
	}
}
