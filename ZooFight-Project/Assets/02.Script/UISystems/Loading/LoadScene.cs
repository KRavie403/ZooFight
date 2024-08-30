using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class LoadScene : Singleton<LoadScene>
{
    //private string m_NextScene = "GameScene"; // 다음 씬의 이름

    public void OnStartGameButtonClicked()
    {
        // 로딩 씬을 비동기적으로 로드하고, 이후 게임 씬을 로드합니다.
        LoadLoadingScene().Forget();
    }


    private async UniTask LoadLoadingScene()
    {
        // 현재 로딩 씬이 이미 로드되어 있는 경우 언로드
        if (SceneManager.GetSceneByName("LoadingScene").isLoaded)
        {
            await SceneManager.UnloadSceneAsync("LoadingScene");
        }

        // 로딩 씬을 비동기적으로 로드
        AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene");
        loadingSceneOp.allowSceneActivation = true; // 씬 활성화를 제어합니다.

        // 로딩 씬이 로드될 때까지 기다립니다.
        while (!loadingSceneOp.isDone)
        {
            await UniTask.Yield(); // 프레임마다 대기하여 로딩 진행 상황을 체크합니다.
        }

        // 로딩 씬의 애니메이션을 시작하도록 설정
        // 로딩 씬에서의 애니메이션 처리는 로딩 씬 전용 스크립트에서 다룹니다.

        // 게임 씬을 비동기적으로 로드
        await LoadGameSceneAsync();
    }

    private async UniTask LoadGameSceneAsync()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            await UniTask.Yield();
            if (op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                return;
            }
        }
    }

    //__________________________________________________________________________________________

    //public async UniTask LoadingScene(string sceneName)
    //{
    //    // 로딩 씬을 비동기적으로 언로드
    //    if (SceneManager.GetSceneByName("LoadingScene").isLoaded)
    //    {
    //        await SceneManager.UnloadSceneAsync("LoadingScene");
    //    }

    //    // 로딩 씬을 비동기적으로 로드
    //    AsyncOperation loadingSceneOp = SceneManager.LoadSceneAsync("LoadingScene");
    //    loadingSceneOp.allowSceneActivation = false; // 씬 활성화를 제어합니다.

    //    // 로딩 씬이 로드될 때까지 기다립니다.
    //    while (!loadingSceneOp.isDone)
    //    {
    //        await UniTask.Yield(); // 프레임마다 대기하여 로딩 진행 상황을 체크합니다.
    //    }

    //    m_NextScene = sceneName;

    //    // 로딩 애니메이션을 시작
    //    LoadLoadingImg();

    //    // 비동기적으로 씬을 로드
    //    await LoadSceneAsync();
    //}

    //private async UniTask LoadSceneAsync()
    //{
    //    await UniTask.Yield();

    //    // 애니메이션이 끝나길 기다림
    //    await UniTask.WaitUntil(() => anim != null && _uiRawImage != null);

    //    AsyncOperation op = SceneManager.LoadSceneAsync(m_NextScene);
    //    op.allowSceneActivation = false;

    //    float timer = 0.0f;
    //    while (!op.isDone)
    //    {
    //        await UniTask.Yield();
    //        timer += Time.deltaTime;
    //        if (op.progress < 0.9f)
    //        {
    //            // 로딩 프로그레스가 0.9 미만일 때 애니메이션을 계속 진행
    //            anim.SetBool("IsRotating", timer > 0.5f);
    //        }
    //        else
    //        {
    //            // 로딩이 거의 완료되었을 때 애니메이션을 멈춤
    //            anim.SetBool("IsRotating", false);
    //            if (timer >= 1.0f)
    //            {
    //                op.allowSceneActivation = true;
    //                return;
    //            }
    //        }
    //    }
    //}
    //_____________________________________________________________________________________________________


    //private async UniTaskVoid OnLoadGameScene()
    //{
    //    await UniTask.Yield();

    //    // 현재 씬에서 2초 대기
    //    await UniTask.Delay(TimeSpan.FromSeconds(2));


    //    // 로딩 씬을 비동기적으로 로드합니다.
    //    AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("GameScene");
    //    loadSceneAsync.allowSceneActivation = false; // 씬 활성화를 제어합니다.

    //    // 씬 로드가 완료될 때까지 대기합니다.
    //    while (!loadSceneAsync.isDone)
    //    {
    //        // 로딩 화면을 보여주기 위한 코드 작성
    //        LoadLoadingImg();

    //        // 씬이 로드되고 나서 자동으로 활성화할 수 있도록 allowSceneActivation을 true로 설정합니다.
    //        if (loadSceneAsync.progress >= 0.9f)
    //        {
    //            loadSceneAsync.allowSceneActivation = true;
    //        }

    //        await UniTask.Yield(); // 프레임마다 대기하여 로딩 진행 상황을 체크합니다.
    //    }
    //}

}
