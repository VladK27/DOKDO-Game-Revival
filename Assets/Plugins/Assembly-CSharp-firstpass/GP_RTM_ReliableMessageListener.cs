using SA.Common.Pattern;

public class GP_RTM_ReliableMessageListener
{
	private byte[] _Data;

	private int _DataTokenId;

	private int _ReliableMessagesCounter;

	public int DataTokenId
	{
		get
		{
			return _DataTokenId;
		}
	}

	public byte[] Data
	{
		get
		{
			return _Data;
		}
	}

	public GP_RTM_ReliableMessageListener(int dataTokenId, byte[] data)
	{
		_DataTokenId = dataTokenId;
	}

	public void ReportSentMessage()
	{
		_ReliableMessagesCounter++;
	}

	public void ReportDeliveredMessage()
	{
		_ReliableMessagesCounter--;
		if (_ReliableMessagesCounter == 0)
		{
			Singleton<GooglePlayRTM>.Instance.ClearReliableMessageListener(_DataTokenId);
		}
	}
}
