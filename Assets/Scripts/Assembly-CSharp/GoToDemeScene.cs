using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToDemeScene : MonoBehaviour
{
	private void Start()
	{
		SceneManager.LoadScene("DemoScene");
	}
}
