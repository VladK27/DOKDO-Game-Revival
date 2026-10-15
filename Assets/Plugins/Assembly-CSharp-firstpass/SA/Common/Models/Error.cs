using System;

namespace SA.Common.Models
{
	public class Error
	{
		private int _Code;

		private string _Messgae = string.Empty;

		public int Code
		{
			get
			{
				return _Code;
			}
		}

		public string Message
		{
			get
			{
				return _Messgae;
			}
		}

		public Error()
		{
			_Code = 0;
			_Messgae = "Unknown Error";
		}

		public Error(int code, string message = "")
		{
			_Code = code;
			_Messgae = message;
		}

		public Error(string errorData)
		{
			string[] array = errorData.Split('|');
			_Code = Convert.ToInt32(array[0]);
			_Messgae = array[1];
		}
	}
}
