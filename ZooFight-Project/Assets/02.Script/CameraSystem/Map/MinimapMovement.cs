using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MinimapMovement : MonoBehaviour
{
    private float moveSpeed = 80f;
    private float boundary = 20f;

    private float boundaryXMin = 50f;
    private float boundaryXMax = 155f;
    private float boundaryYMin = 40f;
    private float boundaryYMax = 100f;

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
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        else if (mousePos.y > Screen.height - boundary)
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // ��谪�� �Ѿ�� �ʵ��� ����
        float clampedX = Mathf.Clamp(transform.position.x, boundaryXMin + 20, boundaryXMax - 20);
        float clampedY = Mathf.Clamp(transform.position.y, boundaryYMin + 20, boundaryYMax - 20);

        transform.position = new Vector3(clampedX, clampedY, 0);
    }
}
