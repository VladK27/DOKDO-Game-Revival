using System.Collections.Generic;
using UnityEngine;

public class SA_AmazonBillingExample : MonoBehaviour
{
	public DefaultPreviewButton[] buttons;

	private List<string> entitlements;

	private Dictionary<string, AmazonProductTemplate> availableItems;

	private List<string> unavailableSkus;

	private List<SA_AmazonReceipt> listReceipts;

	private SA_AmazonReceipt receipt;

	private bool isInitialized;

	private string SKU_EXAMPLE = "first_item";

	private void Start()
	{
		entitlements = AMN_PlayerData.GetAvailableSKUs();
		DisableButtons();
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetUserDataReceived += OnGetUserDataReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnPurchaseProductReceived += OnPurchaseProductReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetProductDataReceived += OnGetProductDataReceived;
		AMN_Singleton<SA_AmazonBillingManager>.Instance.OnGetPurchaseProductsUpdatesReceived += OnGetPurchaseProductsUpdatesReceived;
	}

	private void OnGUI()
	{
		if (isInitialized)
		{
			EnableButtons();
		}
	}

	private void OnGetUserDataReceived(AMN_GetUserDataResponse result)
	{
		string requestId = result.RequestId;
		string userId = result.UserId;
		string marketplace = result.Marketplace;
		string status = result.Status;
		SA_StatusBar.text = "GetUserData status " + result.Status;
		Debug.Log(requestId + " " + userId + " " + marketplace + " " + status);
	}

	private void OnPurchaseProductReceived(AMN_PurchaseResponse result)
	{
		if (result.isSuccess)
		{
			string requestId = result.RequestId;
			string userId = result.UserId;
			string marketplace = result.Marketplace;
			string receiptId = result.ReceiptId;
			long cancelDate = result.CancelDate;
			long purchaseDatee = result.PurchaseDatee;
			string sku = result.Sku;
			string productType = result.ProductType;
			string status = result.Status;
			SA_StatusBar.text = "PurchaseProduct status " + result.Status;
			Debug.Log(requestId + " " + userId + " " + marketplace + " " + receiptId + " " + cancelDate + " " + purchaseDatee + " " + sku + " " + productType + " " + status);
		}
		else
		{
			string requestId2 = result.RequestId;
			string status2 = result.Status;
			SA_StatusBar.text = "PurchaseProduct status " + result.Status;
			Debug.Log("_status " + status2 + " _requestId " + requestId2);
		}
	}

	private void OnGetProductDataReceived(AMN_GetProductDataResponse result)
	{
		isInitialized = true;
		string requestId = result.RequestId;
		string status = result.Status;
		availableItems = AMN_Singleton<SA_AmazonBillingManager>.Instance.availableItems;
		unavailableSkus = AMN_Singleton<SA_AmazonBillingManager>.Instance.unavailableSkus;
		SA_StatusBar.text = "OnGetProductData status " + result.Status;
		Debug.Log(string.Concat(availableItems, " ", status, " ", requestId, " ", unavailableSkus));
	}

	private void OnGetPurchaseProductsUpdatesReceived(AMN_GetPurchaseProductsUpdateResponse result)
	{
		string requestId = result.RequestId;
		string userId = result.UserId;
		string marketplace = result.Marketplace;
		string status = result.Status;
		bool hasMore = result.HasMore;
		listReceipts = AMN_Singleton<SA_AmazonBillingManager>.Instance.listReceipts;
		foreach (SA_AmazonReceipt listReceipt in listReceipts)
		{
			string sku = listReceipt.Sku;
			string productType = listReceipt.ProductType;
			string receiptId = listReceipt.ReceiptId;
			long purchaseDate = listReceipt.PurchaseDate;
			long cancelDate = listReceipt.CancelDate;
			Debug.Log(sku + " " + productType + " " + receiptId + " " + purchaseDate + " " + cancelDate);
		}
		Debug.Log(requestId + " " + userId + " " + marketplace + " " + status + " " + hasMore + " " + listReceipts);
	}

	private void InitializeAmazonBilling()
	{
		SA_StatusBar.text = "Initializing Amazon Billing";
		AMN_Singleton<SA_AmazonBillingManager>.Instance.Initialize();
	}

	private void DisableButtons()
	{
		DefaultPreviewButton[] array = buttons;
		foreach (DefaultPreviewButton defaultPreviewButton in array)
		{
			defaultPreviewButton.DisabledButton();
		}
	}

	private void EnableButtons()
	{
		DefaultPreviewButton[] array = buttons;
		foreach (DefaultPreviewButton defaultPreviewButton in array)
		{
			defaultPreviewButton.EnabledButton();
		}
	}

	private void Purchase()
	{
		if (entitlements.Contains(SKU_EXAMPLE))
		{
			Debug.Log("Already buyed!");
		}
		else
		{
			AMN_Singleton<SA_AmazonBillingManager>.Instance.Purchase(SKU_EXAMPLE);
		}
	}

	private void GetUserData()
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.GetUserData();
	}

	private void GetProductUpdates()
	{
		AMN_Singleton<SA_AmazonBillingManager>.Instance.GetProductUpdates();
	}

	private void AddEntitlement(string SKU)
	{
		if (!entitlements.Contains(SKU))
		{
			entitlements.Add(SKU);
			AMN_PlayerData.AddNewSKU(SKU);
		}
	}
}
