using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Models;
using SA.Common.Pattern;
using UnityEngine;

namespace SA.IOSNative.StoreKit
{
	public class PaymentManager : Singleton<PaymentManager>
	{
		public const string APPLE_VERIFICATION_SERVER = "https://buy.itunes.apple.com/verifyReceipt";

		public const string SANDBOX_VERIFICATION_SERVER = "https://sandbox.itunes.apple.com/verifyReceipt";

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Result> OnStoreKitInitComplete__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action OnRestoreStarted__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<RestoreResult> OnRestoreComplete__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<string> OnTransactionStarted__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<PurchaseResult> OnTransactionComplete__BackingField = delegate
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<VerificationResponse> OnVerificationComplete__BackingField = delegate
		{
		};

		private bool _IsStoreLoaded;

		private bool _IsWaitingLoadResult;

		private static int _nextId = 1;

		private Dictionary<int, StoreProductView> _productsView = new Dictionary<int, StoreProductView>();

		private static string lastPurchasedProduct;

		public List<Product> Products
		{
			get
			{
				return IOSNativeSettings.Instance.InAppProducts;
			}
		}

		public bool IsStoreLoaded
		{
			get
			{
				return _IsStoreLoaded;
			}
		}

		public bool IsInAppPurchasesEnabled
		{
			get
			{
				return BillingNativeBridge.ISN_InAppSettingState();
			}
		}

		public bool IsWaitingLoadResult
		{
			get
			{
				return _IsWaitingLoadResult;
			}
		}

		private static int NextId
		{
			get
			{
				_nextId++;
				return _nextId;
			}
		}

