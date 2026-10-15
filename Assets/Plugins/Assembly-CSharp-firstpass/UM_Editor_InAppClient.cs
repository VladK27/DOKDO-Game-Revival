using SA.Common.Util;
using UnityEngine;

public class UM_Editor_InAppClient : UM_BaseInAppClient, UM_InAppClient
{
	private float _RequestsSuccessRate = 100f;

	private UM_InAppProduct _CurrentProduct;

	public UM_Editor_InAppClient()
	{
		_RequestsSuccessRate = UltimateMobileSettings.Instance.InApps_EditorFillRate;
	}

	public void Connect()
	{
		General.Invoke(Random.Range(0.5f, 3f), () =>
		{
			bool flag = SA_EditorTesting.HasFill(_RequestsSuccessRate);
			UM_BillingConnectionResult uM_BillingConnectionResult = new UM_BillingConnectionResult();
			if (flag)
			{
				_IsConnected = true;
				uM_BillingConnectionResult.isSuccess = true;
				uM_BillingConnectionResult.message = "Editor Testing Service Connected";
				SA_EditorNotifications.ShowNotification("Billing Connected", "Connection successful", SA_EditorNotificationType.Message);
			}
			else
			{
				uM_BillingConnectionResult.isSuccess = false;
				uM_BillingConnectionResult.message = "Connection failed";
				SA_EditorNotifications.ShowNotification("Billing Connection Failed", "Connection Failed", SA_EditorNotificationType.Error);
			}
			SendServiceConnectedEvent(uM_BillingConnectionResult);
		}, string.Empty);
	}

	public override void Purchase(UM_InAppProduct product)
	{
		_CurrentProduct = product;
		SA_EditorInApps.ShowInAppPopup(product.DisplayName, product.Description, product.LocalizedPrice, OnPurchaseComplete);
	}

	public override void Subscribe(UM_InAppProduct product)
	{
		Purchase(product);
	}

	public override void Consume(UM_InAppProduct product)
	{
	}

	public override void FinishTransaction(UM_InAppProduct product)
	{
	}

	public void RestorePurchases()
	{
		foreach (UM_InAppProduct inAppProduct in UM_InAppPurchaseManager.InAppProducts)
		{
			if (inAppProduct.IsPurchased)
			{
				UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
				uM_PurchaseResult.isSuccess = true;
				uM_PurchaseResult.product = _CurrentProduct;
				SendPurchaseFinishedEvent(uM_PurchaseResult);
			}
		}
		General.Invoke(Random.Range(0.5f, 3f), () =>
		{
			SendRestoreFinishedEvent(new UM_BaseResult
			{
				IsSucceeded = true
			});
		}, string.Empty);
	}

	private void OnPurchaseComplete(bool IsSucceeded)
	{
		UM_PurchaseResult uM_PurchaseResult = new UM_PurchaseResult();
		uM_PurchaseResult.isSuccess = IsSucceeded;
		uM_PurchaseResult.product = _CurrentProduct;
		SendPurchaseFinishedEvent(uM_PurchaseResult);
	}
}
