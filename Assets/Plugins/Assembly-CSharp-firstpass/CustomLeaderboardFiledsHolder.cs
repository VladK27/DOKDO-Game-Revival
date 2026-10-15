using UnityEngine;

public class CustomLeaderboardFiledsHolder : MonoBehaviour
{
	public TextMesh rank;

	public TextMesh score;

	public TextMesh playerId;

	public TextMesh playerName;

	public GameObject avatar;

	private void Awake()
	{
		avatar.GetComponent<Renderer>().material = new Material(avatar.GetComponent<Renderer>().material);
	}

	public void Disable()
	{
		rank.text = string.Empty;
		score.text = string.Empty;
		playerId.text = string.Empty;
		playerName.text = string.Empty;
		avatar.GetComponent<Renderer>().enabled = false;
	}
}
