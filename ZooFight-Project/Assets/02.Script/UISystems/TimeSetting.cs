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
            // ���� ���� �Լ� ȣ���ϸ� �� ��
            timerText.color = Color.red;
            // ������ ���� ���� �ڷ�ƾ�� ������ �ߴ�
            if (movingNextSceneCoroutine != null)
            {
                Debug.Log("���� ���� �ڷ�ƾ ����");
                StopCoroutine(movingNextSceneCoroutine);
            }

            Debug.Log("�ڷ�ƾ ����");
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
        Debug.Log("1������");
        SceneManager.LoadScene(3);
        Debug.Log("�ε�3��");

        Debug.Log("�ڷ�ƾ �ʱ�ȭ");
        //  �� �ε� �� �ڷ�ƾ ������ �ʱ�ȭ
        movingNextSceneCoroutine = null;
    }
}
