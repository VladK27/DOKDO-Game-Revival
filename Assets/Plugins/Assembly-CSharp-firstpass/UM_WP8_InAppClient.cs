public class UM_WP8_InAppClient : UM_BaseInAppClient, UM_InAppClient
{
	public void Connect()
	{
		WP8InAppPurchasesManager.Instance.Init();
		WP8InAppPurchasesManager.OnInitComplete += OnInitComplete;
		WP8InAppPurchasesManager.OnPurchaseFinished += OnProductPurchased;
	}

	public override void Purchase(UM_InAppProduct product)
	{
		WP8InAppPurchasesManager.Instance.Purchase(product.WP8Id);
	}

	public override void Subscribe(UM_InAppProduct product)
	{
		WP8InAppPurchasesManager.Instance.Purchase(product.WP8Id);
	}

	public override void Consume(UM_InAppProduct product)
	{
	}

	public override void FinishTransaction(UM_InAppProduct product)
	{
	}

	public void RestorePurchases()
	{
	}

	private void OnInitComplete(WP8_InAppsInitResult result)
	{
		_IsConnected = true;
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.message = "Inited";
		uM_BillingConnectionResult.isSuccess = true;
		foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
		{
			WP8ProductTemplate productById = WP8InAppPurchasesManager.Instance.GetProductById(inAppProduct.WP8Id);
			if (productById != null)
			{
				inAppProduct.SetTemplate(productById);
				if (inAppProduct.WP8Template.IsPurchased && !inAppProduct.IsConsumable)
				{
					UM_InAppPurchaseManager.SaveNonConsumableItemPurchaseInfo(inAppProduct);
				}
			}
		}
		SendServiceConnectedEvent(uM_BillingConnectionResult);
	}

	private void OnProductPurchased(WP8PurchseResponce resp)
	{
		UM_InAppProduct productByWp8Id = UltimateMobileSettings.Instance.GetProductByWp8Id(resp.ProductId);
		if (productByWp8Id != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.product = productByWp8Id;
			uM_PurchaseResult.WP8_PurchaseInfo = resp;
			uM_PurchaseResult.isSuccess = resp.IsSuccses;
			SendPurchaseFinishedEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}
}
