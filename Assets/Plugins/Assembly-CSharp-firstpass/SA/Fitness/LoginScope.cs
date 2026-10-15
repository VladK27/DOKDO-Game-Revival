namespace SA.Fitness
{
	public sealed class LoginScope
	{
		public static LoginScope SCOPE_ACTIVITY_READ = new LoginScope("https://www.googleapis.com/auth/fitness.activity.read");

		public static LoginScope SCOPE_ACTIVITY_READ_WRITE = new LoginScope("https://www.googleapis.com/auth/fitness.activity.write");

		public static LoginScope SCOPE_LOCATION_READ = new LoginScope("https://www.googleapis.com/auth/fitness.location.read");

		public static LoginScope SCOPE_LOCATION_READ_WRITE = new LoginScope("https://www.googleapis.com/auth/fitness.location.write");

		public static LoginScope SCOPE_BODY_READ = new LoginScope("https://www.googleapis.com/auth/fitness.body.read");

		public static LoginScope SCOPE_BODY_READ_WRITE = new LoginScope("https://www.googleapis.com/auth/fitness.body.write");

		public static LoginScope SCOPE_NUTRITION_READ = new LoginScope("https://www.googleapis.com/auth/fitness.nutrition.read");

		public static LoginScope SCOPE_NUTRITION_READ_WRITE = new LoginScope("https://www.googleapis.com/auth/fitness.nutrition.write");

		private string value = string.Empty;

		public string Value
		{
			get
			{
				return value;
			}
		}

		private LoginScope()
		{
		}

		private LoginScope(string scope)
		{
			value = scope;
		}

		public override bool Equals(object obj)
		{
			if (GetType() != obj.GetType())
			{
				return false;
			}
			LoginScope loginScope = obj as LoginScope;
			return value.Equals(loginScope.Value);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
