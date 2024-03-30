using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MinimapMovement : MonoBehaviour
{
    private float moveSpeed = 80f;
    private float boundary = 20f;

    private float boundaryXMin = 55f;
    private float boundaryXMax = 305f;
    private float boundaryYMin = 15f;
    private float boundaryYMax = 225f;

    private void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        // 마우스 위치 감지
        Vector2 mousePos = Input.mousePosition;

        // 이동 조건 검사 및 이동
        if (mousePos.x < boundary)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        else if (mousePos.x > Screen.width - boundary)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if (mousePos.y < boundary)
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        else if (mousePos.y > Screen.height - boundary)
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // 경계값을 넘어가지 않도록 제한
        float clampedX = Mathf.Clamp(transform.position.x, boundaryXMin + boundary, boundaryXMax - boundary);
        float clampedY = Mathf.Clamp(transform.position.y, boundaryYMin + boundary, boundaryYMax - boundary);

        transform.position = new Vector3(clampedX, clampedY, 0);
    }
}
