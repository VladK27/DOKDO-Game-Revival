using UnityEngine;
using UnityEngine.UI;

public class AppsFlyerTrackerCallbacks : MonoBehaviour
{
	public Text callbacks;

	private void Start()
	{
		MonoBehaviour.print("AppsFlyerTrackerCallbacks on Start");
	}

	private void Update()
	{
	}

	public void didReceiveConversionData(string conversionData)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got conversion data = " + conversionData);
	}

	public void didReceiveConversionDataWithError(string error)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got conversion data error = " + error);
	}

	public void didFinishValidateReceipt(string validateResult)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got didFinishValidateReceipt  = " + validateResult);
	}

	public void didFinishValidateReceiptWithError(string error)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got idFinishValidateReceiptWithError error = " + error);
	}

	public void onAppOpenAttribution(string validateResult)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got onAppOpenAttribution  = " + validateResult);
	}

	public void onAppOpenAttributionFailure(string error)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got onAppOpenAttributionFailure error = " + error);
	}

	public void onInAppBillingSuccess()
	{
		printCallback("AppsFlyerTrackerCallbacks:: got onInAppBillingSuccess succcess");
	}

	public void onInAppBillingFailure(string error)
	{
		printCallback("AppsFlyerTrackerCallbacks:: got onInAppBillingFailure error = " + error);
	}

	private void printCallback(string str)
	{
		Text text = callbacks;
		text.text = text.text + str + "\n";
	}
}
