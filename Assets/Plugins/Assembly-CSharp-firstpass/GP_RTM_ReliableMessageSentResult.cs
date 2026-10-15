public class GP_RTM_ReliableMessageSentResult : GP_RTM_Result
{
	private int _MessageTokenId;

	private byte[] _Data;

	public int MessageTokenId
	{
		get
		{
			return _MessageTokenId;
		}
	}

	public byte[] Data
	{
		get
		{
			return _Data;
		}
	}

	public GP_RTM_ReliableMessageSentResult(string status, string roomId, int messageTokedId, byte[] data)
		: base(status, roomId)
	{
		_MessageTokenId = messageTokedId;
		_Data = data;
	}
}
