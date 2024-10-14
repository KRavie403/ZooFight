using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSize : MonoBehaviour
{
    public GameObject Walls;
    public Camera MinimapCamera;
    public Camera MinimapMainCamera;

    private int lastScreenWidth;
    private int lastScreenHeight;
    private Vector3 wallsCenter;


    private void Start()
    {
        // 초기 해상도 저장
        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        // Walls 오브젝트의 중점에서 x와 z 값을 계산
        wallsCenter = CalculateWallsCenter(Walls);

        // 시작 시점에 카메라 위치를 설정
        UpdateMinimapCameraPosition();
    }

    void Update()
    {
        // 해상도가 변경되었는지 확인
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            Debug.Log("해상도 변경됨");
            // 해상도가 변경되었으므로 카메라 위치를 업데이트
            //UpdateMinimapCameraPosition();

            // 현재 해상도로 값을 업데이트
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
        }
    }

    void UpdateMinimapCameraPosition()
    {
        // Minimap 카메라의 위치를 Walls의 중점으로 설정 (x, z 값만 반영)
        //MinimapCamera.transform.position = new Vector3(wallsCenter.x, MinimapCamera.transform.position.y, wallsCenter.z);
        //MinimapCamera.transform.position = new Vector3(0, MinimapCamera.transform.position.y, 0);
    }

    Vector3 CalculateWallsCenter(GameObject walls)
    {
        Renderer[] renderers = walls.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return walls.transform.position; // 자식이 없으면 그냥 Walls의 위치 반환

        Vector3 center = Vector3.zero;
        foreach (Renderer renderer in renderers)
        {
            center += renderer.bounds.center;
        }
        center /= renderers.Length; // 모든 중심점의 평균을 계산
        return center;
    }
}
