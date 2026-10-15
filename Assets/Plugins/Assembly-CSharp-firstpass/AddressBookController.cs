using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using SA.Common.Pattern;
using UnityEngine;

public class AddressBookController : Singleton<AddressBookController>
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action OnContactsLoadedAction__BackingField = () =>
	{
	};

	private const string DATA_SPLITTER_1 = "&#&";

	private const string DATA_SPLITTER_2 = "#&#";

	private const int byte_limit = 256;

	private static bool _isLoaded = false;

	private List<AndroidContactInfo> _contacts = new List<AndroidContactInfo>();

	public List<AndroidContactInfo> contacts
	{
		get
		{
			return _contacts;
		}
	}

	public static bool isLoaded
	{
		get
		{
			return _isLoaded;
		}
	}

	public static event Action OnContactsLoadedAction
	{
		add
		{
			Action action = OnContactsLoadedAction__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnContactsLoadedAction__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OnContactsLoadedAction__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnContactsLoadedAction__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadContacts()
	{
		AndroidNative.LoadContacts();
	}

	private void OnContactsLoaded(string data)
	{
		if (data.Equals(string.Empty))
		{
			UnityEngine.Debug.Log("AddressBookController OnContactsLoaded, no data avaiable");
			return;
		}
		parseContacts(data);
		_isLoaded = true;
		UnityEngine.Debug.Log("OnContactsLoaded, total:" + _contacts.Count);
		OnContactsLoadedAction__BackingField();
	}

	private void parseContacts(string data)
	{
		string[] array = data.Split("|"[0]);
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (!isValid(text))
			{
				continue;
			}
			AndroidContactInfo androidContactInfo = new AndroidContactInfo();
			if (text.Contains("&#&"))
			{
				string[] array3 = Regex.Split(text, "&#&");
				androidContactInfo.name = trimString(array3[0], 5);
				androidContactInfo.phone = trimString(array3[1], 6);
				androidContactInfo.email = new AndroidABEmail();
				if (array3[2].Contains("#&#"))
				{
					string[] array4 = Regex.Split(array3[2], "#&#");
					androidContactInfo.email.email = trimString(array4[0], 6);
					androidContactInfo.email.emailType = array4[1];
				}
				else
				{
					androidContactInfo.email.email = trimString(array3[2], 6);
				}
				androidContactInfo.note = trimString(array3[3], 5);
				androidContactInfo.chat = new AndroidABChat();
				if (array3[4].Contains("#&#"))
				{
					string[] array5 = Regex.Split(array3[4], "#&#");
					androidContactInfo.chat.name = trimString(array5[0], 5);
					androidContactInfo.chat.type = array5[1];
				}
				else
				{
					androidContactInfo.chat.name = trimString(array3[4], 5);
				}
				androidContactInfo.organization = new AndroidABOrganization();
				if (array3[5].Contains("#&#"))
				{
					string[] array6 = Regex.Split(array3[5], "#&#");
					androidContactInfo.organization.name = trimString(array6[0], 13);
					androidContactInfo.organization.title = array6[1];
				}
				else
				{
					androidContactInfo.organization.name = trimString(array3[5], 13);
				}
				string text2 = trimString(array3[6], 6);
				if (havePhoto(text2))
				{
					string[] array7 = text2.Split(","[0]);
					List<byte> list = new List<byte>();
					string[] array8 = array7;
					foreach (string value in array8)
					{
						int num = Convert.ToInt32(value);
						int value2 = ((num >= 0) ? num : (256 + num));
						list.Add(Convert.ToByte(value2));
					}
					byte[] data2 = list.ToArray();
					Texture2D texture2D = new Texture2D(150, 150);
					texture2D.LoadImage(data2);
					androidContactInfo.photo = texture2D;
				}
				else
				{
					androidContactInfo.photo = null;
				}
				androidContactInfo.address = new AndroidABAddress();
				string[] array9 = Regex.Split(array3[7], "#&#");
				androidContactInfo.address.poBox = trimString(array9[0], 8);
				androidContactInfo.address.street = array9[1];
				androidContactInfo.address.city = array9[2];
				androidContactInfo.address.state = array9[3];
				androidContactInfo.address.postalCode = array9[4];
				androidContactInfo.address.country = array9[5];
				androidContactInfo.address.type = array9[6];
			}
			else
			{
				androidContactInfo.name = trimString(text, 5);
			}
			_contacts.Add(androidContactInfo);
		}
	}

	private string trimString(string str, int index)
	{
		return str.Substring(index);
	}

	private bool isValid(string str)
	{
		return str.Contains("Name");
	}

	private bool havePhoto(string str)
	{
		return !str.Equals("-");
	}
}
