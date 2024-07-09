using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MinimapMovement : MonoBehaviour
{
    public float moveSpeed = 59;
    private float boundary = 20f;

    public float boundaryXMin = 114;
    public float boundaryXMax = 352;
    public float boundaryYMin = 48;
    public float boundaryYMax = 278;

    private void Start()
    {
        transform.localPosition = Vector3.zero;
    }

    void Update()
    {
        // 마우스 위치 감지
        Vector2 mousePos = Input.mousePosition;

        // 목표 위치 설정
        Vector3 targetPosition = transform.position;


        // 이동 조건 검사 및 목표 위치 갱신
        if (mousePos.x < boundary)
            targetPosition += Vector3.left * moveSpeed * Time.deltaTime;
        else if (mousePos.x > Screen.width - boundary)
            targetPosition += Vector3.right * moveSpeed * Time.deltaTime;

        if (mousePos.y < boundary)
            targetPosition += Vector3.down * moveSpeed * Time.deltaTime;
        else if (mousePos.y > Screen.height - boundary)
            targetPosition += Vector3.up * moveSpeed * Time.deltaTime;

        // 경계값을 넘어가지 않도록 목표 위치 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryXMin + boundary, boundaryXMax - boundary);
        targetPosition.y = Mathf.Clamp(targetPosition.y, boundaryYMin + boundary, boundaryYMax - boundary);

        // 부드러운 이동을 위해 Lerp 사용
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
