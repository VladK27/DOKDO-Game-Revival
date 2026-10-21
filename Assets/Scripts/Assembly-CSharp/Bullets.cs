using EZObjectPools;
using UnityEngine;

public class Bullets : MonoBehaviour
{
	private Transform myT;

	private bool isdone;

	private EZObjectPool fxpool;

	private EZObjectPool boompool;

	private Rigidbody rigid;

	private MeshRenderer mesh;

	private bool loggedLaunchState;

	private float flightTime;

	private int flightLogStage;

	public bool isEnemy;

	private float localT;

	public int atk;

	public void SetFaction(bool enemy)
	{
		isEnemy = enemy;
		if (enemy)
		{
			if (mesh == null)
			{
				mesh = GetComponent<MeshRenderer>();
			}
			Material material = mesh != null ? mesh.material : null;
			if (material != null)
			{
				if (material.HasProperty("_Color"))
				{
					material.SetColor("_Color", new Color(1f, 0.65f, 0.12f, 1f));
				}
				else if (material.HasProperty("_BaseColor"))
				{
					material.SetColor("_BaseColor", new Color(1f, 0.65f, 0.12f, 1f));
				}
				Debug.Log("[SHOT_DIAG] enemy visual shader=" + material.shader.name + " hasColor=" + material.HasProperty("_Color") + " hasBaseColor=" + material.HasProperty("_BaseColor") + " color=" + material.color);
			}
		}
	}

	private void Awake()
	{
		myT = base.transform;
		fxpool = GameObject.Find("drop Effect").GetComponent<EZObjectPool>();
		boompool = GameObject.Find("boom Effect").GetComponent<EZObjectPool>();
		rigid = GetComponent<Rigidbody>();
		mesh = GetComponent<MeshRenderer>();
	}

	private void OnEnable()
	{
		mesh.enabled = true;
		rigid.useGravity = true;
		isdone = false;
		localT = 0.3f;
		loggedLaunchState = false;
		flightTime = 0f;
		flightLogStage = 0;
	}

	private void Update()
	{
		flightTime += Time.deltaTime;
		if (!loggedLaunchState)
		{
			loggedLaunchState = true;
			Camera camera = Camera.main;
			Vector3 viewport = camera != null ? camera.WorldToViewportPoint(myT.position) : Vector3.zero;
			Material material = mesh != null ? mesh.sharedMaterial : null;
			Debug.Log("[SHOT_DIAG] first-frame object=" + name + " enemy=" + isEnemy + " pos=" + myT.position + " viewport=" + (camera != null ? viewport.ToString() : "NO_CAMERA") + " visibleInViewport=" + (camera != null && viewport.z > 0f && viewport.x >= 0f && viewport.x <= 1f && viewport.y >= 0f && viewport.y <= 1f) + " meshEnabled=" + (mesh != null && mesh.enabled) + " lossyScale=" + myT.lossyScale + " rendererBounds=" + (mesh != null ? mesh.bounds.ToString() : "NO_RENDERER") + " material=" + (material != null ? material.name : "NO_MATERIAL") + " materialColor=" + (material != null ? material.color.ToString() : "n/a") + " texture=" + (material != null && material.mainTexture != null ? material.mainTexture.name : "NONE") + " velocity=" + (rigid != null ? rigid.velocity.ToString() : "NO_RIGIDBODY"));
		}
		if (isEnemy && flightLogStage < 3 && flightTime >= (flightLogStage + 1) * 0.15f)
		{
			flightLogStage++;
			Camera flightCamera = Camera.main;
			Vector3 flightViewport = flightCamera != null ? flightCamera.WorldToViewportPoint(myT.position) : Vector3.zero;
			GameObject playerObject = GameObject.Find("MyPos");
			float playerDistance = playerObject != null ? Vector3.Distance(myT.position, playerObject.transform.position) : -1f;
			Debug.Log("[SHOT_DIAG] enemy flight stage=" + flightLogStage + " object=" + name + " pos=" + myT.position + " velocity=" + (rigid != null ? rigid.velocity.ToString() : "NO_RIGIDBODY") + " forward=" + myT.forward + " viewport=" + flightViewport + " playerDistance=" + playerDistance + " atk=" + atk + " layer=" + gameObject.layer + " collisionMode=" + (rigid != null ? rigid.collisionDetectionMode.ToString() : "n/a"));
		}
		localT -= Time.deltaTime;
		if (myT.position.y <= 0f && !isdone && localT <= 0f)
		{
			isdone = true;
			OnWater();
		}
	}

