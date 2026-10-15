using System;
using SA.Common.Models;
using UnityEngine;

public class WSAMultimediaManager
{
	public static void PickImageFromGallery()
	{
	}

	public static void SaveScreenshot()
	{
		ScreenshotMaker screenshotMaker = ScreenshotMaker.Create();
		screenshotMaker.OnScreenshotReady = (Action<Texture2D>)Delegate.Combine(screenshotMaker.OnScreenshotReady, new Action<Texture2D>(ScreenshotReady));
		ScreenshotMaker.Create().GetScreenshot();
	}

	private static void ScreenshotReady(Texture2D screenshot)
	{
		Debug.Log("[ScreenshotReady]");
		ScreenshotMaker screenshotMaker = ScreenshotMaker.Create();
		screenshotMaker.OnScreenshotReady = (Action<Texture2D>)Delegate.Remove(screenshotMaker.OnScreenshotReady, new Action<Texture2D>(ScreenshotReady));
	}
}
