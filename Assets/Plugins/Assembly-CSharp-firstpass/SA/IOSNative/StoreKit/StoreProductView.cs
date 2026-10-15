using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SA.Common.Pattern;

namespace SA.IOSNative.StoreKit
{
	public class StoreProductView
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action Loaded__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action LoadFailed__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action Appeared__BackingField = () =>
		{
		};

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action Dismissed__BackingField = () =>
		{
		};

		private int _id;

		private List<string> _ids = new List<string>();

		public int Id
		{
			get
			{
				return _id;
			}
		}

		public event Action Loaded
		{
			add
			{
				Action action = Loaded__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Loaded__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = Loaded__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Loaded__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action LoadFailed
		{
			add
			{
				Action action = LoadFailed__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref LoadFailed__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = LoadFailed__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref LoadFailed__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action Appeared
		{
			add
			{
				Action action = Appeared__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Appeared__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = Appeared__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Appeared__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public event Action Dismissed
		{
			add
			{
				Action action = Dismissed__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Dismissed__BackingField, (Action)Delegate.Combine(action2, value), action);
				}
				while ((object)action != action2);
			}
			remove
			{
				Action action = Dismissed__BackingField;
				Action action2;
				do
				{
					action2 = action;
					action = Interlocked.CompareExchange(ref Dismissed__BackingField, (Action)Delegate.Remove(action2, value), action);
				}
				while ((object)action != action2);
			}
		}

		public StoreProductView()
		{
			foreach (string item in IOSNativeSettings.Instance.DefaultStoreProductsView)
			{
				addProductId(item);
			}
			Singleton<PaymentManager>.Instance.RegisterProductView(this);
		}

		public StoreProductView(params string[] ids)
		{
			foreach (string productId in ids)
			{
				addProductId(productId);
			}
			Singleton<PaymentManager>.Instance.RegisterProductView(this);
		}

		public void addProductId(string productId)
		{
			if (!_ids.Contains(productId))
			{
				_ids.Add(productId);
			}
		}

		public void Load()
		{
		}

		public void Show()
		{
		}

		public void OnProductViewAppeard()
		{
			Appeared__BackingField();
		}

		public void OnProductViewDismissed()
		{
			Dismissed__BackingField();
		}

		public void OnContentLoaded()
		{
			Show();
			Loaded__BackingField();
		}

		public void OnContentLoadFailed()
		{
			LoadFailed__BackingField();
		}

		public void SetId(int viewId)
		{
			_id = viewId;
		}
	}
}
