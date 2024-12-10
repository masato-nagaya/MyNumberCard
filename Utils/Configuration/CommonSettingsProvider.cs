using System;
using System.Collections;
using System.Configuration;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Xml;
using System.IO;

namespace Utils.Configuration {

	public sealed class CommonSettingsProvider : SettingsProvider, IApplicationSettingsProvider {
		private string _ApplicationName = String.Empty;
		private const string _Settingsroot = "Settings";
		private const string _LocalSettingsNodeName = "LocalSettings";
		private const string _GlobalSettingsNodeName = "GlobalSettings";
		private const string _ClassName = "CommonSettingsProvider";
		private XmlDocument _XmlDocument;

		private string FilePath {
			get {
				//return Path.Combine(Path.GetDirectoryName(Application.ExecutablePath),
				//   string.Format("{0}.settings", ApplicationName));
				return Path.Combine(Application.CommonAppDataPath, "user.cofig");
			}
		}

		private XmlNode LocalSettingsNode {
			get {
				XmlNode settingsNode = GetSettingsNode(_LocalSettingsNodeName);
				XmlNode machineNode = settingsNode.SelectSingleNode(Environment.MachineName.ToLowerInvariant());
				if (machineNode == null) {
					machineNode = RootDocument.CreateElement(Environment.MachineName.ToLowerInvariant());
					settingsNode.AppendChild(machineNode);
				}
				return machineNode;
			}
		}

		private XmlNode GlobalSettingsNode {
			get { return GetSettingsNode(_GlobalSettingsNodeName); }
		}

		private XmlNode RootNode {
			get { return RootDocument.SelectSingleNode(_Settingsroot); }
		}

		private XmlDocument RootDocument {
			get {
				if (_XmlDocument == null) {
					try {
						_XmlDocument = new XmlDocument();
						_XmlDocument.Load(FilePath);
					}
					catch (Exception) {
					}
					if (_XmlDocument.SelectSingleNode(_Settingsroot) != null) {
						return _XmlDocument;
					}
					_XmlDocument = GetBlankXmlDocument();
				}
				return _XmlDocument;
			}
		}

		//public override string ApplicationName {
		//    get { return Path.GetFileNameWithoutExtension(Application.ExecutablePath); }
		//    set { }
		//}

		public override string ApplicationName {
			get { return _ApplicationName; }
			set { _ApplicationName = value; }
		}


		public override string Name {
			get { return _ClassName; }
		}

		public override void Initialize(string name, NameValueCollection values) {
			if (String.IsNullOrEmpty(name)) {
				name = Name;
			}
			base.Initialize(Name, values);
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection) {
			foreach (SettingsPropertyValue propertyValue in collection)
				SetValue(propertyValue);

			try {
				RootDocument.Save(FilePath);
			}
			catch (Exception) {
				/* 
				 * If this is a portable application and the device has been 
				 * removed then this will fail, so don't do anything. It's 
				 * probably better for the application to stop saving settings 
				 * rather than just crashing outright. Probably.
				 */
			}
		}

		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection) {
			SettingsPropertyValueCollection values = new SettingsPropertyValueCollection();

			foreach (SettingsProperty property in collection) {
				values.Add(new SettingsPropertyValue(property) {
					SerializedValue = GetValue(property)
				});
			}

			return values;
		}

		private void SetValue(SettingsPropertyValue propertyValue) {
			XmlNode targetNode = IsGlobal(propertyValue.Property) ? GlobalSettingsNode : LocalSettingsNode;
			XmlNode settingNode = targetNode.SelectSingleNode(string.Format("setting[@name='{0}']", propertyValue.Name));

			if (settingNode != null) {
				settingNode.InnerText = propertyValue.SerializedValue.ToString();
			}
			else {
				settingNode = RootDocument.CreateElement("setting");

				XmlAttribute nameAttribute = RootDocument.CreateAttribute("name");
				nameAttribute.Value = propertyValue.Name;

				settingNode.Attributes.Append(nameAttribute);
				settingNode.InnerText = propertyValue.SerializedValue.ToString();

				targetNode.AppendChild(settingNode);
			}
		}

		private string GetValue(SettingsProperty property) {
			XmlNode targetNode = IsGlobal(property) ? GlobalSettingsNode : LocalSettingsNode;
			XmlNode settingNode = targetNode.SelectSingleNode(string.Format("setting[@name='{0}']", property.Name));

			if (settingNode == null)
				return property.DefaultValue != null ? property.DefaultValue.ToString() : string.Empty;

			return settingNode.InnerText;
		}

		private bool IsGlobal(SettingsProperty property) {
			foreach (DictionaryEntry attribute in property.Attributes) {
				if ((Attribute)attribute.Value is SettingsManageabilityAttribute)
					return true;
			}

			return false;
		}

		private XmlNode GetSettingsNode(string name) {
			XmlNode settingsNode = RootNode.SelectSingleNode(name);

			if (settingsNode == null) {
				settingsNode = RootDocument.CreateElement(name);
				RootNode.AppendChild(settingsNode);
			}

			return settingsNode;
		}

		public XmlDocument GetBlankXmlDocument() {
			XmlDocument blankXmlDocument = new XmlDocument();
			blankXmlDocument.AppendChild(blankXmlDocument.CreateXmlDeclaration("1.0", "utf-8", string.Empty));
			blankXmlDocument.AppendChild(blankXmlDocument.CreateElement(_Settingsroot));

			return blankXmlDocument;
		}

		public void Reset(SettingsContext context) {
			LocalSettingsNode.RemoveAll();
			GlobalSettingsNode.RemoveAll();

			_XmlDocument.Save(FilePath);
		}

		public SettingsPropertyValue GetPreviousVersion(SettingsContext context, SettingsProperty property) {
			// do nothing
			return new SettingsPropertyValue(property);
		}

		public void Upgrade(SettingsContext context, SettingsPropertyCollection properties) {
		}
	}
}
