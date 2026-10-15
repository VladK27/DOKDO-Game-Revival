using System;

public class GooglePurchaseTemplate
{
	public string OrderId;

	public string PackageName;

	public string SKU;

	public string DeveloperPayload;

	public string Signature;

	public string Token;

	public long Time;

	public string OriginalJson;

	public GooglePurchaseState State;

	public void SetState(string code)
	{
		switch (Convert.ToInt32(code))
		{
		case 0:
			State = GooglePurchaseState.PURCHASED;
			break;
		case 1:
			State = GooglePurchaseState.CANCELED;
			break;
		case 2:
			State = GooglePurchaseState.REFUNDED;
			break;
		}
	}
}
