using UnityEngine;
using UnityEngine.UI;

public class BillingTab : FeatureTab
{
	[SerializeField]
	private Button initButton;

	[SerializeField]
	private Button[] initBoundButtons;

	public void Init()
	{
		GPaymnetManagerExample.init();
	}

	private void FixedUpdate()
	{
		if (GPaymnetManagerExample.isInited)
		{
			initButton.interactable = false;
			Button[] array = initBoundButtons;
			foreach (Button button in array)
			{
				button.interactable = true;
			}
		}
		else
		{
			initButton.interactable = true;
			Button[] array2 = initBoundButtons;
			foreach (Button button2 in array2)
			{
				button2.interactable = false;
			}
		}
	}

	public void SuccsesPurchase()
	{
		if (GPaymnetManagerExample.isInited)
		{
			AndroidInAppPurchaseManager.Client.Purchase("android.test.Purchased");
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}

	public void FailPurchase()
	{
		if (GPaymnetManagerExample.isInited)
		{
			AndroidInAppPurchaseManager.Client.Purchase("android.test.item_unavailable");
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}

	public void ConsumeProduct()
	{
		if (GPaymnetManagerExample.isInited)
		{
			if (AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased("android.test.Purchased"))
			{
				GPaymnetManagerExample.consume("android.test.Purchased");
			}
			else
			{
				AndroidMessage.Create("Error", "You do not own product to consume it");
			}
		}
		else
		{
			AndroidMessage.Create("Error", "PaymnetManagerExample not yet inited");
		}
	}
}
