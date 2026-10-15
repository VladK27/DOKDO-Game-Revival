using UnityEngine;

public class CustomPlayerUIRow : MonoBehaviour
{
	public TextMesh playerId;

	public TextMesh playerName;

	public GameObject avatar;

	public TextMesh hasIcon;

	public TextMesh hasImage;

	private void Awake()
	{
		avatar.GetComponent<Renderer>().material = new Material(avatar.GetComponent<Renderer>().material);
	}

	public void Disable()
	{
		hasIcon.text = string.Empty;
		hasImage.text = string.Empty;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		avatar.GetComponent<Renderer>().enabled = false;
	}
}
