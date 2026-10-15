using System;
using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

public class PlusButtonsAPIExample : MonoBehaviour
{
	private List<AN_PlusButton> Abuttons = new List<AN_PlusButton>();

	private AN_PlusButton PlusButton;

	private string PlusUrl = "https://unionassets.com/";

	public void CreatePlusButtons()
	{
		if (Abuttons.Count != 0)
		{
			return;
		}
		AN_PlusButton aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_TALL, AN_PlusBtnAnnotation.ANNOTATION_BUBBLE);
		aN_PlusButton.SetGravity(TextAnchor.UpperLeft);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_SMALL, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.UpperRight);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_MEDIUM, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.UpperCenter);
		Abuttons.Add(aN_PlusButton);
		aN_PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_STANDARD, AN_PlusBtnAnnotation.ANNOTATION_INLINE);
		aN_PlusButton.SetGravity(TextAnchor.MiddleLeft);
		Abuttons.Add(aN_PlusButton);
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.ButtonClicked = (Action)Delegate.Combine(abutton.ButtonClicked, new Action(ButtonClicked));
		}
	}

	public void HideButtons()
	{
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.Hide();
		}
	}

	public void ShoweButtons()
	{
		foreach (AN_PlusButton abutton in Abuttons)
		{
			abutton.Show();
		}
	}

	public void CreateRandomPostButton()
	{
		if (PlusButton == null)
		{
			PlusButton = new AN_PlusButton(PlusUrl, AN_PlusBtnSize.SIZE_STANDARD, AN_PlusBtnAnnotation.ANNOTATION_BUBBLE);
			PlusButton.SetPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
			AN_PlusButton plusButton = PlusButton;
			plusButton.ButtonClicked = (Action)Delegate.Combine(plusButton.ButtonClicked, new Action(ButtonClicked));
		}
	}

	public void ChangePosPostButton()
	{
		if (PlusButton != null)
		{
			PlusButton.SetPosition(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
		}
	}

	private void ButtonClicked()
	{
		AndroidMessage.Create("Click Detected", "Plus Button Click Detected");
	}

	private void SendInvitation()
	{
		GP_AppInviteBuilder gP_AppInviteBuilder = new GP_AppInviteBuilder("Test Title");
		gP_AppInviteBuilder.SetMessage("Test Message");
		gP_AppInviteBuilder.SetDeepLink("http://testUrl");
		gP_AppInviteBuilder.SetCallToActionText("Test Text");
		GP_AppInvitesController.ActionAppInvitesSent += HandleActionAppInvitesSent;
		Singleton<GP_AppInvitesController>.Instance.StartInvitationDialog(gP_AppInviteBuilder);
	}

	private void HandleActionAppInvitesSent(GP_SendAppInvitesResult res)
	{
		if (res.IsSucceeded)
		{
			Debug.Log("Invitation was sent to " + res.InvitationIds.Length + " people");
			AN_PoupsProxy.showMessage("Success", "Invitation was sent to " + res.InvitationIds.Length + " people");
		}
		else
		{
			Debug.Log("App invite failed" + res.Message);
			AN_PoupsProxy.showMessage("Fail", "App invite failed" + res.Message);
		}
		GP_AppInvitesController.ActionAppInvitesSent -= HandleActionAppInvitesSent;
	}

	private void GetInvitation()
	{
		GP_AppInvitesController.ActionAppInviteRetrieved += HandleActionAppInviteRetrieved;
		Singleton<GP_AppInvitesController>.Instance.GetInvitation(true);
	}

	private void HandleActionAppInviteRetrieved(GP_RetrieveAppInviteResult res)
	{
		GP_AppInvitesController.ActionAppInviteRetrieved -= HandleActionAppInviteRetrieved;
		if (res.IsSucceeded)
		{
			Debug.Log("Invitation Retrieved");
			GP_AppInvite appInvite = res.AppInvite;
			Debug.Log("Invitation Id: " + appInvite.Id);
			Debug.Log("Invitation Deep Link: " + appInvite.DeepLink);
			Debug.Log("Is Opened From PlayStore: " + appInvite.IsOpenedFromPlayStore);
		}
		else
		{
			Debug.Log("No invitation data found");
		}
	}

	private void AddNewFriends()
	{
		Debug.Log("AddNewFriends");
		AndroidNativeUtility.InvitePlusFriends();
	}

	private void OnDestroy()
	{
		HideButtons();
		if (PlusButton != null)
		{
			PlusButton.Hide();
		}
	}
}
