using System.Reflection;
using UnityEngine;

public class DebugVariant : MonoBehaviour
{
	private string variant = "none";
	private float waitTimer;
	private float logTimer;
	private float applyTimer;
	private float directFireTimer;
	private bool directFireLeft;
	private ShipController ship;
	private GUIStyle overlayStyle;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void Create()
	{
		if (FindObjectOfType<DebugVariant>() == null)
		{
			GameObject go = new GameObject("DebugVariant");
			DontDestroyOnLoad(go);
			go.AddComponent<DebugVariant>();
		}
	}

	private void Awake()
	{
		variant = ReadIntent();
		Debug.Log("[VAR] runtime diagnostic loaded variant=" + variant);
	}

	private void Update()
	{
		waitTimer += Time.unscaledDeltaTime;
		applyTimer -= Time.unscaledDeltaTime;
		logTimer -= Time.unscaledDeltaTime;
		if (applyTimer <= 0f)
		{
			applyTimer = 1f;
			ApplyVariant();
		}
		if (logTimer <= 0f && waitTimer >= 5f)
		{
			logTimer = 3f;
			LogState();
		}
	}

	private static string ReadIntent()
	{
#if UNITY_ANDROID && !UNITY_EDITOR
		try
		{
			using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
			using (AndroidJavaObject intent = activity.Call<AndroidJavaObject>("getIntent"))
			{
				string value = intent.Call<string>("getStringExtra", "variant");
				return string.IsNullOrEmpty(value) ? "none" : value;
			}
		}
		catch (System.Exception exception)
		{
			Debug.LogWarning("[VAR] cannot read launch variant: " + exception.Message);
			return "none";
		}
#else
		return "none";
#endif
	}

	private void ApplyVariant()
	{
		if (ship == null)
		{
			GameObject player = GameObject.Find("MyPos");
			if (player != null)
			{
				ship = player.GetComponent<ShipController>();
			}
		}
		if (variant == "unpark" || variant == "all")
		{
			if (ship != null && ship.isParking)
			{
				ship.isParking = false;
				Debug.Log("[VAR] cleared ShipController.isParking for variant=" + variant);
			}
		}
		if (variant == "target" || variant == "enemyFire" || variant == "all")
		{
			Enemy_Ship[] enemies = FindObjectsOfType<Enemy_Ship>();
			for (int i = 0; i < enemies.Length; i++)
			{
				if (!enemies[i].isDead)
				{
					ShootCheck[] checks = enemies[i].GetComponentsInChildren<ShootCheck>(true);
					for (int j = 0; j < checks.Length; j++)
					{
						if (checks[j].gameObject.activeInHierarchy && checks[j].isActiveAndEnabled)
						{
							checks[j].isTarget = true;
							SetPrivateField(checks[j], "bulletLocalT", 0f);
						}
					}
				}
			}
		}
		if (variant == "direct" || variant == "rebuild" || variant == "all")
		{
			directFireTimer -= Time.unscaledDeltaTime;
			if (directFireTimer <= 0f && ship != null && !ship.isParking && !ship.isDead)
			{
				directFireTimer = 3f;
				directFireLeft = !directFireLeft;
				Enemy_Ship[] pirates = FindObjectsOfType<Enemy_Ship>();
				int fired = 0;
				for (int i = 0; i < pirates.Length; i++)
				{
					Enemy_Ship pirate = pirates[i];
					if (!pirate.isPirate || pirate.isDead)
					{
						continue;
					}
					if (variant == "rebuild" && pirate.Cannons != null)
					{
						pirate.Cannons.cannonsL = new Transform[0];
						pirate.Cannons.cannonsR = new Transform[0];
					}
					pirate.Shoot(directFireLeft);
					fired++;
				}
				Debug.Log("[VAR] direct pirate fire variant=" + variant + " side=" + (directFireLeft ? "left" : "right") + " pirates=" + fired);
			}
		}
	}

