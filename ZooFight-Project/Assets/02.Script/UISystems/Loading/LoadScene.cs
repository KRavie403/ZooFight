using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyUI.Progress;

public class LoadScene : MonoBehaviour
{
    private Coroutine movingNextSceneCoroutine;

    public Animator anim;

    [SerializeField]
    private RawImage _uiRawImage;

    private void Start()
    {
        //anim = GetComponent<Animator>();
        LoadLoadingImg();

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

        yield return new WaitForSeconds(5.0f);
        Debug.Log("5������");
        SceneManager.LoadScene(2);
        Debug.Log("�ε�2��");

        Debug.Log("�ڷ�ƾ �ʱ�ȭ");
        //  �� �ε� �� �ڷ�ƾ ������ �ʱ�ȭ
        movingNextSceneCoroutine = null;
    }

    public void LoadLoadingImg()
    {
        Debug.Log("Loading Started");
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
