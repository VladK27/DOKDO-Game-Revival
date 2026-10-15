using UnityEngine;

public class UM_Amazon_InAppClient : UM_BaseInAppClient, UM_InAppClient
{
	public void Connect()
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.Initialize();
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetProductDataReceived += HandleAmazonGetProductDataReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetPurchaseProductsUpdatesReceived += HandleAmazonGetPurchaseProductsUpdatesReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetUserDataReceived += HandleAmazonGetUserDataReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnPurchaseProductReceived += HandleAmazonPurchaseProductReceived;
	}

	public override void Purchase(UM_InAppProduct product)
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.Purchase(product.AmazonId);
	}

	public override void Subscribe(UM_InAppProduct product)
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.Purchase(product.AmazonId);
	}

	public override void Consume(UM_InAppProduct product)
	{
	}

	public override void FinishTransaction(UM_InAppProduct product)
	{
	}

	public void RestorePurchases()
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.GetProductUpdates();
	}

	private void HandleAmazonPurchaseProductReceived(AMN_PurchaseResponse response)
	{
		Debug.Log("[Amazon] HandleAmazonPurchaseProductReceived");
		UM_InAppProduct productByAmazonId = UltimateMobileSettings.Instance.GetProductByAmazonId(response.Sku);
		if (productByAmazonId != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.Amazon_PurchaseInfo = response;
			uM_PurchaseResult.product = productByAmazonId;
			uM_PurchaseResult.isSuccess = response.isSuccess;
			SendPurchaseFinishedEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void HandleAmazonGetPurchaseProductsUpdatesReceived(AMN_GetPurchaseProductsUpdateResponse response)
	{
		UM_BaseResult uM_BaseResult = new UM_BaseResult();
		uM_BaseResult.IsSucceeded = response.isSuccess;
		SendRestoreFinishedEvent(uM_BaseResult);
	}

	private void HandleAmazonGetProductDataReceived(AMN_GetProductDataResponse response)
	{
		Debug.Log("[Amazon] HandleAmazonGetProductDataReceived");
		_IsConnected = response.isSuccess;
		if (response.isSuccess)
		{
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				if (AMN_Singleton<SA_AmazonBillingManager>.Instance.availableItems.ContainsKey(inAppProduct.AmazonId))
				{
					inAppProduct.SetTemplate(AMN_Singleton<SA_AmazonBillingManager>.Instance.availableItems[inAppProduct.AmazonId]);
				}
			}
		}
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		uM_BillingConnectionResult.isSuccess = response.isSuccess;
		uM_BillingConnectionResult.message = response.Status;
		SendServiceConnectedEvent(uM_BillingConnectionResult);
	}

	private void HandleAmazonGetUserDataReceived(AMN_GetUserDataResponse response)
	{
		Debug.Log("[Amazon] HandleAmazonGetUserDataReceived");
	}
}
