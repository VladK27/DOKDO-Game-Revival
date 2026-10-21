using UnityEngine;

public class DetactEnemy : MonoBehaviour
{
	public string Tag;

	private ShipController SC;

	public bool isEnemy;

	private void OnEnable()
	{
		if (!isEnemy)
		{
			SC = GetComponentInParent<ShipController>();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (!isEnemy && SC != null)
		{
			Enemy_Ship enemy = other.GetComponentInParent<Enemy_Ship>();
			bool isEnemyTarget = enemy != null && (Tag == "Enemy" || (!string.IsNullOrEmpty(Tag) && other.CompareTag(Tag)));
			if (isEnemyTarget && !enemy.isDead && enemy.HEAD != null && !SC.NearEnemys.Contains(enemy.HEAD))
			{
				SC.NearEnemys.Add(enemy.HEAD);
				Debug.Log("[AI_DIAG] player detected enemy=" + enemy.name + " collider=" + other.name + " distance=" + Vector3.Distance(transform.position, enemy.transform.position));
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!isEnemy && SC != null)
		{
			Enemy_Ship enemy = other.GetComponentInParent<Enemy_Ship>();
			bool isEnemyTarget = enemy != null && (Tag == "Enemy" || (!string.IsNullOrEmpty(Tag) && other.CompareTag(Tag)));
			if (isEnemyTarget && enemy.HEAD != null && SC.NearEnemys.Contains(enemy.HEAD))
			{
				SC.NearEnemys.Remove(enemy.HEAD);
			}
		}
	}
}
