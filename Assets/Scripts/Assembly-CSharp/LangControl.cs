using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LangControl : MonoBehaviour
{
	public GameObject list;

	public Text currentLang;

	public void ChangeLang(string lang)
	{
		Debug.Log("[LOCALE] ChangeLang requested=" + lang + " manager=" + (LocalizeManager.Instance != null));
		if (LocalizeManager.Instance == null)
		{
			return;
		}
		LocalizeManager.Instance.Language = lang;
		LocalizeManager.Instance.SaveLang();
		init();
		Debug.Log("[LOCALE] ChangeLang applied=" + LocalizeManager.Instance.Language + " index=" + LocalizeManager.Instance.currentLangNum);
		SceneManager.LoadScene("Intro");
	}

	private void OnEnable()
	{
		init();
	}

	private void init()
	{
		switch (LocalizeManager.Instance.Language)
		{
		case "English":
			currentLang.text = "ENGLISH";
			break;
		case "Korean":
			currentLang.text = "한국어";
			break;
		case "ChineseTraditional":
			currentLang.text = "Chinese(繁體)";
			break;
		case "Japanese":
			currentLang.text = "日本語";
			break;
		case "German":
			currentLang.text = "German";
			break;
		case "Russian":
			currentLang.text = "Russian";
			break;
		}
		list.SetActive(false);
	}
}
