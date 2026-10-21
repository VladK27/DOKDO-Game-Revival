using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Panel_AskAds : MonoBehaviour
{
	public Image BG;

	public Transform popup;

	public GameObject Parent;

	public Text coinLabel;

	public void Show()
	{
		// AD REMOVAL: Keep legacy scene callbacks safe without displaying or pausing for an ad offer.
		Time.timeScale = 1f;
		if (Parent != null)
		{
			Parent.SetActive(false);
		}
	}

	public void Close()
	{
		Time.timeScale = 1f;
		popup.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
		BG.DOFade(0f, 0.5f).OnComplete(() =>
		{
			Parent.SetActive(false);
		});
	}
}
