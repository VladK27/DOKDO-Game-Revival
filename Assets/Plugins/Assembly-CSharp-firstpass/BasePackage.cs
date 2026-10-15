using System.IO;

public abstract class BasePackage
{
	protected MemoryStream buffer = new MemoryStream();

	protected BinaryWriter writer;

	public void initWriter()
	{
		writer = new BinaryWriter(buffer);
		writeInt(getId());
	}

	public abstract int getId();

	public byte[] getBytes()
	{
		return buffer.ToArray();
	}

	public void send()
	{
		NetworkManagerExample.send(this);
	}

	public void writeInt(int val)
	{
		writer.Write(val);
	}

	public void writeString(string val)
	{
	}

	public void writeFloat(float val)
	{
		writer.Write(val);
	}
}
