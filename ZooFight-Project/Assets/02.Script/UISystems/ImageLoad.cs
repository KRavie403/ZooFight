using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class ImageLoad : MonoBehaviour
{
    public RawImage img;

    void Start()
    {
        StartCoroutine(TextureLoad());
    }

    IEnumerator TextureLoad()
    {
        string url = "서버주소";
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);


        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(www.error);
        }
        else
        {
            img.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }
}
