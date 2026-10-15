using System.Collections.Generic;
using SA.Common.Data;

namespace SA.IOSNative.UserNotifications
{
	public class NotificationContent
	{
		public string Title = string.Empty;

		public string Subtitle = string.Empty;

		public string Body = string.Empty;

		public int Badge;

		public string LaunchImageName = string.Empty;

		public Dictionary<string, object> UserInfo = new Dictionary<string, object>();

		public NotificationContent()
		{
		}

		public NotificationContent(Dictionary<string, object> contentDictionary)
		{
			Title = (string)contentDictionary["title"];
			Subtitle = (string)contentDictionary["subtitle"];
			Body = (string)contentDictionary["body"];
			LaunchImageName = (string)contentDictionary["launchImageName"];
			Badge = int.Parse(contentDictionary["badge"].ToString());
			UserInfo = (Dictionary<string, object>)Json.Deserialize(contentDictionary["userInfo"].ToString());
		}

		public override string ToString()
		{
			string text = Json.Serialize(UserInfo);
			return "{" + string.Format("\"title\" : \"{0}\", \"subtitle\" : \"{1}\", \"body\" : \"{2}\", \"badge\" : {3}, \"launchImageName\" : \"{4}\", \"userInfo\" : {5}", Title, Subtitle, Body, Badge, LaunchImageName, text) + "}";
		}
	}
}
