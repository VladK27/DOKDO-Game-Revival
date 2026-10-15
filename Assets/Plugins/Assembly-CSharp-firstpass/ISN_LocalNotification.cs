using System;
using System.Text;
using SA.Common.Pattern;
using SA.Common.Util;

public class ISN_LocalNotification
{
	private int _Id;

	private DateTime _Date;

	private string _Message = string.Empty;

	private bool _UseSound = true;

	private int _Badges;

	private string _Data = string.Empty;

	private string _SoundName = string.Empty;

	private const string DATA_SPLITTER = "|||";

	public int Id
	{
		get
		{
			return _Id;
		}
	}

	public DateTime Date
	{
		get
		{
			return _Date;
		}
	}

	public bool IsFired
	{
		get
		{
			if (DateTime.Now.Ticks > Date.Ticks)
			{
				return true;
			}
			return false;
		}
	}

	public string Message
	{
		get
		{
			return _Message;
		}
	}

	public bool UseSound
	{
		get
		{
			return _UseSound;
		}
	}

	public int Badges
	{
		get
		{
			return _Badges;
		}
	}

	public string Data
	{
		get
		{
			return _Data;
		}
	}

	public string SoundName
	{
		get
		{
			return _SoundName;
		}
	}

	public string SerializedString
	{
		get
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(Id + "|||" + UseSound + "|||" + Badges + "|||" + Data + "|||" + SoundName + "|||" + Date.Ticks));
		}
	}

	public ISN_LocalNotification(DateTime time, string message, bool useSound = true)
	{
		_Id = IdFactory.NextId;
		_Date = time;
		_Message = message;
		_UseSound = useSound;
	}

	public ISN_LocalNotification(string serializaedNotificationData)
	{
		try
		{
			string[] array = serializaedNotificationData.Split(new string[1] { "|||" }, StringSplitOptions.None);
			_Id = Convert.ToInt32(array[0]);
			_UseSound = Convert.ToBoolean(array[1]);
			_Badges = Convert.ToInt32(array[2]);
			_Data = array[3];
			_SoundName = array[4];
			_Date = new DateTime(Convert.ToInt64(array[5]));
		}
		catch (Exception ex)
		{
			ISN_Logger.Log("Failed to deserialize the ISN_LocalNotification object");
			ISN_Logger.Log(ex.Message);
		}
	}

	public void SetId(int id)
	{
		_Id = id;
	}

	public void SetData(string data)
	{
		_Data = data;
	}

	public void SetSoundName(string soundName)
	{
		_SoundName = soundName;
	}

	public void SetBadgesNumber(int badges)
	{
		_Badges = badges;
	}

	public void Schedule()
	{
		Singleton<ISN_LocalNotificationsController>.Instance.ScheduleNotification(this);
	}
}
