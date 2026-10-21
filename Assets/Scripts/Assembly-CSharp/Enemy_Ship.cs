using System.Collections.Generic;
using DG.Tweening;
using EZObjectPools;
using UnityEngine;

public class Enemy_Ship : MonoBehaviour
{
	private const float DefaultShootPower = 1000f;
	[Header("CurrentData")]
	public int CurrentHP;

	public int Atk;

	public bool isDead;

	public bool LockOn;

	public bool isPirate;

	public bool isArena;

	public IslandChecker IslandCheck;

	public Transform HEAD;

	public int Lv_body;

	public int Lv_Sail;

	public int Lv_Head;

	public int Lv_Cannon;

	[Header("BaseData")]
	public int HP;

	public float AtkRange;

	public float DetactRange;

	public float ShootPower;

	public float motorForce;

	public float steerForce;

	[Header("Other")]
	public Transform tempobj;

	public Transform myShip;

	public Transform boat;

	public CannonParent Cannons;

	public EZObjectPool bulletPool;

	public AudioSource audios;

	public AudioClip boom;

	private float v;

	private float v2;

	private float d;

	private float e;

	private float s;

	private float s0;

	private float h;

	private float Distance;

	private float angle;

	private Transform myT;

	private float addAngle = 45f;

	private EZObjectPool shipboom;

	private EZObjectPool itemPool;

	private ShipController SC;

	private Rigidbody rigid;

	public Transform[] boats;

	public int HomeN;

	private Vector3 homeVec;

	private GameManager GM;

	private GenPirate GP;

	private bool isBack;

	private RigidbodyConstraints FixRigidInit = (RigidbodyConstraints)84;

	private RigidbodyConstraints DieRigid = RigidbodyConstraints.FreezeRotation;

	private float pirateT;

	private Boat_Setting BS;

	private EffectList BurnFx;

	private float angleLocalT;

	private List<Item> thisReward = new List<Item>();

	private float fireDiagTimer;

	private float aiDiagTimer;

	private float nextShotTime;

	private const float EnemyShotInterval = 3f;

	private void Update()
	{
		fireDiagTimer -= Time.deltaTime;
		if (fireDiagTimer <= 0f)
		{
			fireDiagTimer = 5f;
			EffectList[] effectLists = GetComponentsInChildren<EffectList>(true);
			string state = "";
			for (int i = 0; i < effectLists.Length; i++)
			{
				EffectList fx = effectLists[i];
				state += " [" + fx.transform.name + " id=" + fx.GetInstanceID() + " active=" + fx.gameObject.activeInHierarchy + " count=" + (fx.FxList == null ? -1 : fx.FxList.Length);
				if (fx.FxList != null)
				{
					for (int j = 0; j < fx.FxList.Length; j++)
					{
						state += " " + (fx.FxList[j] != null ? fx.FxList[j].name + "=" + fx.FxList[j].activeSelf : "NULL");
					}
				}
				state += "]";
			}
			Debug.Log("[FIRE_DIAG] Enemy state name=" + myT.name + " id=" + GetInstanceID() + " HP=" + CurrentHP + "/" + HP + " isDead=" + isDead + " effects=" + effectLists.Length + state);
		}
		aiDiagTimer -= Time.deltaTime;
		if (aiDiagTimer <= 0f)
		{
			aiDiagTimer = 4f;
			ShootCheck[] checks = GetComponentsInChildren<ShootCheck>(true);
			int activeChecks = 0;
			int targetChecks = 0;
			int emptyTags = 0;
			for (int i = 0; i < checks.Length; i++)
			{
				if (checks[i].isActiveAndEnabled)
				{
					activeChecks++;
					if (checks[i].isTarget)
					{
						targetChecks++;
					}
				}
				if (string.IsNullOrEmpty(checks[i].TargetTag))
				{
					emptyTags++;
				}
			}
			float distance = myShip != null ? Vector3.Distance(myT.position, myShip.position) : -1f;
			Debug.Log("[AI_DIAG] Enemy state name=" + myT.name + " id=" + GetInstanceID() + " pos=" + myT.position + " distance=" + distance + " lockOn=" + LockOn + " detectRange=" + DetactRange + " atkRange=" + AtkRange + " parking=" + (SC != null && SC.isParking) + " playerDead=" + (SC == null || SC.isDead) + " pirate=" + isPirate + " arena=" + isArena + "/" + (SC != null && SC.isArena) + " dead=" + isDead + " hp=" + CurrentHP + "/" + HP + " ai(v,s)=(" + v + "," + s + ") velocity=" + (rigid != null ? rigid.velocity.ToString() : "NULL") + " kinematic=" + (rigid != null && rigid.isKinematic) + " constraints=" + (rigid != null ? rigid.constraints.ToString() : "NULL") + " checks=" + activeChecks + "/" + checks.Length + " targeted=" + targetChecks + " emptyTags=" + emptyTags + " enemyShotsActive=" + (bulletPool != null ? bulletPool.ActiveObjectCount().ToString() : "NULL"));
		}
	}

