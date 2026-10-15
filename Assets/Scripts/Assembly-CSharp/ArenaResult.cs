using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SA.Common.Pattern;
using UnityEngine;
using UnityEngine.UI;

public class ArenaResult : MonoBehaviour
{
	public Text myrank;

	public Text myname;

	public Text myScore;

	public Text[] ranks;

	public Text[] names;

	public Text[] scores;

	public Text toppercent;

	public GameObject ranking;

	public GameObject logInPop;

	public Text currentKill;

	public Text bestKill;

	public Text rewardLabel;

	private GameManager GM;

	public GameObject offTarget;

	private ShipController SC;

	public GameObject hud;

	public Image BG;

	public Transform popup;

	public GameObject loading;

	public ArenaDoor AD;

	public GameObject[] title;

	private bool isdone;

	private string rankBoard;

	private int bonus = 1;

	private UM_Score mScore;

	private void Awake()
	{
		GM = GameManager.Instance;
		SC = GameObject.Find("MyPos").GetComponent<ShipController>();
	}

	private void OnDisable()
	{
		UM_GameServiceManager.ActionScoresListLoaded -= Show;
		UM_GameServiceManager.ActionScoresListLoaded -= CallPlayerScore;
		UM_GameServiceManager.ActionScoreSubmitted -= AfterScoreSubmit;
	}

	private void OnEnable()
	{
		BG.DOKill();
		popup.DOKill();
		BG.DOFade(0f, 0f);
		BG.DOFade(0.5f, 2f);
		loading.SetActive(true);
		popup.localScale = Vector3.zero;
		popup.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(2f);
		hud.SetActive(false);
		rankBoard = string.Empty;
		for (int i = 0; i < title.Length; i++)
		{
			title[i].SetActive(false);
		}
		if (GM.CurrentWorld.Equals(WorldType.Dokdo))
		{
			title[0].SetActive(true);
			rankBoard = "dokdo_arena";
			if ((int)GM.CurrentArenaKill > (int)GM.BestArenaKill1)
			{
				GM.BestArenaKill1 = GM.CurrentArenaKill;
			}
			bestKill.text = string.Concat("BEST ", GM.BestArenaKill1, " KILL");
			bonus = 1;
		}
		else
		{
			title[1].SetActive(true);
			rankBoard = "dokdo_Colloseo";
			if ((int)GM.CurrentArenaKill > (int)GM.BestColloseoKill)
			{
				GM.BestColloseoKill = GM.CurrentArenaKill;
			}
			bestKill.text = string.Concat("BEST ", GM.BestColloseoKill, " KILL");
			bonus = 2;
		}
		GM.Save();
		currentKill.text = string.Concat(GM.CurrentArenaKill, " KILL");
		rewardLabel.text = reward().ToString("N0");
		isdone = false;
		if (rankBoard != string.Empty)
		{
			StartCoroutine(load());
		}
	}

	private int reward()
	{
		int num = (int)GM.CurrentArenaKill * ((int)GM.CurrentArenaKill - 2) * 20 + 50;
		if ((int)GM.CurrentArenaKill <= 0)
		{
			num = 0;
		}
		return num * bonus;
	}

	public void GetReward()
	{
		if (!isdone)
		{
			isdone = true;
			GM.AddMoney(reward());
			offTarget.SetActive(false);
			SC.Restart();
		}
	}

	private IEnumerator load()
	{
		ranking.SetActive(false);
		logInPop.SetActive(false);
		if (GM.CurrentWorld.Equals(WorldType.Dokdo))
		{
			rankBoard = "dokdo_arena";
		}
		else
		{
			rankBoard = "dokdo_Colloseo";
		}
		yield return new WaitForSeconds(0.1f);
		if (Singleton<UM_GameServiceManager>.Instance.ConnectionSate == UM_ConnectionState.CONNECTED)
		{
			UM_GameServiceManager.ActionScoreSubmitted += AfterScoreSubmit;
			Singleton<UM_GameServiceManager>.Instance.SubmitScore(rankBoard, (int)GM.CurrentArenaKill, 0L);
		}
		else
		{
			loading.SetActive(false);
			logInPop.SetActive(true);
		}
	}

	private void AfterScoreSubmit(UM_LeaderboardResult res)
	{
		UM_GameServiceManager.ActionScoreSubmitted -= AfterScoreSubmit;
		if (res.IsSucceeded)
		{
			mScore = null;
			UM_GameServiceManager.ActionScoresListLoaded += CallPlayerScore;
			Singleton<UM_GameServiceManager>.Instance.LoadPlayerCenteredScores(rankBoard, 1);
		}
	}

	private void CallPlayerScore(UM_LeaderboardResult res)
	{
		UM_GameServiceManager.ActionScoresListLoaded -= CallPlayerScore;
		if (res.IsSucceeded)
		{
			mScore = res.Leaderboard.GetCurrentPlayerScore(UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL);
			UM_GameServiceManager.ActionScoresListLoaded += Show;
			Singleton<UM_GameServiceManager>.Instance.LoadTopScores(rankBoard, 3);
		}
	}

	public void logIn()
	{
		Singleton<UM_GameServiceManager>.Instance.Connect();
		StartCoroutine(load());
	}

	private void Show(UM_LeaderboardResult res)
	{
		UM_GameServiceManager.ActionScoresListLoaded -= Show;
		if (res.IsSucceeded)
		{
			StartCoroutine(showCo(res));
		}
	}

	private IEnumerator showCo(UM_LeaderboardResult res)
	{
		List<UM_Score> scorelist = new List<UM_Score>();
		for (int i = 1; i < 4; i++)
		{
			if (res.Leaderboard.GetScore(i, UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL) != null)
			{
				scorelist.Add(res.Leaderboard.GetScore(i, UM_TimeSpan.ALL_TIME, UM_CollectionType.GLOBAL));
			}
		}
		yield return scorelist;
		for (int j = 0; j < ranks.Length; j++)
		{
			if (scorelist.Count > j)
			{
				ranks[j].text = scorelist[j].Rank + ".";
				names[j].text = scorelist[j].Player.Name;
				scores[j].text = scorelist[j].LongScore + " KILL";
			}
			else
			{
				ranks[j].text = "-";
				names[j].text = "-";
				scores[j].text = "-";
			}
		}
		if (mScore != null)
		{
			myrank.text = mScore.Rank + ".";
			myname.text = mScore.Player.Name;
			myScore.text = mScore.LongScore + " KILL";
			toppercent.text = string.Empty;
		}
		else
		{
			myrank.text = "-";
			myname.text = "-";
			myScore.text = "-";
			toppercent.text = "-";
		}
		ranking.SetActive(true);
		loading.SetActive(false);
	}

	private void Update()
	{
	}
}
