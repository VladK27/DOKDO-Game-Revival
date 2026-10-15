using UnityEngine;

public class InfiniteWater : MonoBehaviour
{
	public Transform myShip;

	private Transform myT;

	private Vector3 tempPos;

	private void Awake()
	{
		myT = base.transform;
	}

	private void Start()
	{
	}

	public void initPosition(Transform target)
	{
		tempPos = target.position;
		tempPos.y = 0f;
		myT.position = tempPos;
	}

	private void FixedUpdate()
	{
		if (Vector3.Distance(myT.position, myShip.position) > 7f)
		{
			initPosition(myShip);
		}
	}
}
