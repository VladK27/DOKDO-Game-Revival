using UnityEngine;
using UnityEngine.Events;

public class ShootCheck : MonoBehaviour
{
	private float bulletLocalT;

	public string TargetTag;

	public bool isTarget;

	public UnityEvent shoot;

	private ShipController SC;

	private Enemy_Ship enemyOwner;

	private bool loggedEmptyTargetTag;

	private bool restoredShootListener;

	private void Awake()
	{
		GameObject player = GameObject.Find("MyPos");
		SC = player != null ? player.GetComponent<ShipController>() : null;
		enemyOwner = GetComponentInParent<Enemy_Ship>();
		if (string.IsNullOrEmpty(TargetTag))
		{
			TargetTag = enemyOwner != null ? "Player" : "Enemy";
			Debug.LogWarning("[AI_DIAG] ShootCheck repaired empty TargetTag object=" + name + " path=" + GetPath() + " owner=" + (enemyOwner != null ? enemyOwner.name : "player-or-assistant") + " tag=" + TargetTag);
		}
		if (enemyOwner != null && (shoot == null || shoot.GetPersistentEventCount() == 0))
		{
			if (shoot == null)
			{
				shoot = new UnityEvent();
			}
			bool isLeft = string.Equals(name, "L", System.StringComparison.OrdinalIgnoreCase);
			shoot.AddListener(delegate { enemyOwner.Shoot(isLeft); });
			restoredShootListener = true;
			Debug.LogWarning("[AI_DIAG] ShootCheck restored missing Shoot event object=" + name + " path=" + GetPath() + " side=" + (isLeft ? "left" : "right") + " owner=" + enemyOwner.name);
		}
	}

	private void OnEnable()
	{
		isTarget = false;
	}

	private void Update()
	{
		if (bulletLocalT > 0f)
		{
			bulletLocalT -= Time.deltaTime;
		}
		else if (isTarget && SC != null && !SC.isParking && !SC.isDead)
		{
			bulletLocalT = 3f;
			isTarget = false;
			Debug.Log("[AI_DIAG] ShootCheck firing object=" + name + " path=" + GetPath() + " targetTag=" + TargetTag + " parking=" + SC.isParking + " dead=" + SC.isDead + " restoredListener=" + restoredShootListener + " persistentListeners=" + (shoot != null ? shoot.GetPersistentEventCount() : -1));
			if (shoot != null)
			{
				shoot.Invoke();
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (string.IsNullOrEmpty(TargetTag))
		{
			if (!loggedEmptyTargetTag)
			{
				loggedEmptyTargetTag = true;
				Debug.LogError("[AI_DIAG] ShootCheck still has empty TargetTag object=" + name + " path=" + GetPath() + " collider=" + other.name);
			}
			return;
		}
		if (IsTarget(other))
		{
			if (!isTarget)
			{
				Debug.Log("[AI_DIAG] ShootCheck acquired object=" + name + " targetTag=" + TargetTag + " collider=" + other.name + " colliderTag=" + other.tag + " owner=" + (GetComponentInParent<Enemy_Ship>() != null ? "enemy" : "player-or-assistant"));
			}
			isTarget = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (IsTarget(other))
		{
			isTarget = false;
		}
	}

	private bool IsTarget(Collider other)
	{
		if (other == null || string.IsNullOrEmpty(TargetTag))
		{
			return false;
		}
		if (TargetTag == "Player" && other.GetComponentInParent<ShipController>() != null)
		{
			// MyPos also contains large trigger sensors (for example AtkRange).
			// Only the tagged hull colliders are a valid target, not every child
			// collider below the ShipController.
			return other.CompareTag(TargetTag) && other.GetComponentInParent<Bullets>() == null;
		}
		if (TargetTag == "Enemy")
		{
			Enemy_Ship enemy = other.GetComponentInParent<Enemy_Ship>();
			if (enemy != null && !enemy.isDead && other.CompareTag(TargetTag))
			{
				return true;
			}
		}
		return other.CompareTag(TargetTag);
	}

	private string GetPath()
	{
		string path = name;
		Transform node = transform.parent;
		while (node != null)
		{
			path = node.name + "/" + path;
			node = node.parent;
		}
		return path;
	}
}
