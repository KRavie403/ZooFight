using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private Coroutine movingNextSceneCoroutine;

    void Start()
    {
    }

    void Update()
    {
        if(remainingTime > 0)
        {
            if (remainingTime < 11) {timerText.color = Color.red;}
            remainingTime -= Time.deltaTime;
        }
        else if(remainingTime < 0)
        {
            remainingTime = 0;
            // 게임 종료 함수 호출하면 될 듯
            timerText.color = Color.red;
            // 이전에 실행 중인 코루틴이 있으면 중단
            if (movingNextSceneCoroutine != null)
            {
                Debug.Log("실행 중인 코루틴 있음");
                StopCoroutine(movingNextSceneCoroutine);
            }

            Debug.Log("코루틴 실행");
            movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(1.0f);
        Debug.Log("1초지남");
        SceneManager.LoadScene(3);
        Debug.Log("로드3씬");

        Debug.Log("코루틴 초기화");
        //  씬 로드 후 코루틴 참조를 초기화
        movingNextSceneCoroutine = null;
    }
}
