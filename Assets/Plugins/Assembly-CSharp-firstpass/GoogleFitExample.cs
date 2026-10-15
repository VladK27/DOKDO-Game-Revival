using System;
using System.Collections.Generic;
using SA.Common.Models;
using SA.Common.Pattern;
using SA.Fitness;
using UnityEngine;

public class GoogleFitExample : AndroidNativeExampleBase
{
	public GameObject avatar;

	public Texture2D pieIcon;

	public DefaultPreviewButton connectButton;

	public DefaultPreviewButton scoreSubmit;

	public SA_Label playerLabel;

	public DefaultPreviewButton[] ConnectionDependedntButtons;

	public SA_Label a_id;

	public SA_Label a_name;

	public SA_Label a_descr;

	public SA_Label a_type;

	public SA_Label a_state;

	public SA_Label a_steps;

	public SA_Label a_total;

	public SA_Label b_id;

	public SA_Label b_name;

	public SA_Label b_all_time;

	private List<DataSource> dataSources = new List<DataSource>();

	private const string SESSION_ID = "9a4104ae-3e43-stepcounter";

	private void Start()
	{
		playerLabel.text = "Player Disconnected";
		UpdateButtons();
	}

	private void UpdateButtons()
	{
		if (Singleton<Connection>.Instance.ConnectionState == ConnectionState.CONNECTED)
		{
			DefaultPreviewButton[] connectionDependedntButtons = ConnectionDependedntButtons;
			foreach (DefaultPreviewButton defaultPreviewButton in connectionDependedntButtons)
			{
				defaultPreviewButton.EnabledButton();
			}
		}
		else
		{
			DefaultPreviewButton[] connectionDependedntButtons2 = ConnectionDependedntButtons;
			foreach (DefaultPreviewButton defaultPreviewButton2 in connectionDependedntButtons2)
			{
				defaultPreviewButton2.DisabledButton();
			}
		}
	}

