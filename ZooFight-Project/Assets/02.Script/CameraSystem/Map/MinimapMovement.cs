using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MinimapMovement : MonoBehaviour
{
    private float moveSpeed = 45f;
    private float boundary = 20f;

    private float boundaryXMin = 81f;
    private float boundaryXMax = 269f;
    private float boundaryYMin = 36;
    private float boundaryYMax = 213;


    private void Update()
    {
        MinimapHandler();
        HandleKeyboardInput();
    }

    public void MinimapHandler()
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

    private void HandleKeyboardInput()
    {
        // 키보드 입력 감지
        float horizontal = 0;
        float vertical = 0;

        // 화살표 키 입력
        if (Input.GetKey(KeyCode.LeftArrow))
            horizontal -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            horizontal += 1;
        if (Input.GetKey(KeyCode.UpArrow))
            vertical += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            vertical -= 1;

        // WASD 키 입력
        if (Input.GetKey(KeyCode.A))
            horizontal -= 1;
        if (Input.GetKey(KeyCode.D))
            horizontal += 1;
        if (Input.GetKey(KeyCode.W))
            vertical += 1;
        if (Input.GetKey(KeyCode.S))
            vertical -= 1;

        Vector3 moveDirection = new Vector3(horizontal, vertical, 0).normalized;
        Vector3 targetPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;

        // 경계값을 넘어가지 않도록 목표 위치 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, boundaryXMin + boundary, boundaryXMax - boundary);
        targetPosition.y = Mathf.Clamp(targetPosition.y, boundaryYMin + boundary, boundaryYMax - boundary);

        // 부드러운 이동을 위해 Lerp 사용
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
