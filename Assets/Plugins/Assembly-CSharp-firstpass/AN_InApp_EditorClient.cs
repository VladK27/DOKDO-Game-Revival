using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class AN_InApp_EditorClient : MonoBehaviour, AN_InAppClient
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BillingResult> ActionProductPurchased__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BillingResult> ActionProductConsumed__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BillingResult> ActionBillingSetupFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<BillingResult> ActionRetrieveProducsFinished__BackingField = delegate
	{
	};

	private string _processedSKU;

	private AndroidInventory _inventory;

	private bool _IsConnectingToServiceInProcess;

	private bool _IsProductRetrievingInProcess;

	private bool _IsConnected;

	private bool _IsInventoryLoaded;

	private float _RequestsSuccessRate = 100f;

	private string _CurrentSKU = string.Empty;

	public AndroidInventory Inventory
	{
		get
		{
			return _inventory;
		}
	}

	public bool IsConnectingToServiceInProcess
	{
		get
		{
			return _IsConnectingToServiceInProcess;
		}
	}

	public bool IsProductRetrievingInProcess
	{
		get
		{
			return _IsProductRetrievingInProcess;
		}
	}

	public bool IsConnected
	{
		get
		{
			return _IsConnected;
		}
	}

	public bool IsInventoryLoaded
	{
		get
		{
			return _IsInventoryLoaded;
		}
	}

	public event Action<BillingResult> ActionProductPurchased
	{
		add
		{
			Action<BillingResult> action = ActionProductPurchased__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductPurchased__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionProductPurchased__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductPurchased__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BillingResult> ActionProductConsumed
	{
		add
		{
			Action<BillingResult> action = ActionProductConsumed__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductConsumed__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionProductConsumed__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionProductConsumed__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BillingResult> ActionBillingSetupFinished
	{
		add
		{
			Action<BillingResult> action = ActionBillingSetupFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionBillingSetupFinished__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionBillingSetupFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionBillingSetupFinished__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<BillingResult> ActionRetrieveProducsFinished
	{
		add
		{
			Action<BillingResult> action = ActionRetrieveProducsFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRetrieveProducsFinished__BackingField, (Action<BillingResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<BillingResult> action = ActionRetrieveProducsFinished__BackingField;
			Action<BillingResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ActionRetrieveProducsFinished__BackingField, (Action<BillingResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		_inventory = new AndroidInventory();
		_RequestsSuccessRate = AndroidNativeSettings.Instance.InApps_EditorFillRate;
	}

	public void AddProduct(string SKU)
	{
		GoogleProductTemplate googleProductTemplate = new GoogleProductTemplate();
		googleProductTemplate.SKU = SKU;
		GoogleProductTemplate template = googleProductTemplate;
		AddProduct(template);
	}

	public void AddProduct(GoogleProductTemplate template)
	{
		bool flag = false;
		int index = 0;
		foreach (GoogleProductTemplate product in _inventory.Products)
		{
			if (product.SKU.Equals(template.SKU))
			{
				flag = true;
				index = _inventory.Products.IndexOf(product);
				break;
			}
		}
		if (flag)
		{
			_inventory.Products[index] = template;
		}
		else
		{
			_inventory.Products.Add(template);
		}
	}

	public void Connect()
	{
		if (AndroidNativeSettings.Instance.IsBase64KeyWasReplaced)
		{
			_IsConnectingToServiceInProcess = true;
			Connect(AndroidNativeSettings.Instance.base64EncodedPublicKey);
		}
		else
		{
			SA_EditorNotifications.ShowNotification("Billing Connection Failed", "Wrong Public Key", SA_EditorNotificationType.Error);
			BillingResult obj = new BillingResult(-1007, "Connection Failed");
			ActionBillingSetupFinished__BackingField(obj);
		}
	}

	public void Connect(string base64EncodedPublicKey)
	{
		foreach (GoogleProductTemplate inAppProduct in AndroidNativeSettings.Instance.InAppProducts)
		{
			AddProduct(inAppProduct);
		}
		Invoke("GenerateConnectionResult", UnityEngine.Random.Range(0.5f, 3f));
	}

	private void GenerateConnectionResult()
	{
		BillingResult obj;
		if (SA_EditorTesting.HasFill(_RequestsSuccessRate))
		{
			_IsConnected = true;
			obj = new BillingResult(0, "Connection Successful");
			SA_EditorNotifications.ShowNotification("Billing Connected", "Connection successful", SA_EditorNotificationType.Message);
		}
		else
		{
			obj = new BillingResult(-1008, "Connection Failed");
			SA_EditorNotifications.ShowNotification("Billing Connection Failed", "Connection Failed", SA_EditorNotificationType.Error);
		}
		_IsConnectingToServiceInProcess = false;
		ActionBillingSetupFinished__BackingField(obj);
	}

	public void RetrieveProducDetails()
	{
		_IsProductRetrievingInProcess = true;
		Invoke("OnQueryInventoryFinishedCallBack", UnityEngine.Random.Range(0.5f, 3f));
	}

	public void OnQueryInventoryFinishedCallBack()
	{
		BillingResult obj = new BillingResult(0, "BILLING_RESPONSE_RESULT_OK");
		_IsInventoryLoaded = true;
		_IsProductRetrievingInProcess = false;
		ActionRetrieveProducsFinished__BackingField(obj);
	}

	public void Purchase(string SKU)
	{
		Purchase(SKU, string.Empty);
	}

	public void Purchase(string SKU, string DeveloperPayload)
	{
		_processedSKU = SKU;
		GoogleProductTemplate productDetails = _inventory.GetProductDetails(SKU);
		string title = SKU;
		string describtion = "???";
		string price = "?.??$";
		_CurrentSKU = SKU;
		if (productDetails != null)
		{
			title = productDetails.Title;
			describtion = productDetails.Description;
			price = productDetails.LocalizedPrice;
		}
		SA_EditorInApps.ShowInAppPopup(title, describtion, price, OnPurchaseComplete);
	}

	private void OnPurchaseComplete(bool IsSucceeded)
	{
		GooglePurchaseTemplate googlePurchaseTemplate = null;
		BillingResult obj;
		if (IsSucceeded)
		{
			googlePurchaseTemplate = new GooglePurchaseTemplate();
			googlePurchaseTemplate.SKU = _CurrentSKU;
			obj = new BillingResult(0, "BILLING_RESPONSE_RESULT_OK", googlePurchaseTemplate);
		}
		else
		{
			obj = new BillingResult(-1005, "BILLINGHELPERR_USER_CANCELLED");
		}
		ActionProductPurchased__BackingField(obj);
	}

	public void Subscribe(string SKU)
	{
		Subscribe(SKU, string.Empty);
	}

	public void Subscribe(string SKU, string DeveloperPayload)
	{
		_processedSKU = SKU;
		AN_SoomlaGrow.PurchaseStarted(SKU);
		AN_BillingProxy.Subscribe(SKU, DeveloperPayload);
	}

	public void Consume(string SKU)
	{
		GooglePurchaseTemplate googlePurchaseTemplate = null;
		googlePurchaseTemplate = new GooglePurchaseTemplate();
		googlePurchaseTemplate.SKU = SKU;
		BillingResult obj = new BillingResult(0, "BILLING_RESPONSE_RESULT_OK", googlePurchaseTemplate);
		ActionProductConsumed__BackingField(obj);
	}

	public void LoadStore()
	{
		Connect();
	}

	public void LoadStore(string base64EncodedPublicKey)
	{
		Connect(base64EncodedPublicKey);
	}

	public void OnPurchaseFinishedCallback(string data)
	{
		UnityEngine.Debug.Log(data);
		string[] array = data.Split("|"[0]);
		int num = Convert.ToInt32(array[0]);
		GooglePurchaseTemplate googlePurchaseTemplate = new GooglePurchaseTemplate();
		if (num == 0)
		{
			googlePurchaseTemplate.SKU = array[2];
			googlePurchaseTemplate.PackageName = array[3];
			googlePurchaseTemplate.DeveloperPayload = array[4];
			googlePurchaseTemplate.OrderId = array[5];
			googlePurchaseTemplate.SetState(array[6]);
			googlePurchaseTemplate.Token = array[7];
			googlePurchaseTemplate.Signature = array[8];
			googlePurchaseTemplate.Time = Convert.ToInt64(array[9]);
			googlePurchaseTemplate.OriginalJson = array[10];
			if (_inventory != null)
			{
				_inventory.addPurchase(googlePurchaseTemplate);
			}
		}
		else
		{
			googlePurchaseTemplate.SKU = _processedSKU;
		}
		switch (num)
		{
		case 0:
		{
			GoogleProductTemplate productDetails = Inventory.GetProductDetails(googlePurchaseTemplate.SKU);
			if (productDetails != null)
			{
				AN_SoomlaGrow.PurchaseFinished(productDetails.SKU, productDetails.PriceAmountMicros, productDetails.PriceCurrencyCode);
			}
			else
			{
				AN_SoomlaGrow.PurchaseFinished(googlePurchaseTemplate.SKU, 0L, "USD");
			}
			break;
		}
		case -1005:
			AN_SoomlaGrow.PurchaseCanceled(googlePurchaseTemplate.SKU);
			break;
		default:
			AN_SoomlaGrow.PurchaseError();
			break;
		}
		BillingResult obj = new BillingResult(num, array[1], googlePurchaseTemplate);
		ActionProductPurchased__BackingField(obj);
	}

	public void OnConsumeFinishedCallBack(string data)
	{
		string[] array = data.Split("|"[0]);
		int num = Convert.ToInt32(array[0]);
		GooglePurchaseTemplate googlePurchaseTemplate = null;
		if (num == 0)
		{
			googlePurchaseTemplate = new GooglePurchaseTemplate();
			googlePurchaseTemplate.SKU = array[2];
			googlePurchaseTemplate.PackageName = array[3];
			googlePurchaseTemplate.DeveloperPayload = array[4];
			googlePurchaseTemplate.OrderId = array[5];
			googlePurchaseTemplate.SetState(array[6]);
			googlePurchaseTemplate.Token = array[7];
			googlePurchaseTemplate.Signature = array[8];
			googlePurchaseTemplate.Time = Convert.ToInt64(array[9]);
			googlePurchaseTemplate.OriginalJson = array[10];
			if (_inventory != null)
			{
				_inventory.removePurchase(googlePurchaseTemplate);
			}
		}
		BillingResult obj = new BillingResult(num, array[1], googlePurchaseTemplate);
		ActionProductConsumed__BackingField(obj);
	}
}
