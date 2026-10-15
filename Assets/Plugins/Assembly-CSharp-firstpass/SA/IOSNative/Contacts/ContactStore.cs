using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.Contacts
{
	public class ContactStore : Singleton<ContactStore>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ContactsResult> ContactsLoadResult__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<ContactsResult> ContactsPickResult__BackingField = delegate
		{
		};

		private event Action<ContactsResult> ContactsLoadResult
		{
			add
			{
				Action<ContactsResult> action = ContactsLoadResult__BackingField;
				Action<ContactsResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ContactsLoadResult__BackingField, (Action<ContactsResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<ContactsResult> action = ContactsLoadResult__BackingField;
				Action<ContactsResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ContactsLoadResult__BackingField, (Action<ContactsResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private event Action<ContactsResult> ContactsPickResult
		{
			add
			{
				Action<ContactsResult> action = ContactsPickResult__BackingField;
				Action<ContactsResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ContactsPickResult__BackingField, (Action<ContactsResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<ContactsResult> action = ContactsPickResult__BackingField;
				Action<ContactsResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref ContactsPickResult__BackingField, (Action<ContactsResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void ShowContactsPickerUI(Action<ContactsResult> callback)
		{
			ContactsPickResult += callback;
		}

		public void RetrievePhoneContacts(Action<ContactsResult> callback)
		{
			ContactsLoadResult += callback;
		}

		private Contact ParseContactData(string data)
		{
			string[] array = data.Split('|');
			Contact contact = new Contact();
			contact.GivenName = array[0];
			contact.FamilyName = array[1];
			string[] collection = Converter.ParseArray(array[2]);
			contact.Emails.AddRange(collection);
			string[] array2 = Converter.ParseArray(array[3]);
			string[] array3 = Converter.ParseArray(array[4]);
			for (int i = 0; i < array2.Length; i++)
			{
				PhoneNumber phoneNumber = new PhoneNumber();
				phoneNumber.CountryCode = array2[i];
				phoneNumber.Digits = array3[i];
				contact.PhoneNumbers.Add(phoneNumber);
			}
			return contact;
		}

		private List<Contact> ParseContactArray(string data)
		{
			string[] array = data.Split(new string[1] { "|%|" }, StringSplitOptions.None);
			List<Contact> list = new List<Contact>();
			for (int i = 0; i < array.Length && !(array[i] == "endofline"); i++)
			{
				Contact item = ParseContactData(array[i]);
				list.Add(item);
			}
			return list;
		}

		private void OnContactPickerDidCancel(string errorData)
		{
			Error error = new Error(0, "User Canceled");
			ContactsResult obj = new ContactsResult(error);
			ContactsPickResult__BackingField(obj);
			ContactsPickResult__BackingField = delegate
			{
			};
		}

		private void OnPickerDidSelectContacts(string data)
		{
			ISN_Logger.Log("[ContactStore] OnPickerDidSelectContacts");
			List<Contact> list = ParseContactArray(data);
			ISN_Logger.Log("[ContactStore] Picked " + list.Count + " contacts");
			ContactsResult obj = new ContactsResult(list);
			ContactsPickResult__BackingField(obj);
			ContactsPickResult__BackingField = delegate
			{
			};
		}

		private void OnContactsRetrieveFailed(string errorData)
		{
			ISN_Logger.Log("[ContactStore] OnContactsRetrieveFailed");
			Error error = new Error(errorData);
			ContactsResult obj = new ContactsResult(error);
			ContactsLoadResult__BackingField(obj);
			ContactsLoadResult__BackingField = delegate
			{
			};
		}

		private void OnContactsRetrieveFinished(string data)
		{
			ISN_Logger.Log("[ContactStore] OnContactsRetrieveFinished");
			List<Contact> list = ParseContactArray(data);
			ISN_Logger.Log("[ContactStore] Loaded " + list.Count + " contacts");
			ContactsResult obj = new ContactsResult(list);
			ContactsLoadResult__BackingField(obj);
			ContactsLoadResult__BackingField = delegate
			{
			};
		}
	}
}
