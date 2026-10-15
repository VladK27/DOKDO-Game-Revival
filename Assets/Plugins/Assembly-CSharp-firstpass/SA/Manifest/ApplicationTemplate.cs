using System.Collections.Generic;
using System.Xml;

namespace SA.Manifest
{
	public class ApplicationTemplate : BaseTemplate
	{
		private Dictionary<int, ActivityTemplate> _activities;

		public Dictionary<int, ActivityTemplate> Activities
		{
			get
			{
				return _activities;
			}
		}

		public ApplicationTemplate()
		{
			_activities = new Dictionary<int, ActivityTemplate>();
		}

		public void AddActivity(ActivityTemplate activity)
		{
			_activities.Add(activity.Id, activity);
		}

		public void RemoveActivity(ActivityTemplate activity)
		{
			_activities.Remove(activity.Id);
		}

		public ActivityTemplate GetOrCreateActivityWithName(string name)
		{
			ActivityTemplate activityTemplate = GetActivityWithName(name);
			if (activityTemplate == null)
			{
				activityTemplate = new ActivityTemplate(false, name);
				AddActivity(activityTemplate);
			}
			return activityTemplate;
		}

		public ActivityTemplate GetActivityWithName(string name)
		{
			foreach (KeyValuePair<int, ActivityTemplate> activity in Activities)
			{
				if (activity.Value.Name.Equals(name))
				{
					return activity.Value;
				}
			}
			return null;
		}

		public ActivityTemplate GetLauncherActivity()
		{
			foreach (KeyValuePair<int, ActivityTemplate> activity in Activities)
			{
				if (activity.Value.IsLauncher)
				{
					return activity.Value;
				}
			}
			return null;
		}

		public override void ToXmlElement(XmlDocument doc, XmlElement parent)
		{
			AddAttributesToXml(doc, parent, this);
			AddPropertiesToXml(doc, parent, this);
			foreach (int key in _activities.Keys)
			{
				XmlElement xmlElement = doc.CreateElement("activity");
				_activities[key].ToXmlElement(doc, xmlElement);
				parent.AppendChild(xmlElement);
			}
		}
	}
}
