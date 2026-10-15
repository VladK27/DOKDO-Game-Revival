using System.Collections.Generic;
using SA.Common.Models;

namespace SA.IOSNative.Contacts
{
	public class ContactsResult : Result
	{
		private List<Contact> _Contacts = new List<Contact>();

		public List<Contact> Contacts
		{
			get
			{
				return _Contacts;
			}
		}

		public ContactsResult(List<Contact> contacts)
		{
			_Contacts = contacts;
		}

		public ContactsResult(Error error)
			: base(error)
		{
		}
	}
}
