namespace SA.Fitness
{
	public sealed class DataType
	{
		public static DataType AGGREGATE_ACTIVITY_SUMMARY = new DataType("com.google.activity.summary");

		public static DataType AGGREGATE_BASAL_METABOLIC_RATE_SUMMARY = new DataType("com.google.bmr.summary");

		public static DataType AGGREGATE_BODY_FAT_PERCENTAGE_SUMMARY = new DataType("com.google.body.fat_percentage.summary");

		public static DataType AGGREGATE_CALORIES_EXPENDED = new DataType("com.google.calories.expended");

		public static DataType AGGREGATE_DISTANCE_DELTA = new DataType("com.google.distance.delta");

		public static DataType AGGREGATE_HEART_RATE_SUMMARY = new DataType("com.google.heart_rate.summary");

		public static DataType AGGREGATE_HYDRATION = new DataType("com.google.hydration");

		public static DataType AGGREGATE_LOCATION_BOUNDING_BOX = new DataType("com.google.location.bounding_box");

		public static DataType AGGREGATE_NUTRITION_SUMMARY = new DataType("com.google.nutrition.summary");

		public static DataType AGGREGATE_POWER_SUMMARY = new DataType("com.google.power.summary");

		public static DataType AGGREGATE_SPEED_SUMMARY = new DataType("com.google.speed.summary");

		public static DataType AGGREGATE_STEP_COUNT_DELTA = new DataType("com.google.step_count.delta");

		public static DataType AGGREGATE_WEIGHT_SUMMARY = new DataType("com.google.weight.summary");

		public static DataType TYPE_ACTIVITY_SAMPLES = new DataType("com.google.activity.samples");

		public static DataType TYPE_ACTIVITY_SEGMENT = new DataType("com.google.activity.segment");

		public static DataType TYPE_BASAL_METABOLIC_RATE = new DataType("com.google.calories.bmr");

		public static DataType TYPE_BODY_FAT_PERCENTAGE = new DataType("com.google.body.fat.percentage");

		public static DataType TYPE_CALORIES_EXPENDED = new DataType("com.google.calories.expended");

		public static DataType TYPE_CYCLING_PEDALING_CADENCE = new DataType("com.google.cycling.cadence");

		public static DataType TYPE_CYCLING_PEDALING_CUMULATIVE = new DataType("com.google.cycling.pedaling.cumulative");

		public static DataType TYPE_CYCLING_WHEEL_REVOLUTION = new DataType("com.google.cycling.wheel_revolution.cumulative");

		public static DataType TYPE_CYCLING_WHEEL_RPM = new DataType("com.google.cycling.wheel.revolutions");

		public static DataType TYPE_DISTANCE_DELTA = new DataType("com.google.distance.delta");

		public static DataType TYPE_HEART_RATE_BPM = new DataType("com.google.heart_rate.bpm");

		public static DataType TYPE_HEIGHT = new DataType("com.google.height");

		public static DataType TYPE_HYDRATION = new DataType("com.google.hydration");

		public static DataType TYPE_LOCATION_SAMPLE = new DataType("com.google.location.sample");

		public static DataType TYPE_LOCATION_TRACK = new DataType("com.google.location.track");

		public static DataType TYPE_NUTRITION = new DataType("com.google.nutrition");

		public static DataType TYPE_POWER_SAMPLE = new DataType("com.google.power.sample");

		public static DataType TYPE_SPEED = new DataType("com.google.speed");

		public static DataType TYPE_STEP_COUNT_CADENCE = new DataType("com.google.step_count.cadence");

		public static DataType TYPE_STEP_COUNT_DELTA = new DataType("com.google.step_count.delta");

		public static DataType TYPE_WEIGHT = new DataType("com.google.weight");

		public static DataType TYPE_WORKOUT_EXERCISE = new DataType("com.google.activity.exercise");

		private string value = string.Empty;

		public string Value
		{
			get
			{
				return value;
			}
		}

		private DataType()
		{
		}

		internal DataType(string dataType)
		{
			value = dataType;
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
			{
				return false;
			}
			DataType dataType = obj as DataType;
			return value.Equals(dataType.Value);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
