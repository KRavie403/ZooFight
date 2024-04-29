using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public GameObject loginBtn;

    [SerializeField]
    Image loadingImg;

    public static string loadScene;
    public static int loadType;
    public static void LoadSceneHandle(string _name, int _loadType)
    {
        loadScene = _name;
        loadType = _loadType;
        LoadScene();
    }


    public static void LoadScene()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void Login()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