	private void ConncetButtonPress()
	{
		Singleton<Connection>.Instance.AddApi(LoginApi.SENSORS_API);
		Singleton<Connection>.Instance.AddApi(LoginApi.RECORDING_API);
		Singleton<Connection>.Instance.AddApi(LoginApi.SESSIONS_API);
		Singleton<Connection>.Instance.AddApi(LoginApi.HISTORY_API);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_LOCATION_READ);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_LOCATION_READ_WRITE);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_ACTIVITY_READ);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_ACTIVITY_READ_WRITE);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_BODY_READ);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_BODY_READ_WRITE);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_NUTRITION_READ);
		Singleton<Connection>.Instance.AddScope(LoginScope.SCOPE_NUTRITION_READ_WRITE);
		Singleton<Connection>.Instance.OnConnectionFinished += SA_Fitness_Connection_Instance_OnConnectionFinished;
		Singleton<Connection>.Instance.Connect();
	}

	private void requestSensors()
	{
		SensorRequest.Builder builder = new SensorRequest.Builder().AddDataType(DataType.TYPE_LOCATION_SAMPLE).AddDataType(DataType.TYPE_LOCATION_TRACK).AddDataType(DataType.TYPE_STEP_COUNT_DELTA)
			.AddDataType(DataType.TYPE_DISTANCE_DELTA)
			.AddDataSourceType(DataSourceType.RAW)
			.AddDataSourceType(DataSourceType.DERIVED);
		SensorRequest sensorRequest = builder.Build();
		sensorRequest.OnRequestFinished += Request_OnRequestFinished;
		Singleton<Sensors>.Instance.RequestSensors(sensorRequest);
	}

	private void registerSensorsListeners()
	{
		int num = 1;
		foreach (DataSource dataSource in dataSources)
		{
			Debug.Log("Data Source #" + num);
			Debug.Log(dataSource.DataSourceType);
			Debug.Log(dataSource.AppPackageName);
			Debug.Log(dataSource.DataType);
			Debug.Log(dataSource.Device);
			Debug.Log(dataSource.Name);
			Debug.Log(dataSource.StreamId);
			Debug.Log(dataSource.StreamName);
			SensorListener.Builder builder = new SensorListener.Builder().SetDataType(dataSource.DataType).SetSamplingRate(5L, TimeUnit.Seconds);
			SensorListener sensorListener = builder.Build();
			sensorListener.OnRegisterSuccess += Listener_OnRegisterSuccess;
			sensorListener.OnRegisterFail += Listener_OnRegisterFail;
			sensorListener.OnDataPointReceived += Listener_OnDataPointReceived;
			Singleton<Sensors>.Instance.RegisterSensorListener(sensorListener);
			num++;
		}
	}

	private void listSubscriptions()
	{
		SubscriptionsRequest.Builder builder = new SubscriptionsRequest.Builder();
		SubscriptionsRequest subscriptionsRequest = builder.Build();
		subscriptionsRequest.OnRequestFinished += Request_OnRequestFinished1;
		Singleton<Recording>.Instance.ListSubscriptions(subscriptionsRequest);
	}

	private void subscribe()
	{
		SubscribeRequest.Builder builder = new SubscribeRequest.Builder();
		builder.SetDataType(DataType.TYPE_STEP_COUNT_DELTA);
		SubscribeRequest subscribeRequest = builder.Build();
		subscribeRequest.OnSubscribeFinished += Request_OnSubscribeFinished;
		Singleton<Recording>.Instance.Subscribe(subscribeRequest);
	}

	private void unsubscribe()
	{
		UnsubscribeRequest.Builder builder = new UnsubscribeRequest.Builder();
		builder.SetDataType(DataType.TYPE_STEP_COUNT_DELTA);
		UnsubscribeRequest unsubscribeRequest = builder.Build();
		unsubscribeRequest.OnUnsubscribeFinished += Request_OnUnsubscribeFinished;
		Singleton<Recording>.Instance.Unsubscribe(unsubscribeRequest);
	}

	public void readSession()
	{
		long startTime = (long)DateTime.Now.ToUniversalTime().AddDays(-5.0).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
			.TotalMilliseconds;
		long endTime = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
		ReadSessionRequest.Builder builder = new ReadSessionRequest.Builder();
		builder.SetIdentifier("9a4104ae-3e43-stepcounter");
		builder.SetDataTypeToRead(DataType.TYPE_STEP_COUNT_DELTA);
		builder.SetTimeinterval(startTime, endTime, TimeUnit.Milliseconds);
		ReadSessionRequest readSessionRequest = builder.Build();
		readSessionRequest.OnSessionReadFinished += Request_OnSessionReadFinished;
		Singleton<Sessions>.Instance.ReadSession(readSessionRequest);
	}

	public void insertSession()
	{
		Singleton<Sessions>.Instance.InsertSession();
	}

	public void startSession()
	{
		long startTime = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
		StartSessionRequest.Builder builder = new StartSessionRequest.Builder();
		builder.SetName("session#test");
		builder.SetIdentifier("9a4104ae-3e43-stepcounter");
		builder.SetDescription("Google Fit Session for Android Native");
		builder.SetStartTime(startTime, TimeUnit.Milliseconds);
		builder.SetActivity(Activity.WALKING);
		StartSessionRequest startSessionRequest = builder.Build();
		startSessionRequest.OnSessionStarted += Request_OnSessionStarted;
		Singleton<Sessions>.Instance.StartSession(startSessionRequest);
	}

	public void stopSession()
	{
		StopSessionRequest.Builder builder = new StopSessionRequest.Builder();
		builder.SetIdentifier("9a4104ae-3e43-stepcounter");
		StopSessionRequest stopSessionRequest = builder.Build();
		stopSessionRequest.OnSessionStopped += Request_OnSessionStopped;
		Singleton<Sessions>.Instance.StopSession(stopSessionRequest);
	}

	public void readData()
	{
		long startTime = (long)DateTime.Now.ToUniversalTime().AddDays(-7.0).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
			.TotalMilliseconds;
		long endTime = (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
		ReadHistoryRequest.Builder builder = new ReadHistoryRequest.Builder();
		builder.SetTimeRange(startTime, endTime, TimeUnit.Milliseconds);
		builder.Aggregate(DataType.TYPE_STEP_COUNT_DELTA, DataType.AGGREGATE_STEP_COUNT_DELTA);
		builder.BucketByTime(1, TimeUnit.Days);
		ReadHistoryRequest readHistoryRequest = builder.Build();
		readHistoryRequest.OnReadFinished += Request_OnReadHistoryFinished;
		Singleton<History>.Instance.ReadData(readHistoryRequest);
	}

	public void readDailyTotal()
	{
		ReadDailyTotalRequest.Builder builder = new ReadDailyTotalRequest.Builder();
		builder.SetDataType(DataType.TYPE_STEP_COUNT_DELTA);
		ReadDailyTotalRequest readDailyTotalRequest = builder.Build();
		readDailyTotalRequest.OnRequestFinished += Request_OnReadDailyTotalRequestFinished;
		Singleton<History>.Instance.ReadDailyTotal(readDailyTotalRequest);
	}

	public void insertData()
	{
		Singleton<History>.Instance.InsertData();
	}

	public void updateData()
	{
		Singleton<History>.Instance.UpdateData();
	}

	public void deleteData()
	{
		Singleton<History>.Instance.DeleteData(null);
	}

	private void Request_OnReadHistoryFinished(ReadHistoryResult result)
	{
		Debug.Log("[OnReadHistoryFinished] status: " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		if (result.IsAggregated)
		{
			int num = 1;
			{
				foreach (Bucket bucket in result.Buckets)
				{
					Debug.Log("Bucket #" + num + "\n\t Type: " + bucket.Bucketing.ToString() + "\n\t Start Time: " + bucket.StartTime + "\n\t End Time: " + bucket.EndTime);
					int num2 = 1;
					foreach (DataSet dataSet in bucket.DataSets)
					{
						Debug.Log("Data Set #" + num2 + "\n\t\t Data Type: " + dataSet.DataType.Value);
						Debug.Log("Data Points Count: " + dataSet.DataPoints.Count);
						int num3 = 1;
						foreach (DataPoint dataPoint in dataSet.DataPoints)
						{
							Debug.Log("Data Point #" + num3 + "\n\t\t\t Data Type: " + dataPoint.DataType.Value + "\n\t\t\t Start Time: " + dataPoint.StartTime + "\n\t\t\t End Time: " + dataPoint.EndTime);
							Debug.Log("Fields Count: " + dataPoint.Fields.Count);
							foreach (KeyValuePair<string, object> field in dataPoint.Fields)
							{
								Debug.Log("\t\t\t\t " + field.Key + " : " + field.Value.ToString());
							}
							num3++;
						}
						num2++;
					}
					num++;
				}
				return;
			}
		}
		int num4 = 1;
		foreach (DataSet dataSet2 in result.DataSets)
		{
			Debug.Log("Data Set #" + num4 + "\n\t\t Data Type: " + dataSet2.DataType.Value);
			Debug.Log("Data Points Count: " + dataSet2.DataPoints.Count);
			int num5 = 1;
			foreach (DataPoint dataPoint2 in dataSet2.DataPoints)
			{
				Debug.Log("Data Point #" + num5 + "\n\t\t\t Data Type: " + dataPoint2.DataType.Value + "\n\t\t\t Start Time: " + dataPoint2.StartTime + "\n\t\t\t End Time: " + dataPoint2.EndTime);
				Debug.Log("Fields Count: " + dataPoint2.Fields.Count);
				foreach (KeyValuePair<string, object> field2 in dataPoint2.Fields)
				{
					Debug.Log("\t\t\t\t " + field2.Key + " : " + field2.Value.ToString());
				}
				num5++;
			}
			num4++;
		}
	}

	private void Request_OnReadDailyTotalRequestFinished(ReadDailyTotalResult result)
	{
		Debug.Log("[OnReadDailyTotalRequestFinished] result status: " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		int num = 0;
		foreach (DataPoint dataPoint in result.DataSet.DataPoints)
		{
			Debug.Log("Data Point #" + num + "\n\t Data Type: " + dataPoint.DataType.Value + "\n\t Start Time: " + dataPoint.StartTime + "\n\t End Time: " + dataPoint.EndTime);
			Debug.Log("Fields Count: " + dataPoint.Fields.Count);
			foreach (KeyValuePair<string, object> field in dataPoint.Fields)
			{
				Debug.Log("\t\t\t\t " + field.Key + " : " + field.Value.ToString());
			}
			num++;
		}
	}

	private void Request_OnSessionReadFinished(ReadSessionResult result)
	{
		Debug.Log("[Request_OnSessionReadFinished] result status: " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		Debug.Log("Sessions Count: " + result.Sessions.Count);
		int num = 1;
		foreach (Session session in result.Sessions)
		{
			Debug.Log("Session #" + num + "\n\t Id: " + session.Id + "\n\t Name: " + session.Name + "\n\t Start Time: " + session.StartTime + "\n\t End Time: " + session.EndTime + "\n\t Activity: " + session.Activity.Value + "\n\t App Package Name: " + session.AppPackageName);
			Debug.Log("Data Sets Count: " + session.DataSets.Count);
			int num2 = 1;
			foreach (DataSet dataSet in session.DataSets)
			{
				Debug.Log("Data Set #" + num2 + "\n\t\t Data Type: " + dataSet.DataType.Value);
				Debug.Log("Data Points Count: " + dataSet.DataPoints.Count);
				int num3 = 1;
				foreach (DataPoint dataPoint in dataSet.DataPoints)
				{
					Debug.Log("Data Point #" + num3 + "\n\t\t\t Data Type: " + dataPoint.DataType.Value + "\n\t\t\t Start Time: " + dataPoint.StartTime + "\n\t\t\t End Time: " + dataPoint.EndTime);
					Debug.Log("Fields Count: " + dataPoint.Fields.Count);
					foreach (KeyValuePair<string, object> field in dataPoint.Fields)
					{
						Debug.Log("\t\t\t\t " + field.Key + " : " + field.Value.ToString());
					}
					num3++;
				}
				num2++;
			}
			num++;
		}
	}

	private void SA_Fitness_Connection_Instance_OnConnectionFinished(ConnectionResult result)
	{
		Debug.Log("Fitness connection result: " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			SA_StatusBar.text = "Player Disconnected " + result.Error.Code + " " + result.Error.Message;
			playerLabel.text = "Player Disconnected";
			Debug.Log("connection result: " + result.Error.Code + " " + result.Error.Message);
		}
		else
		{
			SA_StatusBar.text = "Player Connected";
			playerLabel.text = "Player Connected";
		}
		UpdateButtons();
	}

	private void Request_OnRequestFinished(SensorRequestResult result)
	{
		Debug.Log("Request_OnRequestFinished " + result.Id);
		if (result.IsSucceeded)
		{
			dataSources = result.DataSources;
		}
	}

	private void Listener_OnRegisterSuccess(int id)
	{
		Debug.Log("[Listener_OnRegisterSuccess] " + id);
	}

	private void Listener_OnRegisterFail(int id)
	{
		Debug.Log("[Listener_OnRegisterFail] " + id);
	}

	private void Listener_OnDataPointReceived(int id, DataPoint dataPoint)
	{
		Debug.Log("[Listener_OnDataPointReceived] id " + id + " dataPoint type: " + dataPoint.DataType.Value);
		Debug.Log("FIELDS: " + dataPoint.Fields.Count + "#");
		foreach (KeyValuePair<string, object> field in dataPoint.Fields)
		{
			Debug.Log("key:" + field.Key + " value:" + field.Value.ToString());
		}
	}

	private void Request_OnRequestFinished1(SubscriptionsRequestResult result)
	{
		Debug.Log("SubscriptionsRequest.ListSubscriptions " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		foreach (Subscription subscription in result.Subscriptions)
		{
			Debug.Log("Subscription: " + subscription.DataType.Value);
		}
	}

	private void Request_OnSubscribeFinished(Result result)
	{
		Debug.Log("Request_OnSubscribeFinished " + result.IsSucceeded);
	}

	private void Request_OnUnsubscribeFinished(Result result)
	{
		Debug.Log("[Request_OnUnsubscribeFinished] " + result.IsSucceeded);
	}

	private void Request_OnSessionStarted(Result result)
	{
		Debug.Log("[Request_OnSessionStarted] " + result.IsSucceeded);
	}

	private void Request_OnSessionStopped(StopSessionResult result)
	{
		Debug.Log("[Request_OnSessionStopped] " + result.IsSucceeded);
		if (!result.IsSucceeded)
		{
			return;
		}
		Debug.Log("Sessions Count " + result.Sessions.Count);
		foreach (Session session in result.Sessions)
		{
			Debug.Log("[Session] Id: " + session.Id + "\n\t Start Time: " + session.StartTime + "\n\t End Time: " + session.EndTime + "\n\t Name: " + session.Name + "\n\t Description: " + session.Description + "\n\t Activity: " + session.Activity.Value + "\n\t App Package Name: " + session.AppPackageName);
		}
	}
}