	private void Start()
	{
	}

	private void Awake()
	{
		GM = GameManager.Instance;
		myT = base.transform;
		if (tempobj == null)
		{
			tempobj = myT.Find("tempObj");
		}
		bulletPool = GameObject.Find("bullet Enemy").GetComponent<EZObjectPool>();
		shipboom = GameObject.Find("ship boom Effect").GetComponent<EZObjectPool>();
		itemPool = GameObject.Find("Floating Objects").GetComponent<EZObjectPool>();
		GP = GameObject.Find("Generators").GetComponent<GenPirate>();
		myShip = GameObject.Find("MyPos").transform;
		SC = myShip.GetComponent<ShipController>();
		rigid = GetComponent<Rigidbody>();
		if (audios == null)
		{
			audios = GetComponent<AudioSource>();
		}
	}

	private void OnEnable()
	{
		Debug.Log("[FIRE_DIAG] Enemy OnEnable name=" + myT.name + " id=" + GetInstanceID() + " HP=" + HP + " CurrentHP=" + CurrentHP + " isDead=" + isDead + " pirate=" + isPirate);
		myT.DOKill();
		myT.localScale = Vector3.one;
		myT.DOScale(Vector3.zero, 0.5f).From().SetEase(Ease.OutBack);
		rigid.constraints = FixRigidInit;
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
		myT.localRotation = Quaternion.identity;
		isArena = SC.isArena;
	}

	private void OnDisable()
	{
		if (SC.NearEnemys.Contains(HEAD))
		{
			SC.NearEnemys.Remove(HEAD);
		}
		if (GP.pirateList.Contains(base.gameObject))
		{
			GP.pirateList.Remove(base.gameObject);
		}
		if (IslandCheck != null && IslandCheck.IslandEnemy.Contains(base.gameObject))
		{
			IslandCheck.IslandEnemy.Remove(base.gameObject);
		}
	}

	public void SetHome(int IslandN)
	{
		HomeN = IslandN;
		homeVec = SC.IslandList[HomeN].position;
	}

	public void init(int tierLevel)
	{
		EnsureShootPower();
		if (AtkRange <= 0f || DetactRange <= 0f)
		{
			Debug.LogWarning("[AI_DIAG] restoring missing enemy combat ranges name=" + myT.name + " AtkRange=" + AtkRange + " DetactRange=" + DetactRange + " sourcePrefabDefaults=15/60");
			if (AtkRange <= 0f)
			{
				AtkRange = 15f;
			}
			if (DetactRange <= 0f)
			{
				DetactRange = 60f;
			}
		}
		int num = 1;
		if (GM.CurrentWorld.Equals(WorldType.Dokdo))
		{
			switch (tierLevel)
			{
			case 0:
				num = 1;
				break;
			case 1:
				num = Random.Range(2, 4);
				break;
			case 2:
				num = Random.Range(2, 5);
				break;
			case 3:
				num = Random.Range(3, 6);
				break;
			case 4:
				num = Random.Range(5, 8);
				break;
			case 5:
				num = Random.Range(6, 10);
				break;
			case 6:
				num = 10;
				break;
			}
		}
		else
		{
			switch (tierLevel)
			{
			case 0:
				num = 5;
				break;
			case 1:
				num = 6;
				break;
			case 2:
				num = 7;
				break;
			case 3:
				num = 8;
				break;
			case 4:
				num = 9;
				break;
			case 5:
				num = 10;
				break;
			case 6:
				num = 10;
				break;
			}
		}
		Lv_body = num;
		Debug.Log("[FIRE_DIAG] Enemy init name=" + myT.name + " id=" + GetInstanceID() + " tierLevel=" + tierLevel + " world=" + GM.CurrentWorld + " Lv_body=" + Lv_body + " pirate=" + isPirate);
		Lv_Cannon = num * 2;
		Lv_Head = num * 5;
		Lv_Sail = num * 5;
		isDead = false;
		LockOn = false;
		nextShotTime = 0f;
		rigid.constraints = FixRigidInit;
		rigid.velocity = Vector3.zero;
		rigid.angularVelocity = Vector3.zero;
		myT.localRotation = Quaternion.identity;
		pirateT = 10f;
		if (!isPirate)
		{
			motorForce = (float)GM.baseSpeed + (float)Lv_Sail * (float)GM.SpeedCoeff;
		}
		else
		{
			motorForce = (float)GM.baseSpeed + (float)Lv_Sail * (float)GM.SpeedCoeff * 3f;
		}
		SetLook();
	}

