public class UM_Android_InAppClient : UM_BaseInAppClient, UM_InAppClient
{
	public void Connect()
	{
		AndroidInAppPurchaseManager.Client.Connect();
		AndroidInAppPurchaseManager.ActionBillingSetupFinished += OnBillingConnected;
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished += OnRetrieveProductsFinised;
		AndroidInAppPurchaseManager.ActionProductPurchased += OnProductPurchased;
	}

	public override void Purchase(UM_InAppProduct product)
	{
		AndroidInAppPurchaseManager.Client.Purchase(product.AndroidId);
	}

	public override void Subscribe(UM_InAppProduct product)
	{
		AndroidInAppPurchaseManager.Client.Subscribe(product.AndroidId);
	}

	public override void Consume(UM_InAppProduct product)
	{
		AndroidInAppPurchaseManager.Client.Consume(product.AndroidId);
	}

	public override void FinishTransaction(UM_InAppProduct product)
	{
	}

	public override bool IsProductPurchased(UM_InAppProduct product)
	{
		if (product == null)
		{
			return false;
		}
		if (AndroidInAppPurchaseManager.Client.Inventory == null)
		{
			return false;
		}
		return AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(product.AndroidId);
	}

	public void RestorePurchases()
	{
	}

	private void OnProductPurchased(BillingResult result)
	{
		UM_InAppProduct productByAndroidId = UltimateMobileSettings.Instance.GetProductByAndroidId(result.Purchase.SKU);
		if (productByAndroidId != null)
		{
			if (UltimateMobileSettings.Instance.TransactionsHandlingMode == UM_TransactionsHandlingMode.Automatic && productByAndroidId.IsConsumable && result.IsSuccess)
			{
				AndroidInAppPurchaseManager.Client.Consume(result.Purchase.SKU);
			}
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.isSuccess = result.IsSuccess;
			uM_PurchaseResult.product = productByAndroidId;
			uM_PurchaseResult.SetResponceCode(result.Response);
			uM_PurchaseResult.Google_PurchaseInfo = result.Purchase;
			SendPurchaseFinishedEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void OnBillingConnected(BillingResult result)
	{
		if (result.IsSuccess)
		{
			AndroidInAppPurchaseManager.ActionBillingSetupFinished -= OnBillingConnected;
			AndroidInAppPurchaseManager.Client.RetrieveProducDetails();
			return;
		}
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.isSuccess = false;
		uM_BillingConnectionResult.message = result.Message;
		SendServiceConnectedEvent(uM_BillingConnectionResult);
	}

	private void OnRetrieveProductsFinised(BillingResult result)
	{
		AndroidInAppPurchaseManager.ActionRetrieveProducsFinished -= OnRetrieveProductsFinised;
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.message = result.Message;
		uM_BillingConnectionResult.isSuccess = result.IsSuccess;
		_IsConnected = uM_BillingConnectionResult.isSuccess;
		if (uM_BillingConnectionResult.isSuccess)
		{
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				GoogleProductTemplate productDetails = AndroidInAppPurchaseManager.Client.Inventory.GetProductDetails(inAppProduct.AndroidId);
				if (productDetails != null)
				{
					inAppProduct.SetTemplate(productDetails);
					if (inAppProduct.IsConsumable && AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(inAppProduct.AndroidId))
					{
						AndroidInAppPurchaseManager.Client.Consume(inAppProduct.AndroidId);
					}
					if (!inAppProduct.IsConsumable && AndroidInAppPurchaseManager.Client.Inventory.IsProductPurchased(inAppProduct.AndroidId))
					{
						UM_InAppPurchaseManager.SaveNonConsumableItemPurchaseInfo(inAppProduct);
					}
				}
			}
		}
		SendServiceConnectedEvent(uM_BillingConnectionResult);
	}
}
