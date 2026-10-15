using UnityEngine;
using UnityEngine.UI;

public class LeaderboardInfoPresenter : MonoBehaviour
{
	[SerializeField]
	private Text Rank;

	[SerializeField]
	private Text Score;

	[SerializeField]
	private Text PlayerId;

	[SerializeField]
	private Text Name;

	[SerializeField]
	private Image Avatar;

	private string id = "*****";

	private string playerName = "*****";

	private string rank = "*****";

	private string score = "*****";

	private Sprite avatar;

	private Sprite old;

	private void Start()
	{
		old = Avatar.sprite;
	}

	public void SetInfo(string rank, string score, string id, string name, Texture2D icon)
	{
		this.rank = rank;
		this.score = score;
		playerName = name;
		if (!this.id.Equals(id) && icon != null)
		{
			avatar = Sprite.Create(icon, new Rect(0f, 0f, icon.width, icon.height), new Vector2(0.5f, 0.5f));
		}
		this.id = id;
		UpdateUi();
	}

	public void Disable()
	{
		rank = "*****";
		score = "*****";
		playerName = "*****";
		id = "*****";
		avatar = old;
		UpdateUi();
	}

	private void UpdateUi()
	{
		Rank.text = rank;
		Score.text = score;
		PlayerId.text = id;
		Name.text = playerName;
		if (avatar != null)
		{
			Avatar.sprite = avatar;
		}
		else
		{
			Avatar.sprite = old;
		}
	}
}
