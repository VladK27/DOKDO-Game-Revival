using System;
using System.Linq;
using CodeStage.AntiCheat.Detectors;
using CodeStage.AntiCheat.ObscuredTypes;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private static int COUNT;

	private int index;

	private Text monitorText;

	[Header("Status")]
	public ObscuredInt MyCurrnetHP;

	public ObscuredInt MyAmblemN;

	public bool isHidden;

	public bool isTuto;

	public bool noAds;

	public ObscuredInt[] AssistantShip_Lv;

	public ObscuredInt[] Seagull_Lv;

	public ObscuredInt Nessy_Lv;

	public WorldType CurrentWorld;

	[Header("Data")]
	public ObscuredInt Revival;

	public ObscuredInt Level_Body = 1;

	public ObscuredInt Level_Sail = 1;

	public ObscuredInt Level_Steering = 1;

	public ObscuredInt Level_Storage = 1;

	public ObscuredInt Level_Cannon = 1;

	public ObscuredInt Level_Fishing = 1;

	public bool isAllIsland;

	public ObscuredInt ReRewardT;

	public ObscuredInt DeadNum;

	public ObscuredInt CurrentTier;

	public ObscuredInt CurrentArenaKill;

	public ObscuredInt BestArenaKill1;

	public ObscuredInt BestColloseoKill;

	[Header("Balance")]
	public ObscuredFloat baseSpeed;

	public ObscuredFloat SpeedCoeff;

	public ObscuredFloat baseSteer;

	public ObscuredFloat steerCoeff;

	public ObscuredFloat baseMass;

	public ObscuredFloat massCoeff;

	public ObscuredFloat rigidAngleDrag;

	public ObscuredInt baseHP;

	public ObscuredInt hpCoeff;

	public ObscuredInt hpCoeff2;

	public ObscuredInt baseAtk;

	public ObscuredInt AtkCoeff;

	public string coinDay;

	[Header("Resources")]
	public Item[] myResource;

	[Header("Other")]
	public Sprite[] ResourceType;

	public AudioSource audios;

	public AudioClip coin1;

	public AudioClip coin2;

	public AudioClip Nocoin;

	public AudioClip pop;

	public AudioClip neg;

	public AudioClip positive;

	public AudioClip positive2;

	public AudioClip positive3;

	public bool FxOn;

	public bool BgmOn;

	public AudioMixer Mixer;

	[Header("Island Stautus")]
	public IslandChecker CurrentIsland;

	public ObscuredInt LastIslandNumber;

	public ObscuredInt WorldLast1;

	public ObscuredInt WorldLast2;

	public ObscuredInt TargetIslandNumber;

	public ObscuredFloat ResetMarketTime;

	public ObscuredFloat ResetMarketPeriod;

	public ObscuredFloat MarketAdsTime;

	public ObscuredFloat freeCoinTime;

	public ObscuredFloat freeCoinResetPeriod;

	public bool[] IslandDone;

	public IslandGood[] IslandGoods;

	public IslandGood[] IslandGoodsDefault;

	public bool isNearIsland;

	public Material[] Sail_Flag_Player;

	public Material[] Sail_Flag_Enemy;

	public prices[] MarketPrice;

	public string[] IslandName;

	public bool isRevived;

	public bool pickboat;

	public ObscuredInt currentday;

	private int[] green0 = new int[13]
	{
		0, 1, 4, 6, 8, 11, 20, 21, 27, 28,
		33, 38, 43
	};

	private int[] blue1 = new int[8] { 2, 9, 18, 24, 29, 34, 39, 44 };

	private int[] desert2 = new int[10] { 3, 7, 12, 13, 19, 26, 30, 35, 40, 45 };

	private int[] ice3 = new int[9] { 15, 16, 22, 23, 25, 31, 36, 41, 46 };

	private int[] pink4 = new int[7] { 5, 10, 14, 17, 32, 37, 42 };

	public ObscuredInt MyHP()
	{
		int num = (int)((float)((int)baseHP + ((int)Level_Body - 1) * (int)hpCoeff + (int)hpCoeff2 * (int)Level_Body * (int)Level_Body) * (1f + (float)(int)Nessy_Lv * 0.02f));
		return num;
	}

	public ObscuredInt MyAtk()
	{
		int num = (int)baseAtk + (int)Level_Cannon * (int)AtkCoeff;
		return num;
	}

	private void Awake()
	{
		index = COUNT;
		COUNT++;
		if (index != 0)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
		Instance = this;
		Screen.sleepTimeout = -1;
		BalData();
		Load();
	}

	private void Start()
	{
		if ((int)MyCurrnetHP < 0)
		{
			MyCurrnetHP = MyHP();
		}
		SpeedHackDetector.StartDetection(OnSpeedHackDetected);
	}

	private void OnSpeedHackDetected()
	{
		MNPopup mNPopup = new MNPopup("SYSTEM", "Cheating Detected");
		mNPopup.AddAction("Ok", () =>
		{
			SceneManager.LoadScene("Intro");
		});
		mNPopup.AddDismissListener(() =>
		{
			Debug.Log("dismiss listener");
		});
		mNPopup.Show();
	}

	public void FreeCoinAds()
	{
		int value = 250 * (int)Level_Body;
		freeCoinTime = freeCoinResetPeriod;
		AddMoney(value);
	}

	public void AdsBoxReward()
	{
		popSound();
		AddMoney(500 * (int)Level_Body);
		UnityEngine.Object.FindObjectOfType<Panel_AskAds>().Close();
	}

	public void AdsMarketReset()
	{
		popSound();
		MarketAdsTime = ResetMarketPeriod;
		ResetMarket();
	}

	public void SetVol()
	{
		if (FxOn)
		{
			Mixer.SetFloat("FxVol", 0f);
		}
		else
		{
			Mixer.SetFloat("FxVol", -80f);
		}
		if (BgmOn)
		{
			Mixer.SetFloat("BgmVol", 0f);
		}
		else
		{
			Mixer.SetFloat("BgmVol", -80f);
		}
	}

	public void TempMuteBGM(bool ison)
	{
		if (ison)
		{
			if (FxOn)
			{
				Mixer.SetFloat("FxVol", 0f);
			}
		}
		else if (FxOn)
		{
			Mixer.SetFloat("FxVol", -80f);
		}
	}

	public void hp30()
	{
		popSound();
		MyCurrnetHP = (int)(0.3f * (float)(int)MyHP());
		EventManager.TriggerEvent(MyEvent.HpUpdate);
	}

	public void AddResource(itemType type, int value)
	{
		Item obj = myResource[(int)type];
		obj.value = (int)obj.value + value;
		EventManager.TriggerEvent(MyEvent.ResourceUpdate);
	}

	public bool isEnoughMoney(int value)
	{
		bool flag = false;
		if ((int)myResource[0].value >= value)
		{
			flag = true;
			Item obj = myResource[0];
			obj.value = (int)obj.value - value;
			audios.PlayOneShot(coin2);
			AddStringToMonitor("구매 성공");
			EventManager.TriggerEvent(MyEvent.ResourceUpdate);
		}
		else
		{
			flag = false;
			EventManager.TriggerEvent(MyEvent.NoMoney);
			AddStringToMonitor("코인이 부족합니다.");
			audios.PlayOneShot(Nocoin);
		}
		return flag;
	}

	public bool isEnoughResource(int[] res)
	{
		bool flag = false;
		bool[] array = new bool[7];
		int num = 0;
		for (int i = 0; i < 7; i++)
		{
			array[i] = (int)myResource[i].value >= res[i];
			if (!array[i])
			{
				num++;
			}
		}
		if (num.Equals(0))
		{
			for (int j = 0; j < 7; j++)
			{
				Item obj = myResource[j];
				obj.value = (int)obj.value - res[j];
			}
			audios.PlayOneShot(coin2);
			AddStringToMonitor("구매 성공");
			EventManager.TriggerEvent(MyEvent.ResourceUpdate);
			flag = true;
		}
		else
		{
			flag = false;
			AddStringToMonitor("자원이 부족합니다.");
			audios.PlayOneShot(Nocoin);
		}
		return flag;
	}

	public void AddMoney(int Value)
	{
		CoinSound();
		Item obj = myResource[0];
		obj.value = (int)obj.value + Value;
		EventManager.TriggerEvent(MyEvent.ResourceUpdate);
	}

	public void CoinSound()
	{
		audios.PlayOneShot(coin1);
	}

	public void AddRevival(int value)
	{
		popSound();
		Revival = (int)Revival + value;
	}

	public void AddHP(int Value)
	{
		MyCurrnetHP = (int)MyCurrnetHP + Value;
		if (Value < 0)
		{
			EventManager.TriggerEvent(MyEvent.Attacked);
		}
		EventManager.TriggerEvent(MyEvent.HpUpdate);
		if ((int)MyCurrnetHP <= 0)
		{
			MyCurrnetHP = 0;
			EventManager.TriggerEvent(MyEvent.Dead);
		}
		if ((int)MyCurrnetHP > (int)MyHP())
		{
			MyCurrnetHP = MyHP();
		}
	}

	public void FullHP()
	{
		popSound();
		MyCurrnetHP = MyHP();
		EventManager.TriggerEvent(MyEvent.HpUpdate);
	}

	public void GetIsland(int Num)
	{
		IslandDone[Num] = true;
		if (TargetIslandNumber.Equals(Num) && (int)TargetIslandNumber < 27)
		{
			TargetIslandNumber = 0;
			while (IslandDone[(int)TargetIslandNumber])
			{
				TargetIslandNumber = (int)TargetIslandNumber + 1;
				if ((int)TargetIslandNumber > 27)
				{
					TargetIslandNumber = 0;
					break;
				}
			}
		}
		AddStringToMonitor(Num + "번 섬을 정복했다");
		Save();
		int num = TotalIsland();
		if (num >= 28)
		{
			Singleton<UM_GameServiceManager>.Instance.UnlockAchievement("dokdo_island28");
		}
		else if (num >= 10)
		{
			Singleton<UM_GameServiceManager>.Instance.UnlockAchievement("dokdo_island10");
		}
		else if (num >= 2)
		{
			Singleton<UM_GameServiceManager>.Instance.UnlockAchievement("dokdo_island2");
		}
		Singleton<UM_GameServiceManager>.Instance.SubmitScore("dokdo_rank", num, 0L);
	}

	public ObscuredInt TotalIsland()
	{
		int num = 0;
		for (int i = 0; i < IslandDone.Length; i++)
		{
			if (IslandDone[i])
			{
				num++;
			}
		}
		return num;
	}

	public ObscuredInt TotalDokdoIsland()
	{
		int num = 0;
		for (int i = 0; i < 28; i++)
		{
			if (IslandDone[i])
			{
				num++;
			}
		}
		return num;
	}

	public void NegSound()
	{
		audios.PlayOneShot(neg);
	}

	public void PosiSound(int n)
	{
		switch (n)
		{
		case 0:
			audios.PlayOneShot(positive);
			break;
		case 1:
			audios.PlayOneShot(positive2);
			break;
		case 2:
			audios.PlayOneShot(positive3);
			break;
		}
	}

	public void popSound()
	{
		audios.PlayOneShot(pop);
	}

	public void AddStringToMonitor(string content)
	{
		if (monitorText == null)
		{
			GameObject monitor = GameObject.Find("monitor");
			if (monitor == null)
			{
				return;
			}
			monitorText = monitor.GetComponent<Text>();
		}
		if (monitorText == null)
		{
			return;
		}
		monitorText.text = content;
		monitorText.enabled = !string.IsNullOrEmpty(content);
		CancelInvoke(nameof(HideMonitor));
		if (monitorText.enabled)
		{
			Invoke(nameof(HideMonitor), 3f);
		}
	}

	private void HideMonitor()
	{
		if (monitorText != null)
		{
			monitorText.enabled = false;
		}
	}

	public ObscuredInt HealPoint()
	{
		int num = 0;
		num = (int)((float)(int)MyHP() / (float)(30 * (int)Level_Body));
		return num;
	}

	private void ResetMarket()
	{
		ResetMarketTime = (float)ResetMarketPeriod * 2f;
		for (int i = 0; i < IslandGoods.Length; i++)
		{
			for (int j = 0; j < 6; j++)
			{
				IslandGoods[i].goods[j].value = IslandGoodsDefault[i].goods[j].value;
			}
		}
		AddStringToMonitor("마켓초기화");
		EventManager.TriggerEvent(MyEvent.ReMarket);
	}

	private void Update()
	{
		if ((float)MarketAdsTime > 0f)
		{
			MarketAdsTime = (float)MarketAdsTime - Time.deltaTime;
		}
		if ((float)ResetMarketTime < 0f)
		{
			ResetMarket();
		}
		else
		{
			ResetMarketTime = (float)ResetMarketTime - Time.deltaTime;
		}
		if ((float)freeCoinTime > 0f)
		{
			freeCoinTime = (float)freeCoinTime - Time.deltaTime;
		}
	}

	[ContextMenu("reset")]
	public void Reset()
	{
		ObscuredPrefs.DeleteAll();
		Debug.Log("Reset");
	}

	public void Save()
	{
		ObscuredPrefs.SetBool("pickboat", pickboat);
		ObscuredPrefs.SetBool("getHidden", isHidden);
		ObscuredPrefs.SetBool("completeTuto", isTuto);
		ObscuredPrefs.SetBool("noAds", noAds);
		ObscuredPrefs.SetBool("allIsland", isAllIsland);
		for (int i = 0; i < IslandDone.Length; i++)
		{
			ObscuredPrefs.SetBool("islandDone" + i, IslandDone[i]);
		}
		ObscuredPrefs.SetInt("Level_Body", Level_Body);
		ObscuredPrefs.SetInt("Level_Cannon", Level_Cannon);
		ObscuredPrefs.SetInt("Level_Fishing", Level_Fishing);
		ObscuredPrefs.SetInt("Level_Sail", Level_Sail);
		ObscuredPrefs.SetInt("Level_Steering", Level_Steering);
		ObscuredPrefs.SetInt("coin", myResource[0].value);
		ObscuredPrefs.SetInt("fish", myResource[1].value);
		ObscuredPrefs.SetInt("wood", myResource[2].value);
		ObscuredPrefs.SetInt("iron", myResource[3].value);
		ObscuredPrefs.SetInt("silver", myResource[4].value);
		ObscuredPrefs.SetInt("gold", myResource[5].value);
		ObscuredPrefs.SetInt("diamond", myResource[6].value);
		ObscuredPrefs.SetInt("LastIslandNumber", LastIslandNumber);
		ObscuredPrefs.SetInt("TargetIslandNumber", TargetIslandNumber);
		ObscuredPrefs.SetInt("MyAmblemN", MyAmblemN);
		ObscuredPrefs.SetInt("MyCurrnetHP", MyCurrnetHP);
		ObscuredPrefs.SetInt("DeadNum", DeadNum);
		ObscuredPrefs.SetInt("Revival", Revival);
		ObscuredPrefs.SetInt("BestArenaKill1", BestArenaKill1);
		ObscuredPrefs.SetInt("BestColloseoKill", BestColloseoKill);
		ObscuredPrefs.SetInt("CurrentWorld", (int)CurrentWorld);
		ObscuredPrefs.SetInt("WorldLast1", WorldLast1);
		ObscuredPrefs.SetInt("WorldLast2", WorldLast2);
		for (int j = 0; j < IslandGoods.Length; j++)
		{
			for (int k = 0; k < IslandGoods[j].goods.Length; k++)
			{
				ObscuredPrefs.SetInt("IslandGoods" + j + "_" + k, IslandGoods[j].goods[k].value);
			}
		}
		for (int l = 0; l < AssistantShip_Lv.Length; l++)
		{
			ObscuredPrefs.SetInt("AssistantShip_Lv" + (l + 1), AssistantShip_Lv[l]);
		}
		for (int m = 0; m < Seagull_Lv.Length; m++)
		{
			ObscuredPrefs.SetInt("Seagull_Lv" + (m + 1), Seagull_Lv[m]);
		}
		ObscuredPrefs.SetInt("Nessy_Lv", Nessy_Lv);
		ObscuredPrefs.SetFloat("MarketAdsTime", MarketAdsTime);
		ObscuredPrefs.SetFloat("ResetMarketTime", ResetMarketTime);
		ObscuredPrefs.SetFloat("freeCoinTime", freeCoinTime);
		string value = UnbiasedTime.Instance.Now().ToString();
		ObscuredPrefs.SetString("LastTime", value);
		ObscuredPrefs.SetString("coinDay", coinDay);
		Debug.Log("세이브");
	}

	private void BalData()
	{
		baseSpeed = 500f;
		SpeedCoeff = 25f;
		baseSteer = 300f;
		steerCoeff = 70f;
		baseMass = 500f;
		massCoeff = 100f;
		rigidAngleDrag = 4f;
		baseHP = 30;
		hpCoeff = 50;
		hpCoeff2 = 10;
		baseAtk = 5;
		AtkCoeff = 2;
		ResetMarketPeriod = 300f;
		freeCoinResetPeriod = 300f;
		ObscuredInt[] p = new ObscuredInt[6] { 6, 36, 110, 480, 2100, 20000 };
		ObscuredInt[] p2 = new ObscuredInt[6] { 5, 44, 128, 480, 2400, 22000 };
		ObscuredInt[] p3 = new ObscuredInt[6] { 5, 40, 120, 410, 2400, 24000 };
		ObscuredInt[] p4 = new ObscuredInt[6] { 5, 40, 105, 440, 2400, 24000 };
		ObscuredInt[] p5 = new ObscuredInt[6] { 4, 40, 120, 480, 2200, 26000 };
		ObscuredInt[] p6 = new ObscuredInt[6] { 4, 46, 120, 410, 2100, 24000 };
		MarketPrice[0].p = p;
		MarketPrice[1].p = p2;
		MarketPrice[2].p = p3;
		MarketPrice[3].p = p4;
		MarketPrice[4].p = p5;
		MarketPrice[5].p = p6;
		IdAndInt[] goods = new IdAndInt[6]
		{
			new IdAndInt(200),
			new IdAndInt(100),
			new IdAndInt(35),
			new IdAndInt(20),
			new IdAndInt(5),
			new IdAndInt(1)
		};
		IdAndInt[] goods2 = new IdAndInt[6]
		{
			new IdAndInt(100),
			new IdAndInt(140),
			new IdAndInt(40),
			new IdAndInt(20),
			new IdAndInt(10),
			new IdAndInt(2)
		};
		IdAndInt[] goods3 = new IdAndInt[6]
		{
			new IdAndInt(100),
			new IdAndInt(150),
			new IdAndInt(35),
			new IdAndInt(15),
			new IdAndInt(10),
			new IdAndInt(2)
		};
		IdAndInt[] goods4 = new IdAndInt[6]
		{
			new IdAndInt(100),
			new IdAndInt(150),
			new IdAndInt(25),
			new IdAndInt(15),
			new IdAndInt(11),
			new IdAndInt(3)
		};
		IdAndInt[] goods5 = new IdAndInt[6]
		{
			new IdAndInt(90),
			new IdAndInt(150),
			new IdAndInt(35),
			new IdAndInt(13),
			new IdAndInt(10),
			new IdAndInt(3)
		};
		IdAndInt[] goods6 = new IdAndInt[6]
		{
			new IdAndInt(90),
			new IdAndInt(120),
			new IdAndInt(35),
			new IdAndInt(10),
			new IdAndInt(5),
			new IdAndInt(3)
		};
		for (int i = 0; i < IslandGoodsDefault.Length; i++)
		{
			if (i <= 3)
			{
				IslandGoodsDefault[i].goods = goods;
			}
			else if (i <= 8)
			{
				IslandGoodsDefault[i].goods = goods2;
			}
			else if (i <= 13)
			{
				IslandGoodsDefault[i].goods = goods3;
			}
			else if (i <= 16)
			{
				IslandGoodsDefault[i].goods = goods4;
			}
			else if (i <= 20)
			{
				IslandGoodsDefault[i].goods = goods5;
			}
			else if (i <= 27)
			{
				IslandGoodsDefault[i].goods = goods6;
			}
			else if (i <= 30)
			{
				IslandGoodsDefault[i].goods = goods;
			}
			else if (i <= 35)
			{
				IslandGoodsDefault[i].goods = goods2;
			}
			else if (i <= 38)
			{
				IslandGoodsDefault[i].goods = goods3;
			}
			else if (i <= 42)
			{
				IslandGoodsDefault[i].goods = goods4;
			}
			else if (i <= 45)
			{
				IslandGoodsDefault[i].goods = goods5;
			}
			else
			{
				IslandGoodsDefault[i].goods = goods6;
			}
		}
		green0 = new int[13]
		{
			0, 1, 4, 6, 8, 11, 20, 21, 27, 28,
			33, 38, 43
		};
		blue1 = new int[8] { 2, 9, 18, 24, 29, 34, 39, 44 };
		desert2 = new int[10] { 3, 7, 12, 13, 19, 26, 30, 35, 40, 45 };
		ice3 = new int[9] { 15, 16, 22, 23, 25, 31, 36, 41, 46 };
		pink4 = new int[7] { 5, 10, 14, 17, 32, 37, 42 };
	}

	public int SetTier(int i)
	{
		int num = 0;
		if (i <= 3)
		{
			return 0;
		}
		if (i <= 8)
		{
			return 1;
		}
		if (i <= 13)
		{
			return 2;
		}
		if (i <= 16)
		{
			return 3;
		}
		if (i <= 20)
		{
			return 4;
		}
		if (i <= 27)
		{
			return 5;
		}
		if (i <= 30)
		{
			return 0;
		}
		if (i <= 35)
		{
			return 1;
		}
		if (i <= 38)
		{
			return 2;
		}
		if (i <= 42)
		{
			return 3;
		}
		if (i <= 45)
		{
			return 4;
		}
		if (i <= 47)
		{
			return 5;
		}
		return 6;
	}

	public void Load()
	{
		pickboat = ObscuredPrefs.GetBool("pickboat", false);
		isHidden = ObscuredPrefs.GetBool("getHidden", false);
		isTuto = ObscuredPrefs.GetBool("completeTuto", false);
		noAds = ObscuredPrefs.GetBool("noAds", false);
		isAllIsland = ObscuredPrefs.GetBool("allIsland", false);
		for (int i = 0; i < IslandDone.Length; i++)
		{
			IslandDone[i] = ObscuredPrefs.GetBool("islandDone" + i, false);
		}
		IslandDone[0] = true;
		IslandDone[28] = true;
		Level_Body = ObscuredPrefs.GetInt("Level_Body", 1);
		Level_Cannon = ObscuredPrefs.GetInt("Level_Cannon", 1);
		Level_Fishing = ObscuredPrefs.GetInt("Level_Fishing", 1);
		Level_Sail = ObscuredPrefs.GetInt("Level_Sail", 1);
		Level_Steering = ObscuredPrefs.GetInt("Level_Steering", 1);
		myResource[0].value = ObscuredPrefs.GetInt("coin", 500);
		myResource[1].value = ObscuredPrefs.GetInt("fish", 0);
		myResource[2].value = ObscuredPrefs.GetInt("wood", 0);
		myResource[3].value = ObscuredPrefs.GetInt("iron", 0);
		myResource[4].value = ObscuredPrefs.GetInt("silver", 0);
		myResource[5].value = ObscuredPrefs.GetInt("gold", 0);
		myResource[6].value = ObscuredPrefs.GetInt("diamond", 0);
		LastIslandNumber = ObscuredPrefs.GetInt("LastIslandNumber", 0);
		TargetIslandNumber = ObscuredPrefs.GetInt("TargetIslandNumber", 1);
		MyAmblemN = ObscuredPrefs.GetInt("MyAmblemN", 0);
		MyCurrnetHP = ObscuredPrefs.GetInt("MyCurrnetHP", 40);
		DeadNum = ObscuredPrefs.GetInt("DeadNum", 0);
		Revival = ObscuredPrefs.GetInt("Revival", 3);
		BestArenaKill1 = ObscuredPrefs.GetInt("BestArenaKill1", 0);
		BestColloseoKill = ObscuredPrefs.GetInt("BestColloseoKill", 0);
		CurrentWorld = (WorldType)ObscuredPrefs.GetInt("CurrentWorld", 0);
		WorldLast1 = ObscuredPrefs.GetInt("WorldLast1", 0);
		WorldLast2 = ObscuredPrefs.GetInt("WorldLast2", 28);
		for (int j = 0; j < IslandGoods.Length; j++)
		{
			for (int k = 0; k < IslandGoods[j].goods.Length; k++)
			{
				IslandGoods[j].goods[k].value = ObscuredPrefs.GetInt("IslandGoods" + j + "_" + k, IslandGoodsDefault[j].goods[k].value);
			}
		}
		for (int l = 0; l < AssistantShip_Lv.Length; l++)
		{
			AssistantShip_Lv[l] = ObscuredPrefs.GetInt("AssistantShip_Lv" + (l + 1), 0);
		}
		for (int m = 0; m < Seagull_Lv.Length; m++)
		{
			Seagull_Lv[m] = ObscuredPrefs.GetInt("Seagull_Lv" + (m + 1), 0);
		}
		Nessy_Lv = ObscuredPrefs.GetInt("Nessy_Lv", 0);
		MarketAdsTime = ObscuredPrefs.GetFloat("MarketAdsTime", 0f);
		ResetMarketTime = ObscuredPrefs.GetFloat("ResetMarketTime", 0f);
		freeCoinTime = ObscuredPrefs.GetFloat("freeCoinTime", 0f);
		coinDay = ObscuredPrefs.GetString("coinDay", "01/01/2017 00:00:00");
		Debug.Log("로드");
	}

	public void ReCalTime()
	{
		DateTime dateTime = Convert.ToDateTime(ObscuredPrefs.GetString("LastTime", UnbiasedTime.Instance.Now().ToString()));
		int num = (int)(UnbiasedTime.Instance.Now() - dateTime).TotalSeconds;
		ReRewardT = 0;
		if (num > 300)
		{
			ReRewardT = Mathf.Clamp(num, 300, 43200);
			EventManager.TriggerEvent(MyEvent.ReReward);
		}
		if (num > 0)
		{
			MarketAdsTime = (float)MarketAdsTime - (float)num;
			ResetMarketTime = (float)ResetMarketTime - (float)num;
			freeCoinTime = (float)freeCoinTime - (float)num;
			if ((float)MarketAdsTime < 0f)
			{
				MarketAdsTime = 0f;
			}
			if ((float)ResetMarketTime < 0f)
			{
				ResetMarketTime = 0f;
			}
			if ((float)freeCoinTime < 0f)
			{
				freeCoinTime = 0f;
			}
		}
	}

	public void ReCalHP()
	{
		DateTime dateTime = Convert.ToDateTime(ObscuredPrefs.GetString("LastTime", UnbiasedTime.Instance.Now().ToString()));
		double totalSeconds = (UnbiasedTime.Instance.Now() - dateTime).TotalSeconds;
		int num = 0;
		num = ((!(totalSeconds > 600.0)) ? ((int)totalSeconds) : 600);
		if (num > 0)
		{
			int value = (int)((float)((int)HealPoint() * num) * 0.5f);
			AddHP(value);
			ObscuredPrefs.SetString("LastTime", UnbiasedTime.Instance.Now().ToString());
		}
	}

	public bool isNewDay()
	{
		string text = UnbiasedTime.Instance.Now().Date.ToString();
		string value = ObscuredPrefs.GetString("lastday", "01/01/2017 00:00:00");
		currentday = ObscuredPrefs.GetInt("day10", 0);
		bool result = false;
		if (isTuto)
		{
			if (!text.Equals(value))
			{
				currentday = (int)currentday + 1;
				if ((int)currentday > 10)
				{
					currentday = 1;
				}
				ObscuredPrefs.SetInt("day10", currentday);
				ObscuredPrefs.SetString("lastday", text);
				result = true;
			}
			else
			{
				result = false;
			}
		}
		return result;
	}

	public void DailyReward(int day)
	{
		PosiSound(1);
		switch (day)
		{
		case 1:
			AddResource(itemType.wood, 30);
			break;
		case 2:
			AddRevival(1);
			break;
		case 3:
			AddResource(itemType.iron, 100);
			break;
		case 4:
			AddRevival(3);
			break;
		case 5:
			AddResource(itemType.silver, 50);
			break;
		case 6:
			AddRevival(5);
			break;
		case 7:
			AddResource(itemType.gold, 20);
			break;
		case 8:
			AddRevival(8);
			break;
		case 9:
			AddResource(itemType.diamond, 5);
			break;
		case 10:
			AddRevival(15);
			break;
		}
	}

	public int AmblemNumber(int islandNum)
	{
		int result = 0;
		if (green0.Contains(islandNum))
		{
			result = 0;
		}
		else if (blue1.Contains(islandNum))
		{
			result = 1;
		}
		else if (desert2.Contains(islandNum))
		{
			result = 2;
		}
		else if (ice3.Contains(islandNum))
		{
			result = 3;
		}
		else if (pink4.Contains(islandNum))
		{
			result = 4;
		}
		else
		{
			Debug.Log("나라 없음");
		}
		return result;
	}
}
