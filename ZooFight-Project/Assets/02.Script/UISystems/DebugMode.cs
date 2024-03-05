using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugMode : MonoBehaviour
{
    void Update()
    {
    }

    public void NextScene()
    {
        string NextSceneName;

        if (SceneManager.GetActiveScene().name == "MultiplayerLobbyScene")
        {
            NextSceneName = "GameplayScene";
        }
        else
        {
            string curScene = SceneManager.GetActiveScene().name;
            string[] nextScene = curScene.Split('-');
            int NextSceneNum = int.Parse(nextScene[0]) + 1;
            NextSceneName = NextSceneNum.ToString() + "-" + nextScene[1];
        }
        LoadingManager.LoadSceneHandle(NextSceneName, 0);
    }
}
