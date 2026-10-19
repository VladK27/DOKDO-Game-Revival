using UnityEngine;

public class GetFish : MonoBehaviour
{
	private ShipController SC;

	private void Start()
	{
		SC = GameObject.Find("MyPos").GetComponent<ShipController>();
	}

	// The fishing widget's Animator rotates the fish/hook. If an ancestor has a non-uniform
	// scale, RectTransform.lossyScale becomes NaN and the UI meshes disappear. Keep the
	// immediate parent scale uniform so the animation keeps working and the fish is visible.
	private void LateUpdate()
	{
		Transform p = transform.parent;
		if (p == null)
		{
			return;
		}
		Vector3 s = p.localScale;
		if (!Mathf.Approximately(s.z, s.x) || !Mathf.Approximately(s.z, s.y))
		{
			s.z = s.x;
			s.y = s.x;
			p.localScale = s;
		}
	}

	public void Get()
	{
		SC.GetFish();
	}
}
