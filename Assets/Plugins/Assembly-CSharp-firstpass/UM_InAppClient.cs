using System;

public interface UM_InAppClient
{
	bool IsConnected { get; }

	event Action<UM_BillingConnectionResult> OnServiceConnected;

	event Action<UM_PurchaseResult> OnPurchaseFinished;

	event Action<UM_BaseResult> OnRestoreFinished;

	void Connect();

	void Purchase(string productId);

	void Purchase(UM_InAppProduct product);

	void Subscribe(UM_InAppProduct product);

	void Subscribe(string productId);

	void Consume(string productId);

	void Consume(UM_InAppProduct product);

	void FinishTransaction(string productId);

	void FinishTransaction(UM_InAppProduct product);

	void RestorePurchases();

	bool IsProductPurchased(string productId);

	bool IsProductPurchased(UM_InAppProduct product);
}