	private void LogState()
	{
		Enemy_Ship[] enemies = FindObjectsOfType<Enemy_Ship>();
		ShootCheck[] checks = FindObjectsOfType<ShootCheck>();
		int enemyTargets = 0;
		int playerTargets = 0;
		int emptyTags = 0;
		for (int i = 0; i < checks.Length; i++)
		{
			if (checks[i].isTarget)
			{
				if (checks[i].GetComponentInParent<Enemy_Ship>() != null) enemyTargets++;
				else playerTargets++;
			}
			if (string.IsNullOrEmpty(checks[i].TargetTag)) emptyTags++;
		}
		string playerState = ship == null ? "missing" : "parking=" + ship.isParking + " dead=" + ship.isDead + " outing=" + ship.isOuting + " near=" + ship.NearEnemys.Count;
		string closest = "none";
		float minDistance = float.MaxValue;
		for (int i = 0; i < enemies.Length; i++)
		{
			Enemy_Ship enemy = enemies[i];
			float distance = ship != null ? Vector3.Distance(enemy.transform.position, ship.transform.position) : -1f;
			if (distance < minDistance)
			{
				minDistance = distance;
				Rigidbody body = enemy.GetComponent<Rigidbody>();
				closest = enemy.name + " distance=" + distance.ToString("F1") + " lock=" + enemy.LockOn + " pir=" + enemy.isPirate + " dead=" + enemy.isDead + " hp=" + enemy.CurrentHP + "/" + enemy.HP + " velocity=" + (body != null ? body.velocity.ToString() : "NULL") + " kinematic=" + (body != null && body.isKinematic);
			}
		}
		Camera camera = Camera.main;
		string cameraState = camera == null ? "none" : "name=" + camera.name + " fov=" + camera.fieldOfView.ToString("F1") + " orthographic=" + camera.orthographic + " pos=" + camera.transform.position;
		Debug.Log("[VAR] state variant=" + variant + " screen=" + Screen.width + "x" + Screen.height + " aspect=" + Screen.width / (float)Screen.height + " safeArea=" + Screen.safeArea + " camera=" + cameraState + " player=" + playerState + " activeEnemies=" + enemies.Length + " activeShootChecks=" + checks.Length + " enemyShootTargets=" + enemyTargets + " playerShootTargets=" + playerTargets + " emptyTargetTags=" + emptyTags + " closest=" + closest);
	}

	private static void SetPrivateField(object target, string fieldName, object value)
	{
		FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
		if (field != null)
		{
			field.SetValue(target, value);
		}
	}

	private void OnGUI()
	{
		if (variant == "none" || variant == "normal" || waitTimer < 5f)
		{
			return;
		}
		if (overlayStyle == null)
		{
			overlayStyle = new GUIStyle(GUI.skin.label);
			overlayStyle.fontSize = Mathf.Max(18, Screen.width / 42);
			overlayStyle.normal.textColor = Color.white;
		}
		Enemy_Ship[] enemies = FindObjectsOfType<Enemy_Ship>();
		string text = "VARIANT: " + variant + "\n" + (ship == null ? "PLAYER: missing" : "PLAYER: parking=" + ship.isParking + " dead=" + ship.isDead + " outing=" + ship.isOuting + " near=" + ship.NearEnemys.Count) + "\n" + "ACTIVE ENEMIES: " + enemies.Length;
		for (int i = 0; i < enemies.Length && i < 3; i++)
		{
			Rigidbody body = enemies[i].GetComponent<Rigidbody>();
			text += "\n" + enemies[i].name + " HP=" + enemies[i].CurrentHP + "/" + enemies[i].HP + " lock=" + enemies[i].LockOn + " dist=" + (ship != null ? Vector3.Distance(enemies[i].transform.position, ship.transform.position).ToString("F0") : "?") + " speed=" + (body != null ? body.velocity.magnitude.ToString("F1") : "?");
		}
		GUI.Box(new Rect(8f, 8f, Screen.width - 16f, 54f + 34f * Mathf.Min(enemies.Length, 3)), "");
		GUI.Label(new Rect(18f, 14f, Screen.width - 36f, 50f + 34f * Mathf.Min(enemies.Length, 3)), text, overlayStyle);
	}
}
