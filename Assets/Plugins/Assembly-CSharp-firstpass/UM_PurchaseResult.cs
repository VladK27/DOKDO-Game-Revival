using SA.Common.Util;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class UM_PurchaseResult
{
	public bool isSuccess;

	public UM_InAppProduct product = new UM_InAppProduct();

	private int _ResponceCode = -1;

	private string EditorDummyId = string.Empty;

	public GooglePurchaseTemplate Google_PurchaseInfo;

	public PurchaseResult IOS_PurchaseInfo;

	public WP8PurchseResponce WP8_PurchaseInfo;

	public AMN_PurchaseResponse Amazon_PurchaseInfo;

	public string TransactionId
	{
		get
		{
			if (Application.isEditor)
			{
				if (string.IsNullOrEmpty(EditorDummyId))
				{
					EditorDummyId = IdFactory.NextId.ToString();
				}
				return EditorDummyId;
			}
			switch (Application.platform)
			{
			case RuntimePlatform.Android:
				if (UltimateMobileSettings.Instance.PlatformEngine == UM_PlatformDependencies.Amazon)
				{
					return Amazon_PurchaseInfo.ReceiptId;
				}
				return Google_PurchaseInfo.OrderId;
			case RuntimePlatform.IPhonePlayer:
				return IOS_PurchaseInfo.TransactionIdentifier;
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
				return WP8_PurchaseInfo.TransactionId;
			default:
				return string.Empty;
			}
		}
	}

	public int ResponceCode
	{
		get
		{
			return _ResponceCode;
		}
	}

	public void SetResponceCode(int code)
	{
		_ResponceCode = code;
	}
}
