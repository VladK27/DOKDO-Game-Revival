using UnityEngine;

public class EffectList : MonoBehaviour
{
	public GameObject[] FxList;

	private void ResolveFxList()
	{
		if (FxList != null && FxList.Length >= 3 && FxList[0] != null && FxList[1] != null && FxList[2] != null)
		{
			return;
		}
		GameObject[] resolved = new GameObject[3];
		if (FxList != null)
		{
			for (int i = 0; i < FxList.Length && i < resolved.Length; i++)
			{
				resolved[i] = FxList[i];
			}
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (child.name.StartsWith("burn1"))
			{
				resolved[0] = child.gameObject;
			}
			else if (child.name.StartsWith("burn2"))
			{
				resolved[1] = child.gameObject;
			}
			else if (child.name.StartsWith("burn3"))
			{
				resolved[2] = child.gameObject;
			}
		}
		FxList = resolved;
		Debug.Log("[FIRE_DIAG] EffectList resolved root=" + transform.root.name + " path=" + GetPath() + " fx=" + FxName(0) + "," + FxName(1) + "," + FxName(2));
	}

	private string FxName(int index)
	{
		return FxList[index] == null ? "null" : FxList[index].name;
	}

	private string GetPath()
	{
		string path = transform.name;
		Transform current = transform.parent;
		while (current != null && current != transform.root)
		{
			path = current.name + "/" + path;
			current = current.parent;
		}
		return path;
	}

	private void OnEnable()
	{
		ResolveFxList();
		Debug.Log("[FIRE_DIAG] EffectList OnEnable object=" + name + " root=" + transform.root.name + " id=" + GetInstanceID() + " fxCount=" + (FxList == null ? -1 : FxList.Length));
		for (int i = 0; i < FxList.Length; i++)
		{
			if (FxList[i] == null)
			{
				continue;
			}
			Debug.Log("[FIRE_DIAG] EffectList reset root=" + transform.root.name + " fx=" + i + " name=" + FxList[i].name + " wasActive=" + FxList[i].activeSelf);
			FxList[i].SetActive(false);
		}
	}

	public void init(float p)
	{
		ResolveFxList();
		Debug.Log("[FIRE_DIAG] EffectList init root=" + transform.root.name + " id=" + GetInstanceID() + " ratio=" + p + " fxCount=" + (FxList == null ? -1 : FxList.Length));
		FxList[0].SetActive(p <= 0.7f);
		FxList[1].SetActive(p <= 0.5f);
		FxList[2].SetActive(p <= 0.3f);
		Debug.Log("[FIRE_DIAG] EffectList levels root=" + transform.root.name + " ratio=" + p + " active=" + FxList[0].activeSelf + "," + FxList[1].activeSelf + "," + FxList[2].activeSelf);
	}
}
