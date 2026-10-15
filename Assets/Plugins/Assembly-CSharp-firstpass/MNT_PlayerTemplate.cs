using UnityEngine;

public class MNT_PlayerTemplate
{
	private string _id;

	private string _name;

	private byte[] _info;

	private bool _IsServer;

	private string _deviceName;

	private string _macAddress;

	private string _externalIP;

	private int _externalPort;

	private string _guid;

	private string _ipAddress;

	private int _port;

	public string id
	{
		get
		{
			return _id;
		}
	}

	public string name
	{
		get
		{
			return _name;
		}
	}

	public byte[] info
	{
		get
		{
			return _info;
		}
	}

	public bool IsServer
	{
		get
		{
			return _IsServer;
		}
	}

	public string deviceName
	{
		get
		{
			return _deviceName;
		}
	}

	public string macAddress
	{
		get
		{
			return _macAddress;
		}
	}

	public string externalIP
	{
		get
		{
			return _externalIP;
		}
	}

	public int externalPort
	{
		get
		{
			return _externalPort;
		}
	}

	public string guid
	{
		get
		{
			return _guid;
		}
	}

	public string ipAddress
	{
		get
		{
			return _ipAddress;
		}
	}

	public int port
	{
		get
		{
			return _port;
		}
	}

	public MNT_PlayerTemplate(string pId, string pName, byte[] pInfo)
	{
		_id = pId;
		_name = pName;
		_info = pInfo;
	}

	public MNT_PlayerTemplate(MNT_PlayerTemplate player)
	{
		_id = player.id;
		_deviceName = player.deviceName;
		_macAddress = player.macAddress;
		_externalIP = player.externalIP;
		_externalPort = player.externalPort;
		_guid = player.guid;
		_ipAddress = player.ipAddress;
		_port = player.port;
	}

	public MNT_PlayerTemplate(NetworkPlayer player)
	{
		_externalIP = player.externalIP;
		_externalPort = player.externalPort;
		_guid = player.guid;
		_ipAddress = player.ipAddress;
		_port = player.port;
		_id = player.ipAddress;
	}

	public MNT_PlayerTemplate(MNT_BluetoothDeviceTemplate device)
	{
		_deviceName = device.Name;
		_macAddress = device.Address;
		_id = device.Address;
	}

	public void SetInfo(string playerName, byte[] PlayerInfo, bool IsServerPlayer = false)
	{
		_name = playerName;
		_info = PlayerInfo;
		_IsServer = IsServerPlayer;
	}
}
