using UnityEngine;
using UnityEngine.UI;

public class MultimediaSceneController : MonoBehaviour
{
	public Image SampleImage;

	public Texture2D SampleTexture;

	private void Start()
	{
		SampleImage.sprite = Sprite.Create(SampleTexture, new Rect(0f, 0f, SampleTexture.width, SampleTexture.height), new Vector2(0.5f, 0.5f));
	}

	public void PickImage()
	{
	}

	public void SaveScreenshot()
	{
	}
}
