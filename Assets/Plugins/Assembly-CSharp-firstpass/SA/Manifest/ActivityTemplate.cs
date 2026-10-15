using System.Collections.Generic;
using System.Xml;

namespace SA.Manifest
{
	public class ActivityTemplate : BaseTemplate
	{
		public bool IsOpen;

		private int _id;

		private bool _isLauncher;

		private string _name = string.Empty;

		public bool IsLauncher
		{
			get
			{
				return _isLauncher;
			}
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public int Id
		{
			get
			{
				return _id;
			}
		}

		public ActivityTemplate(bool isLauncher, string name)
		{
			_isLauncher = isLauncher;
			_name = name;
			_id = GetHashCode();
			_values = new Dictionary<string, string>();
			_properties = new Dictionary<string, List<PropertyTemplate>>();
			SetValue("android:name", name);
		}

		public void SetName(string name)
		{
			_name = name;
			SetValue("android:name", name);
		}

		public void SetAsLauncher(bool isLauncher)
		{
			_isLauncher = isLauncher;
		}

		public static PropertyTemplate GetLauncherPropertyTemplate()
		{
			PropertyTemplate propertyTemplate = new PropertyTemplate("intent-filter");
			PropertyTemplate propertyTemplate2 = new PropertyTemplate("action");
			propertyTemplate2.SetValue("android:name", "android.intent.action.MAIN");
			propertyTemplate.AddProperty("action", propertyTemplate2);
			propertyTemplate2 = new PropertyTemplate("category");
			propertyTemplate2.SetValue("android:name", "android.intent.category.LAUNCHER");
			propertyTemplate.AddProperty("category", propertyTemplate2);
			return propertyTemplate;
		}

		public bool IsLauncherProperty(PropertyTemplate property)
		{
			if (property.Tag.Equals("intent-filter") && property.Properties.ContainsKey("category"))
			{
				foreach (PropertyTemplate item in property.Properties["category"])
				{
					if (item.Values.ContainsKey("android:name") && item.Values["android:name"].Equals("android.intent.category.LAUNCHER"))
					{
						return true;
					}
				}
			}
			return false;
		}

		public override void ToXmlElement(XmlDocument doc, XmlElement parent)
		{
			AddAttributesToXml(doc, parent, this);
			PropertyTemplate propertyTemplate = null;
			if (_isLauncher)
			{
				propertyTemplate = GetLauncherPropertyTemplate();
				AddProperty(propertyTemplate.Tag, propertyTemplate);
			}
			AddPropertiesToXml(doc, parent, this);
			if (_isLauncher)
			{
				_properties["intent-filter"].Remove(propertyTemplate);
			}
		}
	}
}
