using UnityEngine;
using UnityEngine.UI;

// Revival build: reward-video ("watch ad") handling removed.
// Kept as a no-op so the scene button references stay valid.
public class SendAdsEvent : MonoBehaviour
{
	public RewardType Type;
	public bool IsRepairStatus { get; private set; }

	private void Awake()
	{
		ApplyAdRemoval();
	}

	private void OnEnable()
	{
		// AD REMOVAL: Parent panels can reactivate ad-only controls after Awake has run.
		ApplyAdRemoval();
	}

	private void ApplyAdRemoval()
	{
		Image[] images = GetComponentsInChildren<Image>(true);
		bool hasRepairVideoShortcut = false;
		for (int i = 0; i < images.Length; i++)
		{
			if (images[i].sprite != null && images[i].sprite.name == "repair_btn_film")
			{
				// AD REMOVAL: Keep the repair status and timer, but remove its rewarded-video shortcut.
				images[i].gameObject.SetActive(false);
				hasRepairVideoShortcut = true;
			}
		}

		IsRepairStatus = hasRepairVideoShortcut;
		if (IsRepairStatus)
		{
			Button repairButton = GetComponent<Button>();
			if (repairButton != null)
			{
				repairButton.enabled = false;
			}
			return;
		}

		// AD REMOVAL: Other controls using this component exist only to request an ad.
		base.gameObject.SetActive(false);
	}

	public void Send()
	{
	}
}
