using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

public class AddressBookExample : MonoBehaviour
{
	public SA_Label _name;

	public SA_Label _phone;

	public SA_Label _note;

	public SA_Label _email;

	public SA_Label _chat;

	public SA_Label _address;

	private List<AndroidContactInfo> all_contacts = new List<AndroidContactInfo>();

	private void LoadAdressBook()
	{
		Singleton<AddressBookController>.Instance.LoadContacts();
		AddressBookController.OnContactsLoadedAction += OnContactsLoaded;
	}

	private void OnContactsLoaded()
	{
		AddressBookController.OnContactsLoadedAction -= OnContactsLoaded;
		all_contacts = Singleton<AddressBookController>.Instance.contacts;
		AN_PoupsProxy.showMessage("On Contacts Loaded", "Andress book has " + all_contacts.Count + " Contacts");
		using (List<AndroidContactInfo>.Enumerator enumerator = all_contacts.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				AndroidContactInfo current = enumerator.Current;
				_name.text = "Name " + current.name;
				_phone.text = "Phone " + current.phone;
				_note.text = "Note " + current.note;
				_email.text = "Email " + current.email.email;
				_chat.text = "Chat.name " + current.chat.name;
				_address.text = "Country " + current.address.country;
			}
		}
	}
}
