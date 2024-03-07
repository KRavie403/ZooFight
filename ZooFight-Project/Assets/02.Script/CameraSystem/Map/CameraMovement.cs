using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float boundary = 20f;

    // ���簢���� �׵θ� ����
    private float boundaryXMin = 20.7f;
    private float boundaryXMax = 39.3f;
    private float boundaryZMin = 11.3f;
    private float boundaryZMax = 29.5f;

    void Update()
    {
        // ���콺 ��ġ ����
        Vector2 mousePos = Input.mousePosition;

        // �̵� ���� �˻� �� �̵�
        if (mousePos.x < boundary)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        else if (mousePos.x > Screen.width - boundary)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (mousePos.y < boundary)
            transform.Translate(Vector3.down * moveSpeed * 0.8f * Time.deltaTime);
        else if (mousePos.y > Screen.height - boundary)
            transform.Translate(Vector3.up * moveSpeed * 0.8f * Time.deltaTime);

        // ��谪�� �Ѿ�� �ʵ��� ����
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundaryXMin, boundaryXMax),
            Mathf.Clamp(transform.position.y, -40, 40),
            Mathf.Clamp(transform.position.z, boundaryZMin, boundaryZMax)
        );
    }
}
