using UnityEngine;

public class NativePopUpsTab : FeatureTab
{
	private string rateText = "If you enjoy using Google Earth, please take a moment to rate it. Thanks for your support!";

	private string rateUrl = "market://details?id=com.unionassets.android.plugin.preview";

	public void RateDialogPopUp()
	{
		AndroidRateUsPopUp androidRateUsPopUp = AndroidRateUsPopUp.Create("Rate Us", rateText, rateUrl);
		androidRateUsPopUp.ActionComplete += OnRatePopUpClose;
	}

	public void DialogPopUp()
	{
		AndroidDialog androidDialog = AndroidDialog.Create("Dialog Titile", "Dialog message");
		androidDialog.ActionComplete += OnDialogClose;
	}

	public void MessagePopUp()
	{
		AndroidMessage androidMessage = AndroidMessage.Create("Message Titile", "Message message");
		androidMessage.ActionComplete += OnMessageClose;
	}

	public void ShowPreloader()
	{
		Invoke("HidePreloader", 2f);
		AndroidNativeUtility.ShowPreloader("Loading", "Wait 2 seconds please");
	}

	public void HidePreloader()
	{
		AndroidNativeUtility.HidePreloader();
	}

	public void OpenRatingPage()
	{
		AndroidNativeUtility.OpenAppRatingPage(rateUrl);
	}

	private void OnRatePopUpClose(AndroidDialogResult result)
	{
		switch (result)
		{
		case AndroidDialogResult.RATED:
			Debug.Log("RATED button pressed");
			break;
		case AndroidDialogResult.REMIND:
			Debug.Log("REMIND button pressed");
			break;
		case AndroidDialogResult.DECLINED:
			Debug.Log("DECLINED button pressed");
			break;
		}
		AN_PoupsProxy.showMessage("Result", result.ToString() + " button pressed");
	}

	private void OnDialogClose(AndroidDialogResult result)
	{
		switch (result)
		{
		case AndroidDialogResult.YES:
			Debug.Log("Yes button pressed");
			break;
		case AndroidDialogResult.NO:
			Debug.Log("No button pressed");
			break;
		}
		AN_PoupsProxy.showMessage("Result", result.ToString() + " button pressed");
	}

	private void OnMessageClose(AndroidDialogResult result)
	{
		AN_PoupsProxy.showMessage("Result", "Message Closed");
	}
}
