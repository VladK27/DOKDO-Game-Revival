using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLoad : MonoBehaviour
{
	private void Start()
	{
		Invoke("load", 1f);
	}

	private void load()
	{
		SceneManager.LoadScene("Intro");
	}

	private void Update()
	{
	}
}
