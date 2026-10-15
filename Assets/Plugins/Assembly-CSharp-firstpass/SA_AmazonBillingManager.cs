using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class SA_AmazonBillingManager : AMN_Singleton<SA_AmazonBillingManager>
{
	public enum status
	{
		SUCCESSFUL = 0,
		FAILED = 1
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_GetUserDataResponse> OnGetUserDataReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_PurchaseResponse> OnPurchaseProductReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_GetProductDataResponse> OnGetProductDataReceived__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AMN_GetPurchaseProductsUpdateResponse> OnGetPurchaseProductsUpdatesReceived__BackingField = delegate
	{
	};

	public string currentSKU = string.Empty;

	private bool _isInitialized;

	public Dictionary<string, AmazonProductTemplate> availableItems;

	public List<string> unavailableSkus;

	public List<SA_AmazonReceipt> listReceipts;

	public bool IsInitialized
	{
		get
		{
			return _isInitialized;
		}
	}

	public event Action<AMN_GetUserDataResponse> OnGetUserDataReceived
	{
		add
		{
			Action<AMN_GetUserDataResponse> action = OnGetUserDataReceived__BackingField;
			Action<AMN_GetUserDataResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetUserDataReceived__BackingField, (Action<AMN_GetUserDataResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_GetUserDataResponse> action = OnGetUserDataReceived__BackingField;
			Action<AMN_GetUserDataResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetUserDataReceived__BackingField, (Action<AMN_GetUserDataResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_PurchaseResponse> OnPurchaseProductReceived
	{
		add
		{
			Action<AMN_PurchaseResponse> action = OnPurchaseProductReceived__BackingField;
			Action<AMN_PurchaseResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseProductReceived__BackingField, (Action<AMN_PurchaseResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_PurchaseResponse> action = OnPurchaseProductReceived__BackingField;
			Action<AMN_PurchaseResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseProductReceived__BackingField, (Action<AMN_PurchaseResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_GetProductDataResponse> OnGetProductDataReceived
	{
		add
		{
			Action<AMN_GetProductDataResponse> action = OnGetProductDataReceived__BackingField;
			Action<AMN_GetProductDataResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetProductDataReceived__BackingField, (Action<AMN_GetProductDataResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_GetProductDataResponse> action = OnGetProductDataReceived__BackingField;
			Action<AMN_GetProductDataResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetProductDataReceived__BackingField, (Action<AMN_GetProductDataResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AMN_GetPurchaseProductsUpdateResponse> OnGetPurchaseProductsUpdatesReceived
	{
		add
		{
			Action<AMN_GetPurchaseProductsUpdateResponse> action = OnGetPurchaseProductsUpdatesReceived__BackingField;
			Action<AMN_GetPurchaseProductsUpdateResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetPurchaseProductsUpdatesReceived__BackingField, (Action<AMN_GetPurchaseProductsUpdateResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AMN_GetPurchaseProductsUpdateResponse> action = OnGetPurchaseProductsUpdatesReceived__BackingField;
			Action<AMN_GetPurchaseProductsUpdateResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnGetPurchaseProductsUpdatesReceived__BackingField, (Action<AMN_GetPurchaseProductsUpdateResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
	}

	public void Initialize()
	{
		Initialize(AmazonNativeSettings.Instance.InAppProducts);
	}

	public void Initialize(List<AmazonProductTemplate> product_ids)
	{
		if (!_isInitialized)
		{
			Init(product_ids);
		}
	}

	public void AddProduct(string sku)
	{
		AmazonProductTemplate amazonProductTemplate = new AmazonProductTemplate();
		amazonProductTemplate.Sku = sku;
		AmazonProductTemplate amazonProductTemplate2 = amazonProductTemplate;
		int num = IsExistsInSettings(amazonProductTemplate2);
		if (num != -1)
		{
			AmazonNativeSettings.Instance.InAppProducts.RemoveAt(num);
		}
		AmazonNativeSettings.Instance.InAppProducts.Add(amazonProductTemplate2);
		UnityEngine.Debug.Log("AddProduct(string sku)" + sku);
	}

	public void GetUserData()
	{
	}

	public void Purchase(string SKU)
	{
	}

	public void GetProductUpdates()
	{
	}

	private void Init(List<AmazonProductTemplate> product_ids)
	{
	}

	private void SubscribeToEvents()
	{
	}

	private int IsExistsInSettings(AmazonProductTemplate product)
	{
		foreach (AmazonProductTemplate inAppProduct in AmazonNativeSettings.Instance.InAppProducts)
		{
			if (inAppProduct.Sku.Equals(product.Sku))
			{
				return AmazonNativeSettings.Instance.InAppProducts.IndexOf(inAppProduct);
			}
		}
		return -1;
	}
}
