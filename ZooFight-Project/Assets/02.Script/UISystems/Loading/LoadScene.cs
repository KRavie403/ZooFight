using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private Coroutine movingNextSceneCoroutine;

    private void Start()
    {
        // ������ ���� ���� �ڷ�ƾ�� ������ �ߴ�
        if (movingNextSceneCoroutine != null)
        {
            Debug.Log("���� ���� �ڷ�ƾ ����");
            StopCoroutine(movingNextSceneCoroutine);
        }

        Debug.Log("�ڷ�ƾ ����");
        movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(3.0f);
        Debug.Log("3������");
        SceneManager.LoadScene(2);
        Debug.Log("�ε�2��");

        Debug.Log("�ڷ�ƾ �ʱ�ȭ");
        //  �� �ε� �� �ڷ�ƾ ������ �ʱ�ȭ
        movingNextSceneCoroutine = null;
    }
}
