public class MNT_RoomTemplate
{
	private int _size;

	private byte[] _data;

	public int size
	{
		get
		{
			return _size;
		}
	}

	public byte[] data
	{
		get
		{
			return _data;
		}
	}

	public MNT_RoomTemplate(int roomSize, byte[] roomData)
	{
		_size = roomSize;
		_data = roomData;
	}
}
