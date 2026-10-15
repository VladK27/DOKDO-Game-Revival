using System;
using System.Collections.Generic;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlayEvents : Singleton<GooglePlayEvents>
{
	public Action<GooglePlayResult> OnEventsLoaded = delegate
	{
	};

	private List<GP_Event> _Events = new List<GP_Event>();

	public List<GP_Event> Events
	{
		get
		{
			return _Events;
		}
	}

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void LoadEvents()
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSQuestsEventsProxy.loadEvents();
		}
	}

	public void SumbitEvent(string eventId)
	{
		SumbitEvent(eventId, 1);
	}

	public void SumbitEvent(string eventId, int count)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSQuestsEventsProxy.sumbitEvent(eventId, count);
		}
	}

	private void OnGPEventsLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		GooglePlayResult googlePlayResult = new GooglePlayResult(array[0]);
		if (googlePlayResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 5)
			{
				GP_Event gP_Event = new GP_Event();
				gP_Event.Id = array[i];
				gP_Event.Description = array[i + 1];
				gP_Event.IconImageUrl = array[i + 2];
				gP_Event.FormattedValue = array[i + 3];
				gP_Event.Value = Convert.ToInt64(array[i + 4]);
				if (AndroidNativeSettings.Instance.LoadEventsIcons)
				{
					gP_Event.LoadIcon();
				}
				_Events.Add(gP_Event);
			}
		}
		OnEventsLoaded(googlePlayResult);
		Debug.Log("OnGPEventsLoaded, total:" + _Events.Count);
	}
}
