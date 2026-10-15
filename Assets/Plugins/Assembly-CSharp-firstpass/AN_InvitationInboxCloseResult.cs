using System;
using UnityEngine;

public class AN_InvitationInboxCloseResult : MonoBehaviour
{
	private AdroidActivityResultCodes _resultCode;

	public AdroidActivityResultCodes ResultCode
	{
		get
		{
			return _resultCode;
		}
	}

	public AN_InvitationInboxCloseResult(string result)
	{
		_resultCode = (AdroidActivityResultCodes)Convert.ToInt32(result);
	}
}
