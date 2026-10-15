using System;
using UnityEngine;

public class WPN_PopUpExamples : WPNFeaturePreview
{
	private void OnGUI()
	{
		UpdateToStartPos();
		GUI.Label(new Rect(StartX, StartY, Screen.width, 40f), "Native Pop Ups", style);
		StartY += YLableStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Rate PopUp with events"))
		{
			WP8RateUsPopUp wP8RateUsPopUp = WP8RateUsPopUp.Create("Like this game?", "Please rate to support future updates!");
			wP8RateUsPopUp.OnComplete = (Action<WP8DialogResult>)Delegate.Combine(wP8RateUsPopUp.OnComplete, new Action<WP8DialogResult>(onRatePopUpClose));
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Dialog PopUp"))
		{
			WP8Dialog wP8Dialog = WP8Dialog.Create("Dialog Titile", "Dialog message");
			wP8Dialog.OnComplete = (Action<WP8DialogResult>)Delegate.Combine(wP8Dialog.OnComplete, new Action<WP8DialogResult>(onDialogClose));
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Message PopUp"))
		{
			WP8Message wP8Message = WP8Message.Create("Message Titile", "Message message");
			wP8Message.OnComplete = (Action<WP8DialogResult>)Delegate.Combine(wP8Message.OnComplete, new Action<WP8DialogResult>(onMessageClose));
		}
		StartX += XButtonStep;
		if (GUI.Button(new Rect(StartX, StartY, buttonWidth, buttonHeight), "Show Preloader"))
		{
			WP8NativeUtils.ShowPreloader();
			Invoke("HidePreloader", 2f);
		}
	}

	private void HidePreloader()
	{
		WP8NativeUtils.HidePreloader();
	}

	private void onRatePopUpClose(WP8DialogResult res)
	{
	}

	private void onDialogClose(WP8DialogResult res)
	{
	}

	private void onMessageClose(WP8DialogResult res)
	{
	}
}
