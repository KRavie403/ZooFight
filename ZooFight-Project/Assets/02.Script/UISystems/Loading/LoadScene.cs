using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyUI.Progress;
using Cysharp.Threading.Tasks;
using System;

public class LoadScene : MonoBehaviour
{
    public Animator anim;

    [SerializeField]
    private RawImage _uiRawImage;

    private void Start()
    {
        LoadLoadingImg();

        OnLoadGameScene().Forget();
    }

    private async UniTaskVoid OnLoadGameScene()
    {
        await UniTask.Yield();

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("GameScene");
        loadSceneAsync.allowSceneActivation = false;

        while(!loadSceneAsync.isDone)
        {
            await UniTask.Yield();

            if(loadSceneAsync.progress >= 0.9f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(2));

                loadSceneAsync.allowSceneActivation=true;
            }
        }
    }

    public void LoadLoadingImg()
    {
        anim.SetBool("IsRotating", true);
        //Progress.Show("Loading image. . .", ProgressColor.Orange);
        //StartCoroutine("LoadImg");
    }

    //private IEnumerator LoadImg()
    //{
    //    yield return null;

    //    Progress.Hide();
    //    _uiRawImage = Resources.Load<RawImage>("Loading");

    //}
}
