namespace Percent.Http
{
	public class Parameter
	{
		private string url;

		private bool isFirstParameter = true;

		internal Parameter(string prefix)
		{
			url = prefix;
		}

		internal void addParamater(string key, string[] paramValues)
		{
			if (isFirstParameter)
			{
				string text = url;
				url = text + PercentHttpConfig.QUERY_STRING + key + PercentHttpConfig.EQUAL + getSetOf(paramValues);
				isFirstParameter = false;
			}
			else
			{
				string text = url;
				url = text + PercentHttpConfig.AND + key + PercentHttpConfig.EQUAL + getSetOf(paramValues);
			}
		}

		private string getSetOf(string[] paramValues)
		{
			string text = paramValues[0];
			for (int i = 1; i < paramValues.Length; i++)
			{
				text = text + "," + paramValues[i];
			}
			return text;
		}

		internal void addParamater(string key, string paramValue)
		{
			if (isFirstParameter)
			{
				string text = url;
				url = text + PercentHttpConfig.QUERY_STRING + key + PercentHttpConfig.EQUAL + paramValue;
				isFirstParameter = false;
			}
			else
			{
				string text = url;
				url = text + PercentHttpConfig.AND + key + PercentHttpConfig.EQUAL + paramValue;
			}
		}

		internal void addParamater(string key, int paramValue)
		{
			addParamater(key, paramValue.ToString());
		}

		internal string getUrl()
		{
			return url;
		}
	}
}
