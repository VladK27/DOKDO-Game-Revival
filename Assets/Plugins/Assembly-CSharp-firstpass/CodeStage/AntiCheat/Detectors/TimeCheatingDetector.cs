using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CodeStage.AntiCheat.Detectors
{
	[AddComponentMenu("Code Stage/Anti-Cheat Toolkit/Time Cheating Detector")]
	public class TimeCheatingDetector : ActDetectorBase
	{
		internal const string COMPONENT_NAME = "Time Cheating Detector";

		private const string FINAL_LOG_PREFIX = "[ACTk] Time Cheating Detector: ";

		private const string TIME_SERVER = "pool.ntp.org";

		private const int NTP_DATA_BUFFER_LENGTH = 48;

		private static int instancesInScene;

		private readonly DateTime date1900 = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		[Tooltip("Time (in minutes) between detector checks.")]
		[Range(1f, 60f)]
		public int interval = 1;

		[Tooltip("Maximum allowed difference between online and offline time, in minutes.")]
		public int threshold = 65;

		private Socket asyncSocket;

		private Action<double> getOnlineTimeCallback;

		private string targetHost;

		private byte[] targetIP;

		private IPEndPoint targetEndpoint;

		private byte[] ntpData = new byte[48];

		private SocketAsyncEventArgs connectArgs;

		private SocketAsyncEventArgs sendArgs;

		private SocketAsyncEventArgs receiveArgs;

		public static TimeCheatingDetector Instance { get; private set; }

		private static TimeCheatingDetector GetOrCreateInstance
		{
			get
			{
				if (Instance != null)
				{
					return Instance;
				}
				if (ActDetectorBase.detectorsContainer == null)
				{
					ActDetectorBase.detectorsContainer = new GameObject("Anti-Cheat Toolkit Detectors");
				}
				Instance = ActDetectorBase.detectorsContainer.AddComponent<TimeCheatingDetector>();
				return Instance;
			}
		}

		private TimeCheatingDetector()
		{
		}

		public static void StartDetection()
		{
			if (Instance != null)
			{
				Instance.StartDetectionInternal(null, Instance.interval);
			}
			else
			{
				Debug.LogError("[ACTk] Time Cheating Detector: can't be started since it doesn't exists in scene or not yet initialized!");
			}
		}

		public static void StartDetection(UnityAction callback)
		{
			StartDetection(callback, GetOrCreateInstance.interval);
		}

		public static void StartDetection(UnityAction callback, int interval)
		{
			GetOrCreateInstance.StartDetectionInternal(callback, interval);
		}

		public static void StopDetection()
		{
			if (Instance != null)
			{
				Instance.StopDetectionInternal();
			}
		}

		public static void Dispose()
		{
			if (Instance != null)
			{
				Instance.DisposeInternal();
			}
		}

		private void Awake()
		{
			instancesInScene++;
			if (Init(Instance, "Time Cheating Detector"))
			{
				Instance = this;
			}
			SceneManager.sceneLoaded += OnLevelWasLoadedNew;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			instancesInScene--;
		}

		private void OnLevelWasLoadedNew(Scene scene, LoadSceneMode mode)
		{
			OnLevelLoadedCallback();
		}

		private void OnLevelLoadedCallback()
		{
			if (instancesInScene < 2)
			{
				if (!keepAlive)
				{
					DisposeInternal();
				}
			}
			else if (!keepAlive && Instance != this)
			{
				DisposeInternal();
			}
		}

		private void StartDetectionInternal(UnityAction callback, int checkInterval)
		{
			if (isRunning)
			{
				Debug.LogWarning("[ACTk] Time Cheating Detector: already running!", this);
				return;
			}
			if (!base.enabled)
			{
				Debug.LogWarning("[ACTk] Time Cheating Detector: disabled but StartDetection still called from somewhere (see stack trace for this message)!", this);
				return;
			}
			if (callback != null && detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Time Cheating Detector: has properly configured Detection Event in the inspector, but still get started with Action callback. Both Action and Detection Event will be called on detection. Are you sure you wish to do this?", this);
			}
			if (callback == null && !detectionEventHasListener)
			{
				Debug.LogWarning("[ACTk] Time Cheating Detector: was started without any callbacks. Please configure Detection Event in the inspector, or pass the callback Action to the StartDetection method.", this);
				base.enabled = false;
				return;
			}
			detectionAction = callback;
			interval = checkInterval;
			InvokeRepeating("CheckForCheat", 0f, interval * 60);
			started = true;
			isRunning = true;
		}

		protected override void StartDetectionAutomatically()
		{
			StartDetectionInternal(null, interval);
		}

		protected override void PauseDetector()
		{
			isRunning = false;
		}

		protected override void ResumeDetector()
		{
			if (detectionAction != null || detectionEventHasListener)
			{
				isRunning = true;
			}
		}

		protected override void StopDetectionInternal()
		{
			if (started)
			{
				CancelInvoke("CheckForCheat");
				detectionAction = null;
				started = false;
				isRunning = false;
			}
		}

		protected override void DisposeInternal()
		{
			base.DisposeInternal();
			if (Instance == this)
			{
				Instance = null;
			}
			if (asyncSocket.Connected)
			{
				asyncSocket.Close();
			}
		}

		private void CheckForCheat()
		{
			if (isRunning)
			{
				GetOnlineTimeAsync("pool.ntp.org", OnTimeGot);
			}
		}

		private void OnTimeGot(double onlineTime)
		{
			if (onlineTime <= 0.0)
			{
				Debug.LogWarning("[ACTk] Time Cheating Detector: Can't retrieve time from time server!");
				return;
			}
			double localTime = GetLocalTime();
			TimeSpan timeSpan = new TimeSpan((long)onlineTime * 10000);
			TimeSpan timeSpan2 = new TimeSpan((long)localTime * 10000);
			double value = timeSpan.TotalMinutes - timeSpan2.TotalMinutes;
			if (Math.Abs(value) > (double)threshold)
			{
				OnCheatingDetected();
			}
		}

		public static double GetOnlineTime(string server)
		{
			try
			{
				byte[] array = new byte[48];
				array[0] = 27;
				IPAddress[] addressList = Dns.GetHostEntry(server).AddressList;
				Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				socket.Connect(new IPEndPoint(addressList[0], 123));
				socket.ReceiveTimeout = 3000;
				socket.Send(array);
				socket.Receive(array);
				socket.Close();
				ulong num = ((ulong)array[40] << 24) | ((ulong)array[41] << 16) | ((ulong)array[42] << 8) | array[43];
				ulong num2 = ((ulong)array[44] << 24) | ((ulong)array[45] << 16) | ((ulong)array[46] << 8) | array[47];
				return (double)num * 1000.0 + (double)num2 * 1000.0 / 4294967296.0;
			}
			catch (Exception ex)
			{
				Debug.Log("[ACTk] Time Cheating Detector: Could not get NTP time from " + server + " =/\n" + ex);
				return -1.0;
			}
		}

		public void GetOnlineTimeAsync(string server, Action<double> callback)
		{
			try
			{
				IPAddress[] addressList = Dns.GetHostEntry(server).AddressList;
				if (addressList.Length == 0)
				{
					Debug.Log("[ACTk] Time Cheating Detector: Could not resolve IP from the host " + server + " =/");
					callback(-1.0);
					return;
				}
				if (asyncSocket == null)
				{
					asyncSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				}
				targetHost = server;
				IPAddress iPAddress = addressList[0];
				byte[] addressBytes = iPAddress.GetAddressBytes();
				if (addressBytes != targetIP)
				{
					targetEndpoint = new IPEndPoint(iPAddress, 123);
					targetIP = addressBytes;
				}
				if (connectArgs == null)
				{
					connectArgs = new SocketAsyncEventArgs();
					connectArgs.Completed += OnSockedConnected;
				}
				connectArgs.RemoteEndPoint = targetEndpoint;
				asyncSocket.ReceiveTimeout = 3000;
				getOnlineTimeCallback = callback;
				asyncSocket.ConnectAsync(connectArgs);
			}
			catch (Exception ex)
			{
				Debug.Log("[ACTk] Time Cheating Detector: Could not get NTP time from " + server + " =/\n" + ex);
				callback(-1.0);
			}
		}

		private void OnSockedConnected(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError != SocketError.Success)
			{
				Debug.Log("[ACTk] Time Cheating Detector: Could not get NTP time from " + targetHost + " =/\n" + e);
				if (getOnlineTimeCallback != null)
				{
					getOnlineTimeCallback(-1.0);
				}
			}
			else if (isRunning)
			{
				ntpData[0] = 27;
				if (sendArgs == null)
				{
					sendArgs = new SocketAsyncEventArgs();
					sendArgs.Completed += OnSocketSend;
					sendArgs.UserToken = asyncSocket;
					sendArgs.SetBuffer(ntpData, 0, 48);
				}
				sendArgs.RemoteEndPoint = targetEndpoint;
				asyncSocket.SendAsync(sendArgs);
			}
		}

		private void OnSocketSend(object sender, SocketAsyncEventArgs e)
		{
			if (!isRunning)
			{
				return;
			}
			if (e.SocketError == SocketError.Success)
			{
				if (e.LastOperation == SocketAsyncOperation.Send)
				{
					if (receiveArgs == null)
					{
						receiveArgs = new SocketAsyncEventArgs();
						receiveArgs.Completed += OnSocketReceive;
						receiveArgs.UserToken = asyncSocket;
						receiveArgs.SetBuffer(ntpData, 0, 48);
					}
					receiveArgs.RemoteEndPoint = targetEndpoint;
					asyncSocket.ReceiveAsync(receiveArgs);
				}
			}
			else
			{
				Debug.Log("[ACTk] Time Cheating Detector: Could not get NTP time from " + targetHost + " =/\n" + e);
				if (getOnlineTimeCallback != null)
				{
					getOnlineTimeCallback(-1.0);
				}
			}
		}

		private void OnSocketReceive(object sender, SocketAsyncEventArgs e)
		{
			if (isRunning)
			{
				ntpData = e.Buffer;
				ulong num = ((ulong)ntpData[40] << 24) | ((ulong)ntpData[41] << 16) | ((ulong)ntpData[42] << 8) | ntpData[43];
				ulong num2 = ((ulong)ntpData[44] << 24) | ((ulong)ntpData[45] << 16) | ((ulong)ntpData[46] << 8) | ntpData[47];
				double obj = (double)num * 1000.0 + (double)num2 * 1000.0 / 4294967296.0;
				if (getOnlineTimeCallback != null)
				{
					getOnlineTimeCallback(obj);
				}
			}
		}

		private double GetLocalTime()
		{
			return DateTime.UtcNow.Subtract(date1900).TotalMilliseconds;
		}
	}
}
