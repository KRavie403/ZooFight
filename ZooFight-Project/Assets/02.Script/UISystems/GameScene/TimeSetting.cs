using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;


    void Update()
    {
        if (remainingTime > 0)
        {
            if (remainingTime < 11) { timerText.color = Color.red; }
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;

            timerText.color = Color.red;

            OnLoadResultScene().Forget();
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private async UniTaskVoid OnLoadResultScene()
    {
        await UniTask.Yield();

        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync("GameResultScene");
        loadSceneAsync.allowSceneActivation = false;

        while (!loadSceneAsync.isDone)
        {
            await UniTask.Yield();

            if (loadSceneAsync.progress >= 0.9f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1));

                loadSceneAsync.allowSceneActivation = true;
            }
        }
    }
}
