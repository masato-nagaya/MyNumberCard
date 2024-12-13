using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Threading;

namespace Utils.Xml
{
    public class XmlFile
    {
        private string _path;
        private XmlSerializer _serializer;

        private object _obj;
        public XmlFile(string path, object obj)
        {
            //if (!File.Exists(_path)) {
            //	throw new IOException();
            //}
            _path = path;
            _obj = obj;
            _serializer = new XmlSerializer(obj.GetType());
        }

        public void Write(object obj)
        {
            try
            {
                using (FileStream stream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter writer = new StreamWriter(stream, new UTF8Encoding(false)))
                    {
                        XmlSerializer xs = _serializer;
                        xs.Serialize(writer, obj);
                        stream.Flush();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        //add 2024/12/11 str        
        //public object Read()
        //{
        //    object obj;
        //    try
        //    {
        //        using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))

        //        {
        //            using (StreamReader sr = new StreamReader(_path, new UTF8Encoding(false)))                    
        //            {
        //                XmlSerializer xs = _serializer;
        //                obj = xs.Deserialize(sr);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    return obj;
        //}
        public object Read(int maxRetries = 5, int retryIntervalMillis = 1000)
        {
            int attempt = 0;

            while (true)
            {
                try
                {
                    using (FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        using (StreamReader sr = new StreamReader(stream, new UTF8Encoding(false)))
                        {
                            XmlSerializer xs = _serializer;
                            return xs.Deserialize(sr);
                        }
                    }
                }
                catch (IOException ex)
                {
                    attempt++;

                    if (attempt >= maxRetries)
                    {
                        // リトライ失敗時のエラーハンドリング
                        throw new IOException("採番ファイルを取得出来ませんでした。時間を置いてから再度採番処理を実行してください。", ex);
                    }

                    // リトライ間隔を指定して待機
                    Thread.Sleep(retryIntervalMillis);
                }
            }
        }
        //add 2024/12/11 end

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