	public void Hit(int Damage)
	{
		if (!isDead)
		{
			int hpBefore = CurrentHP;
			CurrentHP -= Damage;
			Debug.Log("[FIRE_DIAG] Enemy Hit name=" + myT.name + " id=" + GetInstanceID() + " damage=" + Damage + " HP=" + HP + " current=" + hpBefore + "->" + CurrentHP + " ratio=" + ((float)CurrentHP / HP) + " caller=" + new System.Diagnostics.StackTrace(1, true));
			BurnFx.init((float)CurrentHP / (float)HP);
			if (CurrentHP <= 0)
			{
				isDead = true;
				Dead();
			}
		}
	}

	public void SetLook()
	{
		if (BS == null)
		{
			BS = GetComponentInChildren<Boat_Setting>();
		}
		bool missingBoatReference = boats == null || boats.Length < 10;
		if (!missingBoatReference)
		{
			for (int i = 0; i < 10; i++)
			{
				if (boats[i] == null)
				{
					missingBoatReference = true;
					break;
				}
			}
		}
		if (missingBoatReference)
		{
			boats = new Transform[10];
			for (int i = 0; i < boats.Length; i++)
			{
				Transform bodyLevel = myT.Find("BodySetting/BodyLv_" + (i + 1));
				if (bodyLevel == null)
				{
					continue;
				}
				string boatName = (i < 6) ? "BoatBody" : ((i < 9) ? "body_" + (i + 1) : "dummy");
				Transform[] bodyTransforms = bodyLevel.GetComponentsInChildren<Transform>(true);
				for (int j = 0; j < bodyTransforms.Length; j++)
				{
					if (bodyTransforms[j].name.Equals(boatName))
					{
						boats[i] = bodyTransforms[j];
						break;
					}
				}
			}
		}
		boat = boats[Lv_body - 1];
		BS.init(Lv_Sail - 1, Lv_Head - 1, Lv_body - 1);
		SetCannon();
		BurnFx = GetComponentInChildren<EffectList>();
			Debug.Log("[FIRE_DIAG] Enemy SetLook effects name=" + myT.name + " id=" + GetInstanceID() + " selected=" + (BurnFx == null ? "null" : BurnFx.transform.name + " id=" + BurnFx.GetInstanceID() + " path=" + GetPath(BurnFx.transform) + " count=" + (BurnFx.FxList == null ? -1 : BurnFx.FxList.Length)));
		steerForce = (float)GM.baseSteer + (float)Lv_Head * (float)GM.steerCoeff;
		rigid.mass = (float)GM.baseMass + (float)Lv_body * (float)GM.massCoeff;
		rigid.angularDrag = GM.rigidAngleDrag;
		float num = 1f;
		num = ((!GM.CurrentWorld.Equals(WorldType.Dokdo)) ? 1.5f : 1f);
		HP = (int)(num * (1f * (float)((int)GM.baseHP + (Lv_body - 1) * (int)GM.hpCoeff) + (float)((int)GM.hpCoeff2 * Lv_body * Lv_body)));
		Atk = (int)(1f * (float)((int)GM.baseAtk + Lv_Cannon * (int)GM.AtkCoeff));
		CurrentHP = HP;
		Debug.Log("[FIRE_DIAG] Enemy SetLook name=" + myT.name + " id=" + GetInstanceID() + " world=" + GM.CurrentWorld + " bodyLv=" + Lv_body + " baseHP=" + GM.baseHP + " hpCoeff=" + GM.hpCoeff + " hpCoeff2=" + GM.hpCoeff2 + " worldMultiplier=" + num + " HP=" + HP + " CurrentHP=" + CurrentHP + " isPirate=" + isPirate);
		boat.localRotation = Quaternion.identity;
	}

