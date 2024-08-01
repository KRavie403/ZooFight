using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private float boundary = 20f;

    // 정사각형의 테두리 정보
    public float boundaryXMin = 13.7f;
    public float boundaryXMax = 46.3f;
    public float boundaryZMin = 6.0f;
    public float boundaryZMax = 33.2f;

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
            transform.Translate(Vector3.down * moveSpeed * 0.8f * Time.deltaTime);
        else if (mousePos.y > Screen.height - boundary)
            transform.Translate(Vector3.up * moveSpeed * 0.8f * Time.deltaTime);

        // 경계값을 넘어가지 않도록 제한
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, boundaryXMin, boundaryXMax),
            Mathf.Clamp(transform.position.y, -40, 40),
            Mathf.Clamp(transform.position.z, boundaryZMin, boundaryZMax)
        );
    }
}