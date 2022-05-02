using System.IO;
using System.Text;

namespace CommonHelpers.Any.Interfaces
{
    public interface IStreamHelper
    {
        T JsonToObj<T>(byte[] array, Encoding encoding);
        byte[] ObjToJson<T>(T obj, Encoding encoding);

        byte[] StreamToByteArray(Stream input);
        T XmlToObj<T>(byte[] array, Encoding encoding);
        byte[] ObjToXml<T>(T obj);

        void WriteFile(byte[] array, Encoding encoding, string filePath);
    }
}
