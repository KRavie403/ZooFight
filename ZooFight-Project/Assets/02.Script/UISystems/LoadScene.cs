using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //public string nextSceneName;
    //public string secondarySceneName;

    //public void LoadLoadingScene()
    //{
    //    LoadingManager.LoadSceneHandle(nextSceneName, 0);
    //}

    //public void LoadSecondaryScene()
    //{
    //    LoadingManager.LoadSceneHandle(secondarySceneName, 0);
    //}
    private void Start()
    {
        StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(3);
    }
}