	private string GetPath(Transform target)
	{
		string path = target.name;
		while (target.parent != null && target.parent != myT)
		{
			target = target.parent;
			path = target.name + "/" + path;
		}
		return path;
	}

	private void Dead()
	{
		if (SC.isArena)
		{
			EventManager.TriggerEvent(MyEvent.ArenaKill);
		}
		if (!isPirate)
		{
			if (IslandCheck != null && IslandCheck.IslandEnemy.Contains(base.gameObject))
			{
				IslandCheck.IslandEnemy.Remove(base.gameObject);
			}
		}
		else if (GP != null && GP.pirateList.Contains(base.gameObject))
		{
			GP.pirateList.Remove(base.gameObject);
		}
		if (!SC.isArena)
		{
			Vector3 position = boat.position;
			position.y = 0f;
			shipboom.TryGetNextObject(position, Quaternion.identity);
			for (int i = 0; i < Random.Range(2, 5); i++)
			{
				GameObject obj = new GameObject();
				itemPool.TryGetNextObject(position, Quaternion.identity, out obj);
				SetRewardItem();
				obj.GetComponent<FloatingObject>().SetItem(thisReward);
			}
		}
		if (SC.NearEnemys.Contains(HEAD))
		{
			SC.NearEnemys.Remove(HEAD);
		}
		rigid.velocity = Vector3.zero;
		rigid.constraints = DieRigid;
		GameManager.Instance.AddStringToMonitor(LocalizeManager.Instance.translate("monitor_enemy_destroyed"));
		Invoke("off", 3f);
	}

	private void off()
	{
		base.gameObject.SetActive(false);
	}

