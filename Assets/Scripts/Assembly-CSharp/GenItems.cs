using System.Collections;
using EZObjectPools;
using UnityEngine;

public class GenItems : MonoBehaviour
{
	public EZObjectPool itemPool;

	public float r = 15f;

	public float Timer;

	private Vector3 ranpos;

	private Transform myShip;

	private ShipController SC;

	private void Awake()
	{
		GameObject shipObject = GameObject.Find("MyPos");
		if (shipObject == null)
		{
			Debug.LogError("GenItems: ship object 'MyPos' was not found.");
			enabled = false;
			return;
		}
		myShip = shipObject.transform;
		SC = myShip.GetComponent<ShipController>();
		if (SC == null)
		{
			Debug.LogError("GenItems: ShipController was not found on 'MyPos'.");
			enabled = false;
		}
	}

	private void Start()
	{
		StartCoroutine(gening());
	}

	private void Gen()
	{
		do
		{
			ranpos = myShip.position + new Vector3(Random.Range(0f - r, r), 0f, Random.Range(0f - r, r));
		}
		while (Vector3.Distance(myShip.position, ranpos) < r);
		GameObject obj = null;
		ranpos.y = 0f;
		itemPool.TryGetNextObject(ranpos, Quaternion.identity, out obj);
		if (obj != null)
		{
			obj.SetActive(true);
		}
	}

	private IEnumerator gening()
	{
		yield return new WaitForSeconds(Timer);
		while (true)
		{
			if (SC != null && !SC.isParking && !SC.isDead && !SC.isArena && itemPool != null && itemPool.isActiveAndEnabled)
			{
				Gen();
			}
			yield return new WaitForSeconds(Timer);
		}
	}
}
