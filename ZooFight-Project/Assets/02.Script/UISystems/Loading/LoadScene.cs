using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyUI.Progress;

public class LoadScene : MonoBehaviour
{
    WaitForSeconds waitsec = new WaitForSeconds(2.0f);
    private Coroutine movingNextSceneCoroutine;

    public Animator anim;

    [SerializeField]
    private RawImage _uiRawImage;

    private void Start()
    {
        LoadLoadingImg();

        // 이전에 실행 중인 코루틴이 있으면 중단
        if (movingNextSceneCoroutine != null)
        {
            StopCoroutine(movingNextSceneCoroutine);
        }
       
        movingNextSceneCoroutine = StartCoroutine(MovingNextScene());
    }

    IEnumerator MovingNextScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync("GameScene");
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;

            if(op.progress >= 0.9f)
            {
                yield return waitsec;
                op.allowSceneActivation = true;
                yield break;
            }
        }
        //  씬 로드 후 코루틴 참조를 초기화
        movingNextSceneCoroutine = null;
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
