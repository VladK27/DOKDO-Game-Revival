using System;

public interface AN_InAppClient
{
	AndroidInventory Inventory { get; }

	bool IsConnectingToServiceInProcess { get; }

	bool IsProductRetrievingInProcess { get; }

	bool IsConnected { get; }

	bool IsInventoryLoaded { get; }

	event Action<BillingResult> ActionProductPurchased;

	event Action<BillingResult> ActionProductConsumed;

	event Action<BillingResult> ActionBillingSetupFinished;

	event Action<BillingResult> ActionRetrieveProducsFinished;

	void AddProduct(string SKU);

	void AddProduct(GoogleProductTemplate template);

	void RetrieveProducDetails();

	void Purchase(string SKU);

	void Purchase(string SKU, string DeveloperPayload);

	void Subscribe(string SKU);

	void Subscribe(string SKU, string DeveloperPayload);

	void Consume(string SKU);

	void Connect();

	void Connect(string base64EncodedPublicKey);

	[Obsolete("LoadStore is deprectaed, plase use Connect instead")]
	void LoadStore();

	[Obsolete("LoadStore is deprectaed, plase use Connect instead")]
	void LoadStore(string base64EncodedPublicKey);
}
