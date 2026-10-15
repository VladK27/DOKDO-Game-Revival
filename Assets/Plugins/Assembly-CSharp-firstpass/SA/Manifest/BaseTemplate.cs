using System.Collections.Generic;
using System.Xml;

namespace SA.Manifest
{
	public abstract class BaseTemplate
	{
		protected Dictionary<string, List<PropertyTemplate>> _properties;

		protected Dictionary<string, string> _values;

		public Dictionary<string, string> Values
		{
			get
			{
				return _values;
			}
		}

		public Dictionary<string, List<PropertyTemplate>> Properties
		{
			get
			{
				return _properties;
			}
		}

		public BaseTemplate()
		{
			_values = new Dictionary<string, string>();
			_properties = new Dictionary<string, List<PropertyTemplate>>();
		}

		public PropertyTemplate GetOrCreateIntentFilterWithName(string name)
		{
			PropertyTemplate propertyTemplate = GetIntentFilterWithName(name);
			if (propertyTemplate == null)
			{
				propertyTemplate = new PropertyTemplate("intent-filter");
				PropertyTemplate propertyTemplate2 = new PropertyTemplate("action");
				propertyTemplate2.SetValue("android:name", name);
				propertyTemplate.AddProperty(propertyTemplate2);
				AddProperty(propertyTemplate);
			}
			return propertyTemplate;
		}

		public PropertyTemplate GetIntentFilterWithName(string name)
		{
			string tag = "intent-filter";
			List<PropertyTemplate> propertiesWithTag = GetPropertiesWithTag(tag);
			foreach (PropertyTemplate item in propertiesWithTag)
			{
				string intentFilterName = GetIntentFilterName(item);
				if (intentFilterName.Equals(name))
				{
					return item;
				}
			}
			return null;
		}

		public string GetIntentFilterName(PropertyTemplate intent)
		{
			List<PropertyTemplate> propertiesWithTag = intent.GetPropertiesWithTag("action");
			if (propertiesWithTag.Count > 0)
			{
				return propertiesWithTag[0].GetValue("android:name");
			}
			return string.Empty;
		}

		public PropertyTemplate GetOrCreatePropertyWithName(string tag, string name)
		{
			PropertyTemplate propertyTemplate = GetPropertyWithName(tag, name);
			if (propertyTemplate == null)
			{
				propertyTemplate = new PropertyTemplate(tag);
				propertyTemplate.SetValue("android:name", name);
				AddProperty(propertyTemplate);
			}
			return propertyTemplate;
		}

		public PropertyTemplate GetPropertyWithName(string tag, string name)
		{
			List<PropertyTemplate> propertiesWithTag = GetPropertiesWithTag(tag);
			foreach (PropertyTemplate item in propertiesWithTag)
			{
				if (item.Values.ContainsKey("android:name") && item.Values["android:name"] == name)
				{
					return item;
				}
			}
			return null;
		}

		public PropertyTemplate GetOrCreatePropertyWithTag(string tag)
		{
			PropertyTemplate propertyTemplate = GetPropertyWithTag(tag);
			if (propertyTemplate == null)
			{
				propertyTemplate = new PropertyTemplate(tag);
				AddProperty(propertyTemplate);
			}
			return propertyTemplate;
		}

		public PropertyTemplate GetPropertyWithTag(string tag)
		{
			List<PropertyTemplate> propertiesWithTag = GetPropertiesWithTag(tag);
			if (propertiesWithTag.Count > 0)
			{
				return propertiesWithTag[0];
			}
			return null;
		}

		public List<PropertyTemplate> GetPropertiesWithTag(string tag)
		{
			if (Properties.ContainsKey(tag))
			{
				return Properties[tag];
			}
			return new List<PropertyTemplate>();
		}

		public abstract void ToXmlElement(XmlDocument doc, XmlElement parent);

		public void AddProperty(PropertyTemplate property)
		{
			AddProperty(property.Tag, property);
		}

		public void AddProperty(string tag, PropertyTemplate property)
		{
			if (!_properties.ContainsKey(tag))
			{
				List<PropertyTemplate> value = new List<PropertyTemplate>();
				_properties.Add(tag, value);
			}
			_properties[tag].Add(property);
		}

		public void SetValue(string key, string value)
		{
			if (_values.ContainsKey(key))
			{
				_values[key] = value;
			}
			else
			{
				_values.Add(key, value);
			}
		}

		public string GetValue(string key)
		{
			if (_values.ContainsKey(key))
			{
				return _values[key];
			}
			return string.Empty;
		}

		public void RemoveProperty(PropertyTemplate property)
		{
			_properties[property.Tag].Remove(property);
		}

		public void RemoveValue(string key)
		{
			_values.Remove(key);
		}

		public void AddPropertiesToXml(XmlDocument doc, XmlElement parent, BaseTemplate template)
		{
			foreach (string key in template.Properties.Keys)
			{
				foreach (PropertyTemplate item in template.Properties[key])
				{
					XmlElement xmlElement = doc.CreateElement(key);
					AddAttributesToXml(doc, xmlElement, item);
					AddPropertiesToXml(doc, xmlElement, item);
					parent.AppendChild(xmlElement);
				}
			}
		}

		public void AddAttributesToXml(XmlDocument doc, XmlElement parent, BaseTemplate template)
		{
			foreach (string key in template.Values.Keys)
			{
				string name = key;
				if (key.Contains("android:"))
				{
					name = key.Replace("android:", "android___");
				}
				XmlAttribute xmlAttribute = doc.CreateAttribute(name);
				xmlAttribute.Value = template.Values[key];
				parent.Attributes.Append(xmlAttribute);
			}
		}
	}
}
