using System;
using System.Collections.Generic;
using System.Text;
using SA.Common.Pattern;
using UnityEngine;

public class GooglePlayQuests : Singleton<GooglePlayQuests>
{
	public Action<GP_QuestResult> OnQuestsLoaded = delegate
	{
	};

	public Action<GP_QuestResult> OnQuestsAccepted = delegate
	{
	};

	public Action<GP_QuestResult> OnQuestsCompleted = delegate
	{
	};

	public static GP_QuestsSelect[] SELECT_ALL_QUESTS = new GP_QuestsSelect[8]
	{
		GP_QuestsSelect.SELECT_ACCEPTED,
		GP_QuestsSelect.SELECT_COMPLETED,
		GP_QuestsSelect.SELECT_COMPLETED_UNCLAIMED,
		GP_QuestsSelect.SELECT_ENDING_SOON,
		GP_QuestsSelect.SELECT_EXPIRED,
		GP_QuestsSelect.SELECT_FAILED,
		GP_QuestsSelect.SELECT_OPEN,
		GP_QuestsSelect.SELECT_UPCOMING
	};

	private Dictionary<string, GP_Quest> _Quests = new Dictionary<string, GP_Quest>();

	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public void Init()
	{
	}

	public void LoadQuests(GP_QuestSortOrder sortOrder)
	{
		LoadQuests(sortOrder, SELECT_ALL_QUESTS);
	}

	public void LoadQuests(GP_QuestSortOrder sortOrder, params GP_QuestsSelect[] selectors)
	{
		if (GooglePlayConnection.CheckState())
		{
			string text = string.Empty;
			for (int i = 0; i < selectors.Length; i++)
			{
				int num = (int)selectors[i];
				text += num;
				text += ",";
			}
			text = text.TrimEnd(',');
			AN_GMSQuestsEventsProxy.loadQuests(text, (int)sortOrder);
		}
	}

	public void ShowQuests()
	{
		ShowQuests(SELECT_ALL_QUESTS);
	}

	public void ShowQuests(params GP_QuestsSelect[] selectors)
	{
		if (GooglePlayConnection.CheckState())
		{
			string text = string.Empty;
			for (int i = 0; i < selectors.Length; i++)
			{
				int num = (int)selectors[i];
				text += num;
				text += ",";
			}
			text = text.TrimEnd(',');
			AN_GMSQuestsEventsProxy.showSelectedQuests(text);
		}
	}

	public void AcceptQuest(string id)
	{
		if (GooglePlayConnection.CheckState())
		{
			AN_GMSQuestsEventsProxy.acceptQuest(id);
		}
	}

	public GP_Quest GetQuestById(string id)
	{
		if (_Quests.ContainsKey(id))
		{
			return _Quests[id];
		}
		GP_Quest gP_Quest = new GP_Quest();
		gP_Quest.Id = id;
		return gP_Quest;
	}

	public List<GP_Quest> GetQuests()
	{
		List<GP_Quest> list = new List<GP_Quest>();
		foreach (KeyValuePair<string, GP_Quest> quest in _Quests)
		{
			list.Add(quest.Value);
		}
		return list;
	}

	public List<GP_Quest> GetQuestsByState(GP_QuestState state)
	{
		List<GP_Quest> list = new List<GP_Quest>();
		foreach (KeyValuePair<string, GP_Quest> quest in _Quests)
		{
			if (state == quest.Value.state)
			{
				list.Add(quest.Value);
			}
		}
		return list;
	}

	private void OnGPQuestAccepted(string data)
	{
		string[] array = data.Split("|"[0]);
		GP_QuestResult gP_QuestResult = new GP_QuestResult(array[0]);
		gP_QuestResult.quest = GetQuestById(array[1]);
		OnQuestsAccepted(gP_QuestResult);
	}

	private void OnGPQuestCompleted(string data)
	{
		string[] array = data.Split("|"[0]);
		GP_QuestResult gP_QuestResult = new GP_QuestResult(array[0]);
		gP_QuestResult.quest = GetQuestById(array[1]);
		OnQuestsCompleted(gP_QuestResult);
	}

	private void OnGPQuestUpdated(string data)
	{
		string[] array = data.Split("|"[0]);
		int num = 0;
		UpdateQuestInfo(array[num], array[num + 1], array[num + 2], array[num + 3], array[num + 4], array[num + 5], array[num + 6], array[num + 7], array[num + 8], array[num + 9], array[num + 10], array[num + 11]);
	}

	private void OnGPQuestsLoaded(string data)
	{
		string[] array = data.Split("|"[0]);
		GP_QuestResult gP_QuestResult = new GP_QuestResult(array[0]);
		if (gP_QuestResult.IsSucceeded)
		{
			for (int i = 1; i < array.Length && !(array[i] == "endofline"); i += 12)
			{
				UpdateQuestInfo(array[i], array[i + 1], array[i + 2], array[i + 3], array[i + 4], array[i + 5], array[i + 6], array[i + 7], array[i + 8], array[i + 9], array[i + 10], array[i + 11]);
			}
		}
		OnQuestsLoaded(gP_QuestResult);
		Debug.Log("OnGPQuestsLoaded, total:" + _Quests.Count);
	}

	private void UpdateQuestInfo(string id, string name, string descr, string icon, string banner, string state, string timeUpdated, string timeAccepted, string timeEnded, string rewardData, string currentProgress, string targetProgress)
	{
		GP_Quest gP_Quest;
		if (_Quests.ContainsKey(id))
		{
			gP_Quest = _Quests[id];
		}
		else
		{
			gP_Quest = new GP_Quest();
			gP_Quest.Id = id;
			_Quests.Add(gP_Quest.Id, gP_Quest);
		}
		gP_Quest.Name = name;
		gP_Quest.Description = descr;
		gP_Quest.IconImageUrl = icon;
		gP_Quest.BannerImageUrl = banner;
		int state2 = Convert.ToInt32(state);
		gP_Quest.state = (GP_QuestState)state2;
		gP_Quest.LastUpdatedTimestamp = Convert.ToInt64(timeUpdated);
		gP_Quest.AcceptedTimestamp = Convert.ToInt64(timeAccepted);
		gP_Quest.EndTimestamp = Convert.ToInt64(timeEnded);
		gP_Quest.RewardData = Encoding.UTF8.GetBytes(rewardData);
		gP_Quest.CurrentProgress = Convert.ToInt64(currentProgress);
		gP_Quest.TargetProgress = Convert.ToInt64(targetProgress);
		if (AndroidNativeSettings.Instance.LoadQuestsIcons)
		{
			gP_Quest.LoadIcon();
		}
		if (AndroidNativeSettings.Instance.LoadQuestsImages)
		{
			gP_Quest.LoadBanner();
		}
	}
}
