using CommonHelpers.Any.Interfaces;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace CommonHelpers.Any
{
    public class StreamHelper : IStreamHelper
    {
        public T JsonToObj<T>(byte[] array, Encoding encoding)
        {
            string result = encoding.GetString(array);
            return JsonConvert.DeserializeObject<T>(result);
        }
        public byte[] ObjToJson<T>(T obj, Encoding encoding)
        {
            string result = JsonConvert.SerializeObject(obj);
            return encoding.GetBytes(result);
        }

        public byte[] StreamToByteArray(Stream input)
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                result = memoryStream.ToArray();
            }
            return result;
        }
        public T XmlToObj<T>(byte[] array, Encoding encoding)
        {
            MemoryStream stream = new MemoryStream(array);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            StreamReader streamReader = new StreamReader(stream, encoding);
            return (T)xmlSerializer.Deserialize(streamReader);
        }
        public byte[] ObjToXml<T>(T obj)
        {
            byte[] productTypeByteArray;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xmlSerializer.Serialize(ms, obj);
                productTypeByteArray = ms.ToArray();
            }
            return productTypeByteArray;
        }

        public void WriteFile(byte[] array, Encoding encoding, string filePath)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filePath, FileMode.Create), encoding))
            {
                binaryWriter.Write(array);
                binaryWriter.Flush();
            }
        }
    }
}
