using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

public class WP8InAppPurchasesManager
{
	private static WP8InAppPurchasesManager _instance = null;

	private bool _IsInitialized;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<WP8_InAppsInitResult> OnInitComplete__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<WP8PurchseResponce> OnPurchaseFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static Action<WP8ConsumeResponse> OnConsumeFinished__BackingField = delegate
	{
	};

	public const string SPLITTER1 = "|";

	public const string SPLITTER2 = "|%|";

	public const int RESULT_OK = 0;

	public const int RESULT_ERROR = 1;

	private List<WP8ProductTemplate> _products = new List<WP8ProductTemplate>();

	[Obsolete("This property is obsolete. Use 'Instance' property instead")]
	public static WP8InAppPurchasesManager instance
	{
		get
		{
			return Instance;
		}
	}

	public static WP8InAppPurchasesManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new WP8InAppPurchasesManager();
			}
			return _instance;
		}
	}

	[Obsolete("This property is obsolete. Use 'Products' property instead")]
	public List<WP8ProductTemplate> products
	{
		get
		{
			return Products;
		}
	}

	public List<WP8ProductTemplate> Products
	{
		get
		{
			return _products;
		}
	}

	public bool IsInitialized
	{
		get
		{
			return _IsInitialized;
		}
	}

	public static event Action<WP8_InAppsInitResult> OnInitComplete
	{
		add
		{
			Action<WP8_InAppsInitResult> action = OnInitComplete__BackingField;
			Action<WP8_InAppsInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitComplete__BackingField, (Action<WP8_InAppsInitResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<WP8_InAppsInitResult> action = OnInitComplete__BackingField;
			Action<WP8_InAppsInitResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnInitComplete__BackingField, (Action<WP8_InAppsInitResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<WP8PurchseResponce> OnPurchaseFinished
	{
		add
		{
			Action<WP8PurchseResponce> action = OnPurchaseFinished__BackingField;
			Action<WP8PurchseResponce> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseFinished__BackingField, (Action<WP8PurchseResponce>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<WP8PurchseResponce> action = OnPurchaseFinished__BackingField;
			Action<WP8PurchseResponce> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseFinished__BackingField, (Action<WP8PurchseResponce>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public static event Action<WP8ConsumeResponse> OnConsumeFinished
	{
		add
		{
			Action<WP8ConsumeResponse> action = OnConsumeFinished__BackingField;
			Action<WP8ConsumeResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnConsumeFinished__BackingField, (Action<WP8ConsumeResponse>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<WP8ConsumeResponse> action = OnConsumeFinished__BackingField;
			Action<WP8ConsumeResponse> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnConsumeFinished__BackingField, (Action<WP8ConsumeResponse>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	[Obsolete("This method is obsolete. Use 'Init' method instead")]
	public void init()
	{
		Init();
	}

	public void Init()
	{
	}

	[Obsolete("This method is obsolete. Use 'Purchase' method instead")]
	public void purchase(string productId)
	{
		Purchase(productId);
	}

	public void Purchase(string productID)
	{
	}

	public WP8ProductTemplate GetProductById(string id)
	{
		foreach (WP8ProductTemplate product in _products)
		{
			if (product.ProductId.Equals(id))
			{
				return product;
			}
		}
		return null;
	}

	private void InitCallback(string res)
	{
		string[] array = res.Split(new string[1] { "|%|" }, StringSplitOptions.None);
		int statusCode = int.Parse(array[0]);
		WP8_InAppsInitResult wP8_InAppsInitResult = new WP8_InAppsInitResult(statusCode);
		if (wP8_InAppsInitResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new string[1] { "|" }, StringSplitOptions.None);
				WP8ProductTemplate wP8ProductTemplate = new WP8ProductTemplate();
				wP8ProductTemplate.ImgURI = array2[0];
				wP8ProductTemplate.Name = array2[1];
				wP8ProductTemplate.ProductId = array2[2];
				wP8ProductTemplate.Price = array2[3];
				wP8ProductTemplate.Type = (WP8PurchaseProductType)Enum.Parse(typeof(WP8PurchaseProductType), array2[4]);
				wP8ProductTemplate.Description = array2[5];
				wP8ProductTemplate.IsPurchased = bool.Parse(array2[6]);
				wP8ProductTemplate.TransactionID = array2[7];
				_products.Add(wP8ProductTemplate);
			}
			_IsInitialized = true;
		}
		OnInitComplete__BackingField(wP8_InAppsInitResult);
	}

	private void PurchaseCallback(string result)
	{
		string[] array = result.Split(new string[1] { "|" }, StringSplitOptions.None);
		int num = int.Parse(array[0]);
		WP8PurchseResponce wP8PurchseResponce = null;
		if (num == 0)
		{
			wP8PurchseResponce = new WP8PurchseResponce((WP8PurchaseCodes)Enum.Parse(typeof(WP8PurchaseCodes), array[1]), array[2]);
			wP8PurchseResponce.TransactionId = array[3];
		}
		else
		{
			wP8PurchseResponce = new WP8PurchseResponce((WP8PurchaseCodes)Enum.Parse(typeof(WP8PurchaseCodes), array[2]), array[1]);
		}
		OnPurchaseFinished__BackingField(wP8PurchseResponce);
	}

	private void ConsumeCallback(string result)
	{
		string[] array = result.Split(new string[1] { "|" }, StringSplitOptions.None);
		int num = int.Parse(array[0]);
		WP8ConsumeResponse wP8ConsumeResponse = null;
		if (num == 0)
		{
			wP8ConsumeResponse = new WP8ConsumeResponse((WP8ConsumeCodes)Enum.Parse(typeof(WP8ConsumeCodes), array[1]), array[2]);
			wP8ConsumeResponse.TransactionId = array[3];
		}
		else
		{
			wP8ConsumeResponse = new WP8ConsumeResponse((WP8ConsumeCodes)Enum.Parse(typeof(WP8ConsumeCodes), array[2]), array[1]);
		}
		OnConsumeFinished__BackingField(wP8ConsumeResponse);
	}

	private void DefferedPurchaseCallback(string result)
	{
		string[] array = result.Split(new string[1] { "|" }, StringSplitOptions.None);
		int num = int.Parse(array[0]);
		WP8PurchseResponce wP8PurchseResponce = null;
		if (num == 0)
		{
			wP8PurchseResponce = new WP8PurchseResponce((WP8PurchaseCodes)Enum.Parse(typeof(WP8PurchaseCodes), array[1]), array[2]);
			wP8PurchseResponce.TransactionId = array[3];
		}
		else
		{
			wP8PurchseResponce = new WP8PurchseResponce((WP8PurchaseCodes)Enum.Parse(typeof(WP8PurchaseCodes), array[2]), array[1]);
		}
	}
}
