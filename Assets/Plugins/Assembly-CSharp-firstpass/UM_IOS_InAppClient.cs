using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class UM_IOS_InAppClient : UM_BaseInAppClient, UM_InAppClient
{
	public void Connect()
	{
		Singleton<PaymentManager>.Instance.LoadStore();
		PaymentManager.OnStoreKitInitComplete += IOS_OnStoreKitInitComplete;
		PaymentManager.OnTransactionComplete += IOS_OnTransactionComplete;
		PaymentManager.OnRestoreComplete += IOS_OnRestoreComplete;
	}

	public override void Purchase(UM_InAppProduct product)
	{
		Singleton<PaymentManager>.Instance.BuyProduct(product.IOSId);
	}

	public override void Subscribe(UM_InAppProduct product)
	{
		Singleton<PaymentManager>.Instance.BuyProduct(product.IOSId);
	}

	public override void Consume(UM_InAppProduct product)
	{
	}

	public override void FinishTransaction(UM_InAppProduct product)
	{
		Singleton<PaymentManager>.Instance.FinishTransaction(product.IOSId);
	}

	public void RestorePurchases()
	{
		Singleton<PaymentManager>.Instance.RestorePurchases();
	}

	private void IOS_OnTransactionComplete(PurchaseResult responce)
	{
		UM_InAppProduct productByIOSId = UltimateMobileSettings.Instance.GetProductByIOSId(responce.ProductIdentifier);
		if (productByIOSId != null)
		{
			UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
			uM_PurchaseResult.product = productByIOSId;
			uM_PurchaseResult.IOS_PurchaseInfo = responce;
			switch (uM_PurchaseResult.IOS_PurchaseInfo.State)
			{
			case PurchaseState.Purchased:
			case PurchaseState.Restored:
				uM_PurchaseResult.isSuccess = true;
				break;
			case PurchaseState.Failed:
			case PurchaseState.Deferred:
				uM_PurchaseResult.isSuccess = false;
				break;
			}
			SendPurchaseFinishedEvent(uM_PurchaseResult);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	private void IOS_OnStoreKitInitComplete(Result res)
	{
		UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
		_IsConnected = res.IsSucceeded;
		uM_BillingConnectionResult.isSuccess = res.IsSucceeded;
		if (res.IsSucceeded)
		{
			uM_BillingConnectionResult.message = "Inited";
			foreach (UM_InAppProduct inAppProduct in UltimateMobileSettings.Instance.InAppProducts)
			{
				Product productById = Singleton<PaymentManager>.Instance.GetProductById(inAppProduct.IOSId);
				if (productById != null)
				{
					inAppProduct.SetTemplate(productById);
				}
			}
			SendServiceConnectedEvent(uM_BillingConnectionResult);
		}
		else
		{
			if (res.Error != null)
			{
				uM_BillingConnectionResult.message = res.Error.Message;
			}
			SendServiceConnectedEvent(uM_BillingConnectionResult);
		}
	}

	private void IOS_OnRestoreComplete(RestoreResult res)
	{
		Debug.Log("IOS_OnRestoreComplete");
		UM_BaseResult uM_BaseResult = new UM_BaseResult();
		uM_BaseResult.IsSucceeded = res.IsSucceeded;
		SendRestoreFinishedEvent(uM_BaseResult);
	}
}
