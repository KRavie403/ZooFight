using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeSetting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

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
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Ÿ�̸�
}
