using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public Button[] btns;

    private void Update()
    {
        ESC();
        Q();
    }

    // ���� ȭ������ ������
    public void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    // ���Ī�ϱ�
    public void Q()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("LoadingScene");
        }
    }
}
