using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class BinaryDataStream
{
   public static void Save<T>(T serializedObject, string fileName)
    {
        string path = Application.persistentDataPath + "/saves/";
        Directory.CreateDirectory(path);

        BinaryFormatter formatter = new BinaryFormatter();
        //BinaryFormatter là một công cụ giúp chuyển đổi một đối tượng trong bộ nhớ thành dạng nhị phân để có thể lưu trữ vào tệp tin.

        FileStream fileStream = new FileStream(path + fileName + ".dat", FileMode.Create);
        //.dat(để chỉ ra đây là tệp tin nhị phân).
        //FileMode.Create: Chỉ định rằng nếu tệp tin đã tồn tại, nó sẽ bị ghi đè; nếu chưa tồn tại, nó sẽ được tạo mới.

        try
        {
            formatter.Serialize(fileStream, serializedObject);
            //Nó sử dụng BinaryFormatter để chuyển đổi đối tượng serializedObject thành dạng nhị phân và ghi vào tệp tin thông qua fileStream.
        }
        catch (SerializationException e)
        {
            Debug.Log("Save filled. Error: " + e.Message);
        }
        finally
        {
            fileStream.Close();
            //Đảm bảo rằng tệp tin luôn được đóng sau khi sử dụng, dù có lỗi xảy ra hay không.
            // Điều này quan trọng để tránh rò rỉ tài nguyên và các vấn đề khác.
        }
    }

    public static bool Exist(string fileName) // kiểm tra xem một tệp tin cụ thể có tồn tại trong thư mục lưu trữ dữ liệu của ứng dụng hay không
    {
        string path = Application.persistentDataPath + "/saves/";
        string fullFileName = fileName + ".dat";
        return File.Exists(path + fullFileName);
    }

    public static T Read<T>(string fileName)
    {
        string path = Application.persistentDataPath + "/saves/";
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path + fileName + ".dat", FileMode.Open);
        T returnType = default(T);

        try
        {
            returnType = (T) formatter.Deserialize(fileStream);
        }
        catch (SerializationException e)
        {
            Debug.Log("Read filed. Error: " + e.Message);
        }
        finally
        {
            fileStream.Close();
        }

        return returnType;
    }
}
