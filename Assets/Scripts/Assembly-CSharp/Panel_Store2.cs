using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Store2 : MonoBehaviour
{
	public GameObject cover;

	public Text remainTime;

	private GameManager GM;

	private DateTime next;

	private TimeSpan t;

	private void Awake()
	{
		GM = GameManager.Instance;
	}

	private void OnEnable()
	{
		GM.popSound();
	}

	private void OnDisable()
	{
		GM.popSound();
	}

	public void Buy_IAP(string productID)
	{
		ThirdPartyManager.instance.BuyProduct(productID);
	}

	private void Update()
	{
		next = Convert.ToDateTime(GM.coinDay);
		t = next - UnbiasedTime.Instance.Now();
		if (t.TotalSeconds > 0.0)
		{
			cover.SetActive(true);
			remainTime.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", t.Days, t.Hours, t.Minutes, t.Seconds);
		}
		else
		{
			cover.SetActive(false);
		}
	}
}
