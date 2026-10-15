using UnityEngine;

public class WPM_MarketExample : WPNFeaturePreview
{
	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(10f, 200f, Screen.width, Screen.height), WPN_BillingManagerExample._status);
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Market Example", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Init"))
		{
			WPN_BillingManagerExample.Init();
		}
		if (WPN_BillingManagerExample.IsInited)
		{
			StartX += XButtonStep;
			if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Consumable"))
			{
				WPN_BillingManagerExample.Purchase("item1");
			}
			StartX += XButtonStep;
			if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Buy Durable"))
			{
				WPN_BillingManagerExample.Purchase("item2");
			}
		}
	}
}