	private void OnWater()
	{
		Debug.Log("[SHOT_DIAG] water object=" + name + " pos=" + myT.position + " enemy=" + isEnemy);
		Vector3 position = myT.position;
		position.y = 0f;
		rigid.useGravity = false;
		rigid.velocity = Vector3.zero;
		fxpool.TryGetNextObject(position, Quaternion.identity);
		mesh.enabled = false;
		Invoke("setOff", 4f);
	}

	private void setOff()
	{
		base.gameObject.SetActive(false);
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("[SHOT_DIAG] trigger object=" + name + " target=" + other.name + " tag=" + other.tag + " isTrigger=" + other.isTrigger + " enemyBullet=" + isEnemy + " done=" + isdone);
		if (isdone)
		{
			return;
		}
		if (isEnemy)
		{
			ShipController player = other.GetComponentInParent<ShipController>();
			bool playerHull = player != null && other.CompareTag("Player");
			if (!playerHull)
			{
				Debug.Log("[SHOT_DIAG] enemy projectile ignored collider bullet=" + name + " target=" + other.name + " tag=" + other.tag + " isTrigger=" + other.isTrigger + " owner=" + (player != null ? "player" : "other") + " layer=" + other.gameObject.layer + " path=" + GetPath(other.transform));
			}
			else
			{
				isdone = true;
				Debug.Log("[AI_DIAG] enemy bullet hit player bullet=" + name + " collider=" + other.name + " damage=" + atk + " playerHP=" + (GameManager.Instance != null ? GameManager.Instance.MyCurrnetHP.ToString() : "NO_GM") + " pos=" + myT.position);
				Disappear();
				player.Hit(atk);
				Debug.Log("[AI_DIAG] enemy bullet damage applied bullet=" + name + " damage=" + atk + " playerHP=" + (GameManager.Instance != null ? GameManager.Instance.MyCurrnetHP.ToString() : "NO_GM"));
			}
		}
		else if (!isdone)
		{
			Enemy_Ship enemy = other.GetComponentInParent<Enemy_Ship>();
			bool enemyHull = enemy != null && !enemy.isDead && other.CompareTag("Enemy");
			if (enemyHull)
			{
				isdone = true;
				Debug.Log("[SHOT_DIAG] player bullet hit enemy bullet=" + name + " enemy=" + enemy.name + " collider=" + other.name + " damage=" + atk + " hp=" + enemy.CurrentHP + "/" + enemy.HP + " pos=" + myT.position);
				enemy.Hit(atk);
				Disappear();
			}
			else if (other.isTrigger)
			{
				Debug.Log("[SHOT_DIAG] player projectile ignored trigger sensor bullet=" + name + " target=" + other.name + " tag=" + other.tag + " enemyOwner=" + (enemy != null));
			}
		}
	}

	private void Disappear()
	{
		rigid.useGravity = false;
		rigid.velocity = Vector3.zero;
		boompool.TryGetNextObject(myT.position, Quaternion.identity);
		mesh.enabled = false;
		Invoke("setOff", 4f);
	}

	private string GetPath(Transform node)
	{
		string path = node != null ? node.name : "NULL";
		while (node != null && node.parent != null)
		{
			node = node.parent;
			path = node.name + "/" + path;
		}
		return path;
	}
}