		public static event Action<Result> OnStoreKitInitComplete
		{
			add
			{
				Action<Result> action = OnStoreKitInitComplete__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnStoreKitInitComplete__BackingField, (Action<Result>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<Result> action = OnStoreKitInitComplete__BackingField;
				Action<Result> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnStoreKitInitComplete__BackingField, (Action<Result>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action OnRestoreStarted
		{
			add
			{
				Action action = OnRestoreStarted__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRestoreStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = OnRestoreStarted__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRestoreStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<RestoreResult> OnRestoreComplete
		{
			add
			{
				Action<RestoreResult> action = OnRestoreComplete__BackingField;
				Action<RestoreResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRestoreComplete__BackingField, (Action<RestoreResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<RestoreResult> action = OnRestoreComplete__BackingField;
				Action<RestoreResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnRestoreComplete__BackingField, (Action<RestoreResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<string> OnTransactionStarted
		{
			add
			{
				Action<string> action = OnTransactionStarted__BackingField;
				Action<string> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnTransactionStarted__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<string> action = OnTransactionStarted__BackingField;
				Action<string> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnTransactionStarted__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<PurchaseResult> OnTransactionComplete
		{
			add
			{
				Action<PurchaseResult> action = OnTransactionComplete__BackingField;
				Action<PurchaseResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnTransactionComplete__BackingField, (Action<PurchaseResult>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<PurchaseResult> action = OnTransactionComplete__BackingField;
				Action<PurchaseResult> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnTransactionComplete__BackingField, (Action<PurchaseResult>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public static event Action<VerificationResponse> OnVerificationComplete
		{
			add
			{
				Action<VerificationResponse> action = OnVerificationComplete__BackingField;
				Action<VerificationResponse> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnVerificationComplete__BackingField, (Action<VerificationResponse>)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action<VerificationResponse> action = OnVerificationComplete__BackingField;
				Action<VerificationResponse> action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref OnVerificationComplete__BackingField, (Action<VerificationResponse>)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		private void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		}

		public void LoadStore(bool forceLoad = false)
		{
			if (_IsStoreLoaded && !forceLoad)
			{
				Invoke("FireSuccessInitEvent", 1f);
			}
			else
			{
				if (_IsWaitingLoadResult)
				{
					return;
				}
				_IsWaitingLoadResult = true;
				string text = string.Empty;
				int count = Products.Count;
				for (int i = 0; i < count; i++)
				{
					if (i != 0)
					{
						text += ",";
					}
					text += Products[i].Id;
				}
				ISN_SoomlaGrow.Init();
				if (!Application.isEditor)
				{
					BillingNativeBridge.LoadStore(text);
					if (IOSNativeSettings.Instance.TransactionsHandlingMode == TransactionsHandlingMode.Manual)
					{
						BillingNativeBridge.EnableManulaTransactionsMode();
					}
				}
				else if (IOSNativeSettings.Instance.InAppsEditorTesting)
				{
					Invoke("EditorFakeInitEvent", 1f);
				}
			}
		}

		public void BuyProduct(string productId)
		{
			if (!Application.isEditor)
			{
				OnTransactionStarted__BackingField(productId);
				if (!_IsStoreLoaded)
				{
					ISN_Logger.Log("buyProduct shouldn't be called before StoreKit is initialized");
					Error error = new Error(4, "StoreKit not yet initialized");
					SendTransactionFailEvent(productId, error);
				}
				else
				{
					BillingNativeBridge.BuyProduct(productId);
				}
			}
			else if (IOSNativeSettings.Instance.InAppsEditorTesting)
			{
				FireProductBoughtEvent(productId, string.Empty, string.Empty, string.Empty, false);
			}
		}

		public void FinishTransaction(string productId)
		{
			BillingNativeBridge.FinishTransaction(productId);
		}

		public void AddProductId(string productId)
		{
			Product product = new Product();
			product.Id = productId;
			AddProduct(product);
		}

		public void AddProduct(Product product)
		{
			bool flag = false;
			int index = 0;
			foreach (Product product2 in Products)
			{
				if (product2.Id.Equals(product.Id))
				{
					flag = true;
					index = Products.IndexOf(product2);
					break;
				}
			}
			if (flag)
			{
				Products[index] = product;
			}
			else
			{
				Products.Add(product);
			}
		}

		public Product GetProductById(string prodcutId)
		{
			foreach (Product product2 in Products)
			{
				if (product2.Id.Equals(prodcutId))
				{
					return product2;
				}
			}
			Product product = new Product();
			product.Id = prodcutId;
			Products.Add(product);
			return product;
		}

		public void RestorePurchases()
		{
			if (!_IsStoreLoaded)
			{
				Error e = new Error(7, "Store Kit Initilizations required");
				RestoreResult obj = new RestoreResult(e);
				OnRestoreComplete__BackingField(obj);
				return;
			}
			OnRestoreStarted__BackingField();
			if (!Application.isEditor)
			{
				BillingNativeBridge.RestorePurchases();
			}
			else
			{
				if (!IOSNativeSettings.Instance.InAppsEditorTesting)
				{
					return;
				}
				foreach (Product product in Products)
				{
					if (product.Type == ProductType.NonConsumable)
					{
						ISN_Logger.Log("Restored: " + product.Id);
						FireProductBoughtEvent(product.Id, string.Empty, string.Empty, string.Empty, true);
					}
				}
				FireRestoreCompleteEvent();
			}
		}

		public void VerifyLastPurchase(string url)
		{
			BillingNativeBridge.VerifyLastPurchase(url);
		}

		public void RegisterProductView(StoreProductView view)
		{
			view.SetId(NextId);
			_productsView.Add(view.Id, view);
		}

		private void OnStoreKitInitFailed(string data)
		{
			Error error = new Error(data);
			_IsStoreLoaded = false;
			_IsWaitingLoadResult = false;
			Result obj = new Result(error);
			OnStoreKitInitComplete__BackingField(obj);
			if (!IOSNativeSettings.Instance.DisablePluginLogs)
			{
				ISN_Logger.Log("STORE_KIT_INIT_FAILED Error: " + error.Message);
			}
		}

		private void onStoreDataReceived(string data)
		{
			if (data.Equals(string.Empty))
			{
				ISN_Logger.Log("InAppPurchaseManager, no products avaiable");
				Result obj = new Result();
				OnStoreKitInitComplete__BackingField(obj);
				return;
			}
			string[] array = data.Split('|');
			for (int i = 0; i < array.Length; i += 7)
			{
				string prodcutId = array[i];
				Product productById = GetProductById(prodcutId);
				productById.DisplayName = array[i + 1];
				productById.Description = array[i + 2];
				productById.LocalizedPrice = array[i + 3];
				productById.Price = Convert.ToSingle(array[i + 4]);
				productById.CurrencyCode = array[i + 5];
				productById.CurrencySymbol = array[i + 6];
				productById.IsAvailable = true;
			}
			ISN_Logger.Log("InAppPurchaseManager, total products in settings: " + Products.Count);
			int num = 0;
			foreach (Product product in Products)
			{
				if (product.IsAvailable)
				{
					num++;
				}
			}
			ISN_Logger.Log("InAppPurchaseManager, total avaliable products" + num);
			FireSuccessInitEvent();
		}

		private void onProductBought(string array)
		{
			string[] array2 = array.Split("|"[0]);
			bool isRestored = false;
			if (array2[1].Equals("0"))
			{
				isRestored = true;
			}
			string productIdentifier = array2[0];
			FireProductBoughtEvent(productIdentifier, array2[2], array2[3], array2[4], isRestored);
		}

		private void onProductStateDeferred(string productIdentifier)
		{
			PurchaseResult obj = new PurchaseResult(productIdentifier, PurchaseState.Deferred, string.Empty, string.Empty, string.Empty);
			OnTransactionComplete__BackingField(obj);
		}

		private void onTransactionFailed(string data)
		{
			string[] array = data.Split(new string[1] { "|%|".ToString() }, StringSplitOptions.None);
			string productIdentifier = array[0];
			Error error = new Error(array[1]);
			SendTransactionFailEvent(productIdentifier, error);
		}

		private void onVerificationResult(string data)
		{
			VerificationResponse obj = new VerificationResponse(lastPurchasedProduct, data);
			OnVerificationComplete__BackingField(obj);
		}

		public void onRestoreTransactionFailed(string array)
		{
			Error e = new Error(array);
			RestoreResult obj = new RestoreResult(e);
			OnRestoreComplete__BackingField(obj);
		}

		public void onRestoreTransactionComplete(string array)
		{
			FireRestoreCompleteEvent();
		}

		private void OnProductViewLoaded(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (_productsView.ContainsKey(key))
			{
				_productsView[key].OnContentLoaded();
			}
		}

		private void OnProductViewLoadedFailed(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (_productsView.ContainsKey(key))
			{
				_productsView[key].OnContentLoadFailed();
			}
		}

		private void OnProductViewDismissed(string viewId)
		{
			int key = Convert.ToInt32(viewId);
			if (_productsView.ContainsKey(key))
			{
				_productsView[key].OnProductViewDismissed();
			}
		}

		private void FireSuccessInitEvent()
		{
			_IsStoreLoaded = true;
			_IsWaitingLoadResult = false;
			Result obj = new Result();
			OnStoreKitInitComplete__BackingField(obj);
		}

		private void FireRestoreCompleteEvent()
		{
			RestoreResult obj = new RestoreResult();
			OnRestoreComplete__BackingField(obj);
		}

		private void FireProductBoughtEvent(string productIdentifier, string applicationUsername, string receipt, string transactionIdentifier, bool IsRestored)
		{
			PurchaseState state = (IsRestored ? PurchaseState.Restored : PurchaseState.Purchased);
			PurchaseResult purchaseResult = new PurchaseResult(productIdentifier, state, applicationUsername, receipt, transactionIdentifier);
			lastPurchasedProduct = purchaseResult.ProductIdentifier;
			OnTransactionComplete__BackingField(purchaseResult);
		}

		private void SendTransactionFailEvent(string productIdentifier, Error error)
		{
			PurchaseResult obj = new PurchaseResult(productIdentifier, error);
			OnTransactionComplete__BackingField(obj);
		}

		private void EditorFakeInitEvent()
		{
			FireSuccessInitEvent();
		}
	}
}
