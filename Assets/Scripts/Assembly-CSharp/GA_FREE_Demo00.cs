using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GA_FREE_Demo00 : MonoBehaviour
{
	private void Awake()
	{
		if (base.enabled)
		{
			GUIAnimSystemFREE.Instance.m_AutoAnimation = false;
		}
	}

	private void Start()
	{
		StartCoroutine(ShowText(1f));
	}

	private void Update()
	{
	}

	private IEnumerator ShowText(float Delay)
	{
		GameObject go = GameObject.Find("Panel (Middle Center)");
		if ((bool)go)
		{
			GUIAnimSystemFREE.Instance.MoveIn(go.transform, true);
		}
		yield return new WaitForSeconds(3f);
		if ((bool)go)
		{
			GUIAnimSystemFREE.Instance.MoveOut(go.transform, true);
		}
		yield return new WaitForSeconds(Delay / 2f);
		SceneManager.LoadScene("GA FREE - Demo01 (960x600px)");
	}
}
