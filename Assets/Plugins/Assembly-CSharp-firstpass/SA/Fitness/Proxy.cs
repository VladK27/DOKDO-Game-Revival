namespace SA.Fitness
{
	public static class Proxy
	{
		private const string CLASS_NAME = "com.stansassets.fitness.Bridge";

		private static void Call(string methodName, params object[] args)
		{
			AN_ProxyPool.CallStatic("com.stansassets.fitness.Bridge", methodName, args);
		}

		private static ReturnType Call<ReturnType>(string methodName, params object[] args)
		{
			return AN_ProxyPool.CallStatic<ReturnType>("com.stansassets.fitness.Bridge", methodName, args);
		}

		public static void Connect(string connectionRequest)
		{
			Call("connect", connectionRequest);
		}

		public static void Disconnect()
		{
			Call("disconnect");
		}

		public static void RequestDataSources(string request)
		{
			Call("requestDataSources", request);
		}

		public static void RegisterSensorListener(string request)
		{
			Call("addSensorListener", request);
		}

		public static void ListSubscriptions(string request)
		{
			Call("listSubscriptions", request);
		}

		public static void Subscribe(string request)
		{
			Call("subscribe", request);
		}

		public static void Unsubscribe(string request)
		{
			Call("unsubscribe", request);
		}

		public static void ReadData(string request)
		{
			Call("readData", request);
		}

		public static void ReadDailyTotal(string request)
		{
			Call("readDailyTotal", request);
		}

		public static void ReadDailyTotalFromLocalDevice(string request)
		{
			Call("readDailyTotalFromLocalDevice", request);
		}

		public static void InsertData(string request)
		{
			Call("insertData", request);
		}

		public static void UpdateData(string request)
		{
			Call("updateData", request);
		}

		public static void DeleteData(string request)
		{
			Call("deleteData", request);
		}

		public static void StartSession(string request)
		{
			Call("startSession", request);
		}

		public static void StopSession(string request)
		{
			Call("stopSession", request);
		}

		public static void ReadSession(string request)
		{
			Call("readSession", request);
		}

		public static void InsertSesison(string request)
		{
			Call("insertSession", request);
		}
	}
}
