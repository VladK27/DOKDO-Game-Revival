using System;
using System.Text;

public class LocalNotificationTemplate
{
	private int _id;

	private string _title;

	private string _message;

	private DateTime _fireDate;

	private const string DATA_SPLITTER = "|||";

	public int id
	{
		get
		{
			return _id;
		}
	}

	public string title
	{
		get
		{
			return _title;
		}
	}

	public string message
	{
		get
		{
			return _message;
		}
	}

	public DateTime fireDate
	{
		get
		{
			return _fireDate;
		}
	}

	public string SerializedString
	{
		get
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(id + "|||" + title + "|||" + message + "|||" + fireDate.Ticks));
		}
	}

	public bool IsFired
	{
		get
		{
			if (DateTime.Now.Ticks > fireDate.Ticks)
			{
				return true;
			}
			return false;
		}
	}

	public LocalNotificationTemplate(string data)
	{
		string[] array = data.Split(new string[1] { "|||" }, StringSplitOptions.None);
		_id = Convert.ToInt32(array[0]);
		_title = array[1];
		_message = array[2];
		_fireDate = new DateTime(Convert.ToInt64(array[3]));
	}

	public LocalNotificationTemplate(int nId, string ttl, string msg, DateTime date)
	{
		_id = nId;
		_title = ttl;
		_message = msg;
		_fireDate = date;
	}
}