	public void Shoot(bool isLeft)
	{
		if (SC == null || SC.isDead || isDead || SC.isParking)
		{
			return;
		}
		if (Time.time < nextShotTime)
		{
			Debug.Log("[AI_DIAG] Enemy Shoot throttled name=" + myT.name + " side=" + (isLeft ? "left" : "right") + " nextShotIn=" + (nextShotTime - Time.time).ToString("F2"));
			return;
		}
		if (myShip != null)
		{
			float playerLocalX = myT.InverseTransformPoint(myShip.position).x;
			bool playerOnLeft = playerLocalX < 0f;
			if (isLeft != playerOnLeft)
			{
				Debug.Log("[AI_DIAG] Enemy Shoot rejected wrong side name=" + myT.name + " requested=" + (isLeft ? "left" : "right") + " playerLocalX=" + playerLocalX.ToString("F2") + " targetSide=" + (playerOnLeft ? "left" : "right"));
				return;
			}
		}
		nextShotTime = Time.time + EnemyShotInterval;
		EnsureShootPower();
		SetCannon();
		if (Cannons == null || bulletPool == null)
		{
			Debug.LogError("[AI_DIAG] Enemy Shoot cannot fire name=" + myT.name + " cannons=" + (Cannons != null) + " bulletPool=" + (bulletPool != null));
			return;
		}
		Transform[] selectedCannons = isLeft ? Cannons.cannonsL : Cannons.cannonsR;
		if (selectedCannons == null)
		{
			Debug.LogError("[AI_DIAG] Enemy Shoot cannon array missing name=" + myT.name + " side=" + (isLeft ? "left" : "right"));
			return;
		}
		if (selectedCannons.Length == 0)
		{
			Debug.LogError("[AI_DIAG] Enemy Shoot has no cannon transforms name=" + myT.name + " boat=" + (boat != null ? boat.name : "NULL") + " cannonParent=" + GetPath(Cannons.transform) + " side=" + (isLeft ? "left" : "right"));
			return;
		}
		Debug.Log("[AI_DIAG] Enemy Shoot name=" + myT.name + " id=" + GetInstanceID() + " side=" + (isLeft ? "left" : "right") + " cannons=" + selectedCannons.Length + " pool=" + bulletPool.PoolName + " audio=" + (audios != null) + " playerDistance=" + (myShip != null ? Vector3.Distance(myT.position, myShip.position).ToString() : "NULL") + " parking=" + (SC != null && SC.isParking));
		if (audios != null && boom != null)
		{
			audios.PlayOneShot(boom);
		}
		if (isLeft)
		{
			for (int i = 0; i < Cannons.cannonsL.Length; i++)
			{
				if (Cannons.cannonsL[i] == null)
				{
					Debug.LogError("[AI_DIAG] Enemy left cannon transform is null name=" + myT.name + " index=" + i);
					continue;
				}
				GameObject obj = null;
				Quaternion shotRotation = GetEnemyShotRotation(Cannons.cannonsL[i]);
				bool gotBullet = bulletPool != null && bulletPool.TryGetNextObject(Cannons.cannonsL[i].position, shotRotation, out obj);
				Debug.Log("[AI_DIAG] Enemy projectile left=" + i + " got=" + gotBullet + " pos=" + Cannons.cannonsL[i].position + " rot=" + Cannons.cannonsL[i].rotation.eulerAngles);
				if (!gotBullet || obj == null)
				{
					continue;
				}
				Rigidbody projectileBody = obj.GetComponent<Rigidbody>();
				Bullets projectile = obj.GetComponent<Bullets>();
				if (projectileBody == null || projectile == null)
				{
					Debug.LogError("[AI_DIAG] Enemy projectile missing components name=" + myT.name + " rigidbody=" + (projectileBody != null) + " bullets=" + (projectile != null));
					obj.SetActive(false);
					continue;
				}
				projectileBody.velocity = Vector3.zero;
				projectile.SetFaction(true);
				projectile.atk = Atk;
				projectileBody.AddForce(obj.transform.forward * ShootPower);
				LogEnemyProjectile(obj, projectileBody, projectile, isLeft, i);
			}
		}
		else
		{
			for (int j = 0; j < Cannons.cannonsR.Length; j++)
			{
				if (Cannons.cannonsR[j] == null)
				{
					Debug.LogError("[AI_DIAG] Enemy right cannon transform is null name=" + myT.name + " index=" + j);
					continue;
				}
				GameObject obj2 = null;
				Quaternion shotRotation = GetEnemyShotRotation(Cannons.cannonsR[j]);
				bool gotBullet = bulletPool != null && bulletPool.TryGetNextObject(Cannons.cannonsR[j].position, shotRotation, out obj2);
				Debug.Log("[AI_DIAG] Enemy projectile right=" + j + " got=" + gotBullet + " pos=" + Cannons.cannonsR[j].position + " rot=" + Cannons.cannonsR[j].rotation.eulerAngles);
				if (!gotBullet || obj2 == null)
				{
					continue;
				}
				Rigidbody projectileBody = obj2.GetComponent<Rigidbody>();
				Bullets projectile = obj2.GetComponent<Bullets>();
				if (projectileBody == null || projectile == null)
				{
					Debug.LogError("[AI_DIAG] Enemy projectile missing components name=" + myT.name + " rigidbody=" + (projectileBody != null) + " bullets=" + (projectile != null));
					obj2.SetActive(false);
					continue;
				}
				projectileBody.velocity = Vector3.zero;
				projectile.SetFaction(true);
				projectile.atk = Atk;
				projectileBody.AddForce(obj2.transform.forward * ShootPower);
				LogEnemyProjectile(obj2, projectileBody, projectile, isLeft, j);
			}
		}
	}

	private void EnsureShootPower()
	{
		if (ShootPower <= 0f)
		{
			Debug.LogWarning("[AI_DIAG] restoring zero enemy ShootPower name=" + base.name + " value=" + ShootPower + " sourcePrefabDefault=" + DefaultShootPower);
			ShootPower = DefaultShootPower;
		}
	}

