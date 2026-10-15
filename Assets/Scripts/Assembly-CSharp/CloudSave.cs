using System;
using System.Collections.Generic;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CloudSave : MonoBehaviour
{
	public GameObject OffTarget;

	public Texture2D Screenshot;

	public string TempData;

	private List<string> boolDatalist = new List<string>();

	private List<string> intDatalist = new List<string>();

	private List<string> stringDatalist = new List<string>();

	private List<string> floatDatalist = new List<string>();

	private GameManager GM;

	public void Save()
	{
		if (GooglePlayConnection.State == GPConnectionState.STATE_CONNECTED)
		{
			GooglePlaySavedGamesManager.ActionNewGameSaveRequest += ActionNewGameSaveRequest;
			MNP.ShowPreloader("DOKDO", "Saving to Cloud");
			TempData = string.Empty;
			DataToString();
			ActionNewGameSaveRequest();
			return;
		}
		MNPopup mNPopup = new MNPopup("Google Play", "Not Connected");
		mNPopup.AddAction("Ok", () =>
		{
		});
		mNPopup.AddDismissListener(() =>
		{
			Debug.Log("dismiss listener");
		});
		mNPopup.Show();
		Singleton<GooglePlayConnection>.Instance.Connect();
	}

	public void AskSave()
	{
		MNPopup mNPopup = new MNPopup("WARNING", LocalizeManager.Instance.translate("cloud_overwrite"));
		mNPopup.AddAction("No", () =>
		{
			Debug.Log("close");
		});
		mNPopup.AddAction("Yes", () =>
		{
			Save();
		});
		mNPopup.Show();
	}

	public void Load()
	{
		GooglePlaySavedGamesManager.ActionGameSaveLoaded += ActionGameSaveLoaded;
		Singleton<GooglePlaySavedGamesManager>.Instance.ShowSavedGamesUI("Saved Data", 1);
	}

	public void DataToString()
	{
		GM.Save();
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < boolDatalist.Count; i++)
		{
			stringBuilder.Append(boolDatalist[i] + ",");
			stringBuilder.Append(ObscuredPrefs.GetBool(boolDatalist[i], false) + ",");
		}
		stringBuilder.Append("\n");
		for (int j = 0; j < intDatalist.Count; j++)
		{
			stringBuilder.Append(intDatalist[j] + ",");
			stringBuilder.Append(ObscuredPrefs.GetInt(intDatalist[j], 0) + ",");
		}
		stringBuilder.Append("\n");
		for (int k = 0; k < floatDatalist.Count; k++)
		{
			stringBuilder.Append(floatDatalist[k] + ",");
			stringBuilder.Append(ObscuredPrefs.GetFloat(floatDatalist[k], 0f) + ",");
		}
		stringBuilder.Append("\n");
		for (int l = 0; l < stringDatalist.Count; l++)
		{
			stringBuilder.Append(stringDatalist[l] + ",");
			stringBuilder.Append(ObscuredPrefs.GetString(stringDatalist[l]) + ",");
		}
		TempData = stringBuilder.ToString();
	}

	public void StringToData(string _data)
	{
		string[] array = _data.Split('\n');
		string[] array2 = array[0].Split(',');
		string[] array3 = array[1].Split(',');
		string[] array4 = array[2].Split(',');
		string[] array5 = array[3].Split(',');
		int num = array2.Length + array3.Length + array4.Length + array5.Length;
		Debug.Log(num);
		if (num <= 232)
		{
			for (int i = 0; i < boolDatalist.Count; i++)
			{
				if (array2[i].Equals("False"))
				{
					ObscuredPrefs.SetBool(boolDatalist[i], false);
				}
				else
				{
					ObscuredPrefs.SetBool(boolDatalist[i], true);
				}
			}
			for (int j = 0; j < intDatalist.Count; j++)
			{
				ObscuredPrefs.SetInt(intDatalist[j], int.Parse(array3[j]));
			}
			for (int k = 0; k < floatDatalist.Count; k++)
			{
				ObscuredPrefs.SetFloat(floatDatalist[k], float.Parse(array4[k]));
			}
			for (int l = 0; l < stringDatalist.Count; l++)
			{
				ObscuredPrefs.SetString(stringDatalist[l], array5[l]);
			}
			Debug.Log("구버전 클라우드 데이터");
		}
		else
		{
			for (int m = 0; m < array2.Length - 1; m += 2)
			{
				if (array2[m + 1].Equals("False"))
				{
					ObscuredPrefs.SetBool(array2[m], false);
				}
				else
				{
					ObscuredPrefs.SetBool(array2[m], true);
				}
			}
			for (int n = 0; n < array3.Length - 1; n += 2)
			{
				ObscuredPrefs.SetInt(array3[n], int.Parse(array3[n + 1]));
			}
			for (int num2 = 0; num2 < array4.Length - 1; num2 += 2)
			{
				ObscuredPrefs.SetFloat(array4[num2], float.Parse(array4[num2 + 1]));
			}
			for (int num3 = 0; num3 < array5.Length - 1; num3 += 2)
			{
				ObscuredPrefs.SetString(array5[num3], array5[num3 + 1]);
			}
		}
		GM.Load();
		LocalizeManager.Instance.init();
		Debug.Log("로드 완료");
		TempData = string.Empty;
		MNP.HidePreloader();
		MNP.HidePreloader();
		MNPopup mNPopup = new MNPopup("LOAD", "COMPLETE");
		mNPopup.AddAction("Ok", () =>
		{
			SceneManager.LoadScene("intro");
		});
		mNPopup.AddDismissListener(() =>
		{
			Debug.Log("dismiss listener");
		});
		mNPopup.Show();
	}

	private void ActionNewGameSaveRequest()
	{
		GooglePlaySavedGamesManager.ActionNewGameSaveRequest -= ActionNewGameSaveRequest;
		Debug.Log("New  Game Save Requested, Creating newsave..");
		string message = "DOKDO_BackUP";
		string description = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
		GooglePlaySavedGamesManager.ActionGameSaveResult += ActionGameSaveResult;
		Debug.Log(message);
		Singleton<GooglePlaySavedGamesManager>.Instance.CreateNewSnapshot(message, description, Screenshot, TempData, 0L);
		TempData = string.Empty;
	}

	private void ActionGameSaveLoaded(GP_SpanshotLoadResult result)
	{
		Debug.Log("ActionGameSaveLoaded: " + result.Message);
		GooglePlaySavedGamesManager.ActionGameSaveLoaded -= ActionGameSaveLoaded;
		if (result.IsSucceeded)
		{
			StringToData(result.Snapshot.stringData);
			return;
		}
		MNP.HidePreloader();
		MNPopup mNPopup = new MNPopup("LOAD", "Failed");
		mNPopup.AddAction("Ok", () =>
		{
			Debug.Log("Ok action callback");
		});
		mNPopup.AddDismissListener(() =>
		{
			Debug.Log("dismiss listener");
		});
		mNPopup.Show();
	}

	private void ActionGameSaveResult(GP_SpanshotLoadResult result)
	{
		GooglePlaySavedGamesManager.ActionGameSaveResult -= ActionGameSaveResult;
		Debug.Log("ActionGameSaveResult:" + result.Message);
		MNP.HidePreloader();
		if (result.IsSucceeded)
		{
			Debug.Log("Games Saved: " + result.Snapshot.meta.Title);
			MNPopup mNPopup = new MNPopup("SAVE", "COMPLETE");
			mNPopup.AddAction("Ok", () =>
			{
				Debug.Log("Ok action callback");
			});
			mNPopup.AddDismissListener(() =>
			{
				Debug.Log("dismiss listener");
			});
			mNPopup.Show();
		}
		else
		{
			Debug.Log("Games Save Failed");
			MNPopup mNPopup2 = new MNPopup("SAVE", "Failed");
			mNPopup2.AddAction("Ok", () =>
			{
				Debug.Log("Ok action callback");
			});
			mNPopup2.AddDismissListener(() =>
			{
				Debug.Log("dismiss listener");
			});
			mNPopup2.Show();
		}
	}

	private void ActionConflict(GP_SnapshotConflict result)
	{
		Debug.Log("Conflict Detected:");
		GP_Snapshot snapshot = result.Snapshot;
		GP_Snapshot conflictingSnapshot = result.ConflictingSnapshot;
		GP_Snapshot snapshot2 = snapshot;
		if (snapshot.meta.LastModifiedTimestamp < conflictingSnapshot.meta.LastModifiedTimestamp)
		{
			snapshot2 = conflictingSnapshot;
		}
		result.Resolve(snapshot2);
	}

	private void Awake()
	{
		GM = GameManager.Instance;
		if (!Singleton<GooglePlayConnection>.Instance.IsConnected)
		{
			Singleton<GooglePlayConnection>.Instance.Connect();
		}
		boolDatalist.Clear();
		intDatalist.Clear();
		stringDatalist.Clear();
		floatDatalist.Clear();
		boolDatalist.Add("pickboat");
		boolDatalist.Add("getHidden");
		boolDatalist.Add("completeTuto");
		boolDatalist.Add("noAds");
		boolDatalist.Add("allIsland");
		for (int i = 0; i < GM.IslandDone.Length; i++)
		{
			boolDatalist.Add("islandDone" + i);
		}
		intDatalist.Add("Level_Body");
		intDatalist.Add("Level_Cannon");
		intDatalist.Add("Level_Fishing");
		intDatalist.Add("Level_Sail");
		intDatalist.Add("Level_Steering");
		intDatalist.Add("coin");
		intDatalist.Add("fish");
		intDatalist.Add("wood");
		intDatalist.Add("iron");
		intDatalist.Add("silver");
		intDatalist.Add("gold");
		intDatalist.Add("diamond");
		intDatalist.Add("LastIslandNumber");
		intDatalist.Add("TargetIslandNumber");
		intDatalist.Add("MyAmblemN");
		intDatalist.Add("MyCurrnetHP");
		intDatalist.Add("DeadNum");
		intDatalist.Add("Revival");
		intDatalist.Add("day10");
		intDatalist.Add("BestArenaKill1");
		intDatalist.Add("BestColloseoKill");
		intDatalist.Add("CurrentWorld");
		intDatalist.Add("WorldLast1");
		intDatalist.Add("WorldLast2");
		for (int j = 0; j < GM.IslandGoods.Length; j++)
		{
			for (int k = 0; k < GM.IslandGoods[j].goods.Length; k++)
			{
				intDatalist.Add("IslandGoods" + j + "_" + k);
			}
		}
		for (int l = 0; l < GM.AssistantShip_Lv.Length; l++)
		{
			intDatalist.Add("AssistantShip_Lv" + (l + 1));
		}
		for (int m = 0; m < GM.Seagull_Lv.Length; m++)
		{
			intDatalist.Add("Seagull_Lv" + (m + 1));
		}
		intDatalist.Add("Nessy_Lv");
		floatDatalist.Add("MarketAdsTime");
		floatDatalist.Add("ResetMarketTime");
		floatDatalist.Add("freeCoinTime");
		stringDatalist.Add("LastTime");
		stringDatalist.Add("lastday");
		stringDatalist.Add("lang");
		stringDatalist.Add("coinDay");
		GooglePlaySavedGamesManager.ActionConflict += ActionConflict;
	}
}
