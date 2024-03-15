using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class DataParser : MonoBehaviour
{
    #region json
    public static T ReadJsonData<T>(byte[] buf)
    {
        var strByte = Encoding.Default.GetString(buf);
        //byte �迭�� string���� ��ȯ
        return JsonUtility.FromJson<T>(strByte);
    }

    public static byte[] DataToJsonData<T>(T obj)
    {
        var jsonData = JsonUtility.ToJson(obj);
        //string�� byte �迭�� ��ȯ
        return Encoding.UTF8.GetBytes(jsonData);
    }
    #endregion

}