	private Quaternion GetEnemyShotRotation(Transform cannon)
	{
		if (cannon == null || myShip == null)
		{
			return cannon != null ? cannon.rotation : Quaternion.identity;
		}

		Vector3 toTarget = myShip.position - cannon.position;
		Vector3 horizontal = Vector3.ProjectOnPlane(toTarget, Vector3.up);
		float distance = horizontal.magnitude;
		if (distance < 0.01f)
		{
			return cannon.rotation;
		}

		float muzzleSpeed = ShootPower * Time.fixedDeltaTime;
		if (bulletPool != null && bulletPool.Template != null)
		{
			Rigidbody templateBody = bulletPool.Template.GetComponent<Rigidbody>();
			if (templateBody != null && templateBody.mass > 0f)
			{
				muzzleSpeed /= templateBody.mass;
			}
		}

		float gravity = Mathf.Abs(Physics.gravity.y);
		float verticalOffset = toTarget.y;
		float speedSquared = muzzleSpeed * muzzleSpeed;
		float discriminant = speedSquared * speedSquared - gravity * (gravity * distance * distance + 2f * verticalOffset * speedSquared);
		float angle = 0f;
		if (gravity > 0.001f && muzzleSpeed > 0.1f)
		{
			angle = discriminant >= 0f
				? Mathf.Atan((speedSquared - Mathf.Sqrt(discriminant)) / (gravity * distance))
				: Mathf.PI * 0.25f;
		}

		float horizontalScale = Mathf.Cos(angle);
		Vector3 direction = horizontal.normalized * horizontalScale + Vector3.up * Mathf.Sin(angle);
		return Quaternion.LookRotation(direction, Vector3.up);
	}

	private void LogEnemyProjectile(GameObject projectileObject, Rigidbody projectileBody, Bullets projectile, bool isLeft, int cannonIndex)
	{
		Vector3 directionToPlayer = myShip != null ? (myShip.position - projectileObject.transform.position).normalized : Vector3.zero;
		Renderer projectileRenderer = projectileObject.GetComponent<Renderer>();
		Debug.Log("[SHOT_DIAG] enemy launch name=" + myT.name + " side=" + (isLeft ? "left" : "right") + " cannon=" + cannonIndex + " pos=" + projectileObject.transform.position + " rotation=" + projectileObject.transform.eulerAngles + " forward=" + projectileObject.transform.forward + " forwardY=" + projectileObject.transform.forward.y.ToString("F3") + " directionToPlayer=" + directionToPlayer + " aimDot=" + Vector3.Dot(projectileObject.transform.forward, directionToPlayer).ToString("F3") + " shootPower=" + ShootPower + " atk=" + projectile.atk + " velocityAfterAddForce=" + projectileBody.velocity + " materialColor=" + (projectileRenderer != null && projectileRenderer.sharedMaterial != null ? projectileRenderer.sharedMaterial.color.ToString() : "NO_MATERIAL"));
	}

	private void SetCannon()
	{
		if (boat != null)
		{
			CannonParent boatCannons = boat.GetComponentInChildren<CannonParent>(true);
			if (boatCannons != null)
			{
				Cannons = boatCannons;
				if (HasMissingCannonPoints(Cannons.cannonsL) || HasMissingCannonPoints(Cannons.cannonsR))
				{
					List<Transform> leftCannons = new List<Transform>();
					List<Transform> rightCannons = new List<Transform>();
					Transform[] children = boat.GetComponentsInChildren<Transform>(true);
					string cannonPointPrefix = Lv_body <= 3 ? "cannon_1" : Lv_body <= 9 ? "cannon_1_side_base" : "cannon_2";
					for (int i = 0; i < children.Length; i++)
					{
						Transform child = children[i];
						if (child.name != cannonPointPrefix && !child.name.StartsWith(cannonPointPrefix + " ("))
						{
							continue;
						}
						float side = boat.InverseTransformPoint(child.position).x;
						if (side < -0.01f)
						{
							leftCannons.Add(child);
						}
						else if (side > 0.01f)
						{
							rightCannons.Add(child);
						}
					}
					leftCannons.Sort((a, b) => boat.InverseTransformPoint(a.position).z.CompareTo(boat.InverseTransformPoint(b.position).z));
					rightCannons.Sort((a, b) => boat.InverseTransformPoint(a.position).z.CompareTo(boat.InverseTransformPoint(b.position).z));
					Cannons.cannonsL = leftCannons.ToArray();
					Cannons.cannonsR = rightCannons.ToArray();
					Debug.LogWarning("[AI_DIAG] rebuilt cannon points from active boat name=" + myT.name + " bodyLevel=" + Lv_body + " prefix=" + cannonPointPrefix + " boat=" + boat.name + " left=" + Cannons.cannonsL.Length + " right=" + Cannons.cannonsR.Length);
				}
				return;
			}
		}
		Cannons = GetComponentInChildren<CannonParent>();
	}

