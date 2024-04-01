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

        // 이전에 실행 중인 코루틴이 있으면 중단
        if (movingNextSceneCoroutine != null)
        {
            Debug.Log("실행 중인 코루틴 있음");
            StopCoroutine(movingNextSceneCoroutine);
        }

        Debug.Log("코루틴 실행");
       
        movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        Debug.Log("MovingNextScene");

        yield return new WaitForSeconds(5.0f);
        Debug.Log("5초지남");
        SceneManager.LoadScene(2);
        Debug.Log("로드2씬");

        Debug.Log("코루틴 초기화");
        //  씬 로드 후 코루틴 참조를 초기화
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
