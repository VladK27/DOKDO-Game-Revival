using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SA.Common.Data;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.IOSNative.StoreKit;
using UnityEngine;

public class MarketExample : BaseIOSFeaturePreview
{
	private byte[] ReceiptData;

	private void Awake()
	{
	}

	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "In-App Purchases", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Init"))
		{
			PaymentManagerExample.init();
		}
		if (Singleton<PaymentManager>.Instance.IsStoreLoaded)
		{
			GUI.enabled = true;
		}
		else
		{
			GUI.enabled = false;
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Perform Buy #1"))
		{
			PaymentManagerExample.buyItem("your.product.id1.here");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Perform Buy #2"))
		{
			PaymentManagerExample.buyItem("your.product.id2.here");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Restore Purchases"))
		{
			Singleton<PaymentManager>.Instance.RestorePurchases();
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Verify Last Purchases"))
		{
			Singleton<PaymentManager>.Instance.VerifyLastPurchase("https://sandbox.itunes.apple.com/verifyReceipt");
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Load Product View"))
		{
			StoreProductView storeProductView = new StoreProductView("333700869");
			storeProductView.Dismissed += StoreProductViewDisnissed;
			storeProductView.Load();
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Is Payments Enabled On device"))
		{
			IOSNativePopUpManager.showMessage("Payments Settings State", "Is Payments Enabled: " + Singleton<PaymentManager>.Instance.IsInAppPurchasesEnabled);
		}
		StartX = XStartPos;
		StartY += YButtonStep;
		StartY += YLableStep;
		GUI.enabled = true;
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Local Receipt Validation", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth + 10, buttonHeight), "Load Receipt"))
		{
			ISN_Security.OnReceiptLoaded += OnReceiptLoaded;
			Singleton<ISN_Security>.Instance.RetrieveLocalReceipt();
		}
	}

	private void StoreProductViewDisnissed()
	{
		ISN_Logger.Log("Store Product View was dismissed");
	}

	private void OnReceiptLoaded(ISN_LocalReceiptResult result)
	{
		ISN_Logger.Log("OnReceiptLoaded");
		ISN_Security.OnReceiptLoaded -= OnReceiptLoaded;
		if (result.Receipt != null)
		{
			ReceiptData = result.Receipt;
			IOSDialog iOSDialog = IOSDialog.Create("Success", "Receipt loaded, byte array length: " + result.Receipt.Length + " Would you like to veriday it with Apple Sandbox server?");
			iOSDialog.OnComplete += OnVerifayComplete;
		}
		else
		{
			IOSDialog iOSDialog2 = IOSDialog.Create("Failed", "No Receipt found on the device. Would you like to refresh local Receipt?");
			iOSDialog2.OnComplete += OnComplete;
		}
	}

	private void OnVerifayComplete(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			StartCoroutine(SendRequest());
		}
	}

	private IEnumerator SendRequest()
	{
		string base64string = Convert.ToBase64String(ReceiptData);
		string data = Json.Serialize(new Dictionary<string, object> { { "receipt-data", base64string } });
		byte[] binaryData = Encoding.UTF8.GetBytes(data);
		WWW www = new WWW("https://sandbox.itunes.apple.com/verifyReceipt", binaryData);
		yield return www;
		if (www.error == null)
		{
			Debug.Log(www.text);
		}
		else
		{
			Debug.Log(www.error);
		}
	}

	private void OnComplete(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			ISN_Security.OnReceiptRefreshComplete += OnReceiptRefreshComplete;
			Singleton<ISN_Security>.Instance.StartReceiptRefreshRequest();
		}
	}

	private void OnReceiptRefreshComplete(Result res)
	{
		if (res.IsSucceeded)
		{
			IOSDialog iOSDialog = IOSDialog.Create("Success", "Receipt Refreshed, would you like to check it again?");
			iOSDialog.OnComplete += Dialog_RetrieveLocalReceipt;
		}
		else
		{
			IOSNativePopUpManager.showMessage("Fail", "Receipt Refresh Failed");
		}
	}

	private void Dialog_RetrieveLocalReceipt(IOSDialogResult res)
	{
		if (res == IOSDialogResult.YES)
		{
			ISN_Security.OnReceiptLoaded += OnReceiptLoaded;
			Singleton<ISN_Security>.Instance.RetrieveLocalReceipt();
		}
	}
}
