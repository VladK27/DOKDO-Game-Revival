using UnityEngine;

public class SA_Label : MonoBehaviour
{
	public string text
	{
		get
		{
			if (base.gameObject == null)
			{
				return string.Empty;
			}
			TextMesh componentInChildren = base.gameObject.GetComponentInChildren<TextMesh>();
			if (componentInChildren != null)
			{
				return componentInChildren.text;
			}
			return string.Empty;
		}
		set
		{
			if (base.gameObject == null)
			{
				return;
			}
			TextMesh[] componentsInChildren = base.gameObject.GetComponentsInChildren<TextMesh>();
			TextMesh[] array = componentsInChildren;
			foreach (TextMesh textMesh in array)
			{
				if (textMesh != null)
				{
					textMesh.text = value;
				}
			}
		}
	}
}
