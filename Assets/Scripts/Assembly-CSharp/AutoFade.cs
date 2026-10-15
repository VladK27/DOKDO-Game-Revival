using DG.Tweening;
using Percent;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AutoFade : MonoBehaviour
{
	public Image img;

	public Text load;

	public Image img2;

	public GameObject offTarget;

	private void Start()
	{
		img.DOFade(1f, 0f);
		img.DOFade(0f, 1f);
		load.DOFade(0f, 0f);
		if (!GameManager.Instance.noAds)
		{
			CrossPromotion.addShowAdsToQueue();
		}
		CrossPromotion.showPrivacyWindow();
	}

	public void StartGame()
	{
		load.DOFade(1f, 1f);
		img.DOFade(1f, 1f).OnComplete(() =>
		{
			SceneManager.LoadScene("Main");
		});
	}
}
