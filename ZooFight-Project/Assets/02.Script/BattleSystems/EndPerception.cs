using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPerception : MonoBehaviour
{
    [SerializeField] private PlayerController _animatorController;
    [SerializeField] private GameObject _successParticlePrefab;


    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collided with: " + collision.gameObject.name + ", Layer: " + LayerMask.LayerToName(collision.gameObject.layer));
        // Block 레이어와 충돌했을 때 애니메이션 실행
        if (collision.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            StartCoroutine(LoadSceneAfterDelay());

            //_animatorController.PlaySuccess();
            //SoundManager.Instance.PlaySFX("Success");
            GameObject particleInstance = Instantiate(_successParticlePrefab, transform.position + Vector3.up * 10f, Quaternion.identity);
            Destroy(particleInstance, 2f);
        }
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("LobbyScene");
    }
}
