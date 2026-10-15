using System;
using UnityEngine;

[Serializable]
public class GK_AchievementTemplate
{
	public bool IsOpen = true;

	public string Id = string.Empty;

	public string Title = "New Achievement";

	public string Description = string.Empty;

	public float _progress;

	public Texture2D Texture;

	public float Progress
	{
		get
		{
			if (IOSNativeSettings.Instance.UsePPForAchievements)
			{
				return GameCenterManager.GetAchievementProgress(Id);
			}
			return _progress;
		}
		set
		{
			_progress = value;
		}
	}
}
