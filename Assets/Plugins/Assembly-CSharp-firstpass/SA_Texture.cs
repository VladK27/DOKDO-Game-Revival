using UnityEngine;

public class SA_Texture : MonoBehaviour
{
	public Texture texture
	{
		get
		{
			return GetComponent<Renderer>().material.mainTexture;
		}
		set
		{
			GetComponent<Renderer>().material.mainTexture = value;
		}
	}

	private void Awake()
	{
		GetComponent<Renderer>().material = new Material(GetComponent<Renderer>().material);
	}
}
