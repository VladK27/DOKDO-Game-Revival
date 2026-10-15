using System.Collections.Generic;
using System.Xml;

namespace SA.Manifest
{
	public class Template : BaseTemplate
	{
		private ApplicationTemplate _applicationTemplate;

		private List<PropertyTemplate> _permissions;

		public ApplicationTemplate ApplicationTemplate
		{
			get
			{
				return _applicationTemplate;
			}
		}

		public List<PropertyTemplate> Permissions
		{
			get
			{
				return _permissions;
			}
		}

		public Template()
		{
			_applicationTemplate = new ApplicationTemplate();
			_permissions = new List<PropertyTemplate>();
		}

		public bool HasPermission(string name)
		{
			foreach (PropertyTemplate permission in Permissions)
			{
				if (permission.Name.Equals(name))
				{
					return true;
				}
			}
			return false;
		}

		public void RemovePermission(string name)
		{
			while (HasPermission(name))
			{
				foreach (PropertyTemplate permission in Permissions)
				{
					if (permission.Name.Equals(name))
					{
						RemovePermission(permission);
						break;
					}
				}
			}
		}

		public void RemovePermission(PropertyTemplate permission)
		{
			_permissions.Remove(permission);
		}

		public void AddPermission(string name)
		{
			if (!HasPermission(name))
			{
				PropertyTemplate propertyTemplate = new PropertyTemplate("uses-permission");
				propertyTemplate.Name = name;
				AddPermission(propertyTemplate);
			}
		}

		public void AddPermission(PropertyTemplate permission)
		{
			_permissions.Add(permission);
		}

		public override void ToXmlElement(XmlDocument doc, XmlElement parent)
		{
			AddAttributesToXml(doc, parent, this);
			AddPropertiesToXml(doc, parent, this);
			XmlElement xmlElement = doc.CreateElement("application");
			_applicationTemplate.ToXmlElement(doc, xmlElement);
			parent.AppendChild(xmlElement);
			foreach (PropertyTemplate permission in Permissions)
			{
				XmlElement xmlElement2 = doc.CreateElement("uses-permission");
				permission.ToXmlElement(doc, xmlElement2);
				parent.AppendChild(xmlElement2);
			}
		}
	}
}
