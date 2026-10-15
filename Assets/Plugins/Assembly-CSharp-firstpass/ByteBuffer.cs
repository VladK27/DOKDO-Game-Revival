using System;

public class ByteBuffer
{
	private byte[] buffer;

	public int pointer;

	public ByteBuffer(byte[] buf)
	{
		buffer = buf;
	}

	public int readInt()
	{
		int result = BitConverter.ToInt32(buffer, pointer);
		pointer += 4;
		return result;
	}

	public float readFloat()
	{
		float result = BitConverter.ToSingle(buffer, pointer);
		pointer += 4;
		return result;
	}
}
