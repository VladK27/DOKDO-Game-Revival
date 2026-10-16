using UnityEngine;

// ponytail: GUITexture was removed in Unity 2017.1 and GUIText is [Obsolete(error:true)].
// These legacy-GUI types are used only by dead code paths (SA_iTween is unused by the game;
// CFX_Demo_* are Cartoon FX demo scripts). Compat* shims keep that code compiling without
// injecting types into the UnityEngine namespace (which would clash with the real GUIText).
public class CompatGUITexture : MonoBehaviour
{
	public Color color;

	public Texture texture;

	public Rect GetScreenRect(Camera cam)
	{
		return new Rect(0f, 0f, Screen.width, Screen.height);
	}
}

public class CompatGUIText : MonoBehaviour
{
	public Material material;

	public Color color;

	public string text;
}
