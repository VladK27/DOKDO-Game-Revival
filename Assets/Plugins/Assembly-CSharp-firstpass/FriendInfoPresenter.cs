using UnityEngine;
using UnityEngine.UI;

public class FriendInfoPresenter : MonoBehaviour
{
	[SerializeField]
	private Text Id;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private Toggle HasIcon;

	[SerializeField]
	private Toggle HasImage;

	[SerializeField]
	private Image Avatar;

	private string playerId = "*****";

	private string playerName = "*****";

	private bool hasIcon;

	private bool hasImage;

	private Sprite avatar;

	private void Start()
	{
		UpdateUi();
	}

	public void SetInfo(string id, string name, bool icon, bool image, Texture2D srcImage)
	{
		playerId = id;
		playerName = name;
		hasIcon = icon;
		hasImage = image;
		if (avatar == null)
		{
			avatar = Sprite.Create(srcImage, new Rect(0f, 0f, srcImage.width, srcImage.height), new Vector2(0.5f, 0.5f));
		}
		UpdateUi();
	}

	private void UpdateUi()
	{
		Id.text = playerId;
		Name.text = playerName;
		HasIcon.isOn = hasIcon;
		HasImage.isOn = hasImage;
		if (avatar != null)
		{
			Avatar.sprite = avatar;
		}
	}
}
