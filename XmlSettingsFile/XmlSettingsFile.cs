using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlSettingsFile
{
    public class XmlSettingsFile
    {
        private string _path;
        private XmlSerializer _serializer;

        private object _obj;

        public XmlSettingsFile(string path, object obj)
        {            
            _path = path;
            _obj = obj;
            _serializer = new XmlSerializer(obj.GetType());
        }

        public void Write(object obj)
        {
            using (StreamWriter sw = new StreamWriter(_path, false, new UTF8Encoding(false)))
            {
                XmlSerializer xs = _serializer;
                //シリアル化して書き込む
                xs.Serialize(sw, obj);
            }
        }
        public object Read()
        {
            object obj;
            using (StreamReader sr = new StreamReader(_path, new UTF8Encoding(false)))
            {
                XmlSerializer xs = _serializer;
                obj = xs.Deserialize(sr);
            }
            return obj;
        }
        public bool ExistFile()
        {
            return File.Exists(_path);
        }
        public bool ExixtDirectory()
        {
            return File.Exists(_path);
        }

    }
}

