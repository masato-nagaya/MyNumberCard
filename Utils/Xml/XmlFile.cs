using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Utils.Xml {
	public class XmlFile {
		private string _path;
		private XmlSerializer _serializer;

		private object _obj;
		public XmlFile(string path, object obj) {
			//if (!File.Exists(_path)) {
			//	throw new IOException();
			//}
			_path = path;
			_obj = obj;
			_serializer = new XmlSerializer(obj.GetType());
		}

		public void Write(object obj) {
			try {
				using (FileStream stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None)) {
					using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false))) {
						XmlSerializer xs = _serializer;
						xs.Serialize(writer, obj);
						stream.Flush();
					}
				}
			}
			catch {
				throw;
			}
		}
		public object Read() {
			object obj;
			try {
				using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read)) {
					using (StreamReader sr = new StreamReader(_path, new UTF8Encoding(false))) {
						XmlSerializer xs = _serializer;
						obj = xs.Deserialize(sr);
					}
				}
			}
			catch {
				throw;
			}
			return obj;
		}
		public bool ExistFile() {
			return File.Exists(_path);
		}
		public bool ExixtDirectory() {
			return File.Exists(_path);
		}

	}
}