	private bool HasMissingCannonPoints(Transform[] cannonPoints)
	{
		if (cannonPoints == null || cannonPoints.Length == 0)
		{
			return true;
		}
		for (int i = 0; i < cannonPoints.Length; i++)
		{
			if (cannonPoints[i] == null)
			{
				return true;
			}
		}
		return false;
	}

	private void FixedUpdate()
	{
		Distance = Vector3.Distance(myT.position, myShip.position);
		if (!SC.isDead)
		{
			if (isPirate)
			{
				if (SC.isArena)
				{
					LockOn = true;
				}
				else
				{
					LockOn = Distance < DetactRange + 50f;
				}
			}
			else
			{
				LockOn = Distance < DetactRange;
			}
		}
		else
		{
			LockOn = false;
		}
		if (!SC.isParking)
		{
			if (SC.isArena)
			{
				if (!isArena)
				{
					base.gameObject.SetActive(false);
				}
			}
			else if (isArena)
			{
				base.gameObject.SetActive(false);
			}
			if (LockOn)
			{
				if (!isPirate)
				{
					if (Vector3.Distance(myT.position, homeVec) > 60f)
					{
						BackHome();
					}
					else if (!isBack)
					{
						MovetoAtk();
					}
					else
					{
						BackHome();
					}
				}
				else
				{
					MovetoAtk();
					if (Distance > 60f && !isArena)
					{
						pirateT -= Time.deltaTime;
						if (pirateT < 0f)
						{
							base.gameObject.SetActive(false);
						}
						v = 0f;
						s = 0f;
					}
				}
			}
			else if (!isPirate)
			{
				if (Vector3.Distance(myT.position, homeVec) > 30f)
				{
					BackHome();
				}
				else
				{
					v = 0f;
					s = 0f;
				}
			}
			else
			{
				pirateT -= Time.deltaTime;
				if (pirateT < 0f)
				{
					base.gameObject.SetActive(false);
				}
				v = 0f;
				s = 0f;
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		e = Mathf.Lerp(e, (0f - v) * 0.01f, 0.5f * Time.deltaTime);
		v2 = Mathf.Lerp(v2, v, 0.5f * Time.deltaTime);
		d = Mathf.Lerp(d, s0 * 0.5f * (v2 / motorForce), Time.deltaTime);
		if (!isDead)
		{
			rigid.AddTorque(Vector3.up * s * 5f);
			rigid.AddForce(myT.forward * v2 * 10f);
			if (!myT.eulerAngles.x.Equals(0f) || !myT.eulerAngles.z.Equals(0f))
			{
				Vector3 eulerAngles = myT.eulerAngles;
				eulerAngles.x = 0f;
				eulerAngles.z = 0f;
				myT.eulerAngles = eulerAngles;
			}
			if (!rigid.constraints.Equals(FixRigidInit))
			{
				rigid.constraints = FixRigidInit;
				myT.localRotation = Quaternion.identity;
			}
		}
		boat.localRotation = Quaternion.Euler(Mathf.Clamp(e * 0.5f, -5f, 5f), 0f, Mathf.Clamp(d, -10f, 10f));
	}

	private void SetRewardItem()
	{
		thisReward.Clear();
		if (Random.value <= 1f)
		{
			Item item = new Item();
			item.type = itemType.coin;
			item.value = Mathf.CeilToInt(0.3f * (float)(Lv_body * 100 + 50 * Lv_body * Lv_body));
			item.value = (int)item.value + (int)((float)(int)item.value * Random.Range(0f, 0.2f));
			thisReward.Add(item);
		}
		if (Random.value < 0.9f)
		{
			Item item2 = new Item();
			item2.type = itemType.wood;
			item2.value = Mathf.CeilToInt(0.006f * (float)(Lv_body * 100 + 50 * Lv_body * Lv_body));
			item2.value = (int)item2.value + (int)((float)(int)item2.value * Random.Range(0f, 0.2f));
			thisReward.Add(item2);
		}
		if (Random.value < 0.5f)
		{
			Item item3 = new Item();
			item3.type = itemType.iron;
			item3.value = Mathf.CeilToInt(0.003f * (float)(Lv_body * 100 + 50 * Lv_body * Lv_body));
			item3.value = (int)item3.value + (int)((float)(int)item3.value * Random.Range(0f, 0.2f));
			thisReward.Add(item3);
		}
		if (Random.value < 0.02f * (float)Lv_body)
		{
			Item item4 = new Item();
			item4.type = itemType.silver;
			item4.value = Lv_body * 2;
			item4.value = (int)item4.value + (int)((float)(int)item4.value * Random.Range(0f, 0.2f));
			thisReward.Add(item4);
		}
		if (Random.value < 0.01f * (float)Lv_body)
		{
			Item item5 = new Item();
			item5.type = itemType.gold;
			item5.value = Lv_body;
			item5.value = (int)item5.value + (int)((float)(int)item5.value * Random.Range(0f, 0.2f));
			thisReward.Add(item5);
		}
		if (Random.value < 0.001f * (float)Lv_body)
		{
			Item item6 = new Item();
			item6.type = itemType.diamond;
			item6.value = 1;
			item6.value = (int)item6.value + (int)((float)(int)item6.value * Random.Range(0f, 0.2f));
			thisReward.Add(item6);
		}
	}

	private void BackHome()
	{
		if (Vector3.Distance(myT.position, homeVec) < 10f)
		{
			isBack = false;
		}
		else
		{
			isBack = true;
		}
		angle = GetAngle2(myT.position, homeVec) - 90f;
		tempobj.rotation = Quaternion.AngleAxis(angle, Vector3.down);
		h = tempobj.eulerAngles.y - myT.eulerAngles.y;
		if (h > 180f)
		{
			s0 = h - 360f;
		}
		else if (h < -180f)
		{
			s0 = 360f + h;
		}
		else
		{
			s0 = h;
		}
		s = steerForce * 0.03f * s0;
		v = motorForce * 2f;
	}

	private void MovetoAtk()
	{
		pirateT = 5f;
		if (Distance > AtkRange)
		{
			v = motorForce;
			angle = GetAngle2(myT.position, myShip.position) - 90f;
		}
		else
		{
			angle = GetAngle2(myT.position, myShip.position) + addAngle;
			v = motorForce;
		}
		angleLocalT -= Time.deltaTime;
		if (angleLocalT <= 0f)
		{
			angleLocalT = 5f;
			if (Random.value > 0.5f)
			{
				addAngle = 45f;
			}
			else
			{
				addAngle = -225f;
			}
		}
		tempobj.rotation = Quaternion.AngleAxis(angle, Vector3.down);
		h = tempobj.eulerAngles.y - myT.eulerAngles.y;
		if (h > 180f)
		{
			s0 = h - 360f;
		}
		else if (h < -180f)
		{
			s0 = 360f + h;
		}
		else
		{
			s0 = h;
		}
		s = steerForce * 0.01f * s0;
	}

	private float GetAngle(Vector3 vStart, Vector3 vEnd)
	{
		Vector3 vector = vEnd - vStart;
		return Mathf.Atan2(vector.y, vector.x) * 57.29578f;
	}

	private float GetAngle2(Vector3 vStart, Vector3 vEnd)
	{
		Vector3 vector = vEnd - vStart;
		return Mathf.Atan2(vector.z, vector.x) * 57.29578f;
	}
}
