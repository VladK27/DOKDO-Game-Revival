using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public abstract class UM_BaseInAppClient
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_BillingConnectionResult> OnServiceConnected__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_PurchaseResult> OnPurchaseFinished__BackingField = delegate
	{
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<UM_BaseResult> OnRestoreFinished__BackingField = delegate
	{
	};

	protected bool _IsConnected;

	public bool IsConnected
	{
		get
		{
			return _IsConnected;
		}
	}

	public event Action<UM_BillingConnectionResult> OnServiceConnected
	{
		add
		{
			Action<UM_BillingConnectionResult> action = OnServiceConnected__BackingField;
			Action<UM_BillingConnectionResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnServiceConnected__BackingField, (Action<UM_BillingConnectionResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_BillingConnectionResult> action = OnServiceConnected__BackingField;
			Action<UM_BillingConnectionResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnServiceConnected__BackingField, (Action<UM_BillingConnectionResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_PurchaseResult> OnPurchaseFinished
	{
		add
		{
			Action<UM_PurchaseResult> action = OnPurchaseFinished__BackingField;
			Action<UM_PurchaseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseFinished__BackingField, (Action<UM_PurchaseResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_PurchaseResult> action = OnPurchaseFinished__BackingField;
			Action<UM_PurchaseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnPurchaseFinished__BackingField, (Action<UM_PurchaseResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<UM_BaseResult> OnRestoreFinished
	{
		add
		{
			Action<UM_BaseResult> action = OnRestoreFinished__BackingField;
			Action<UM_BaseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRestoreFinished__BackingField, (Action<UM_BaseResult>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<UM_BaseResult> action = OnRestoreFinished__BackingField;
			Action<UM_BaseResult> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OnRestoreFinished__BackingField, (Action<UM_BaseResult>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Purchase(string productId)
	{
		UM_InAppProduct productById = UM_InAppPurchaseManager.GetProductById(productId);
		if (productById != null)
		{
			Purchase(productById);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	public abstract void Purchase(UM_InAppProduct product);

	public void Subscribe(string productId)
	{
		UM_InAppProduct productById = UM_InAppPurchaseManager.GetProductById(productId);
		if (productById != null)
		{
			Subscribe(productById);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	public abstract void Subscribe(UM_InAppProduct product);

	public void Consume(string productId)
	{
		UM_InAppProduct productById = UM_InAppPurchaseManager.GetProductById(productId);
		if (productById != null)
		{
			Consume(productById);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	public abstract void Consume(UM_InAppProduct product);

	public void FinishTransaction(string productId)
	{
		UM_InAppProduct productById = UM_InAppPurchaseManager.GetProductById(productId);
		if (productById != null)
		{
			FinishTransaction(productById);
		}
		else
		{
			SendNoTemplateEvent();
		}
	}

	public abstract void FinishTransaction(UM_InAppProduct product);

	public bool IsProductPurchased(string productId)
	{
		UM_InAppProduct productById = UM_InAppPurchaseManager.GetProductById(productId);
		if (productById != null)
		{
			return IsProductPurchased(productById);
		}
		return false;
	}

	public virtual bool IsProductPurchased(UM_InAppProduct product)
	{
		return UM_InAppPurchaseManager.IsLocalPurchaseRecordExists(product);
	}

	protected void SendNoTemplateEvent()
	{
		UnityEngine.Debug.LogWarning("UM: Product tamplate not found");
		UM_PurchaseResult e = new UM_PurchaseResult();
		SendPurchaseFinishedEvent(e);
	}

	protected void SendServiceConnectedEvent(UM_BillingConnectionResult e)
	{
		OnServiceConnected__BackingField(e);
	}

	protected void SendPurchaseFinishedEvent(UM_PurchaseResult e)
	{
		OnPurchaseFinished__BackingField(e);
	}

	protected void SendRestoreFinishedEvent(UM_BaseResult e)
	{
		OnRestoreFinished__BackingField(e);
	}
}
