using UnityEngine;
using UnityEngine.UI;

public class SendSmsExample : MonoBehaviour
{
	[SerializeField]
	private Text smsBody;

	[SerializeField]
	private Text reciever;

	public void SendSMS()
	{
		string text = smsBody.text;
		string text2 = reciever.text;
		if (text.Length > 0 && text2.Length == 10)
		{
			AndroidSocialGate.SendTextMessage(text, text2);
		}
	}
}
