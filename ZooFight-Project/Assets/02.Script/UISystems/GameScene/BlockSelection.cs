using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockSelection : MonoBehaviour
{
    private GameObject selectedBlock; // 현재 선택된 블록
    private Material outlineMaterial; // 외곽선 Material
    private int blockLayerMask; // Block 레이어에 대한 마스크

    void Start()
    {
        // "Outline" Material을 로드
        outlineMaterial = Resources.Load<Material>("Outline");

        // "Block" 레이어 마스크를 설정
        blockLayerMask = LayerMask.GetMask("Block");
    }

    void Update()
    {
        // 마우스 왼쪽 버튼을 클릭했을 때
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    private void HandleClick()
    {
        // 마우스 위치에서 레이 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // "Block" 레이어에 대해서만 레이캐스트를 수행
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask))
        {
            GameObject clickedBlock = hit.collider.gameObject;

            // 이전에 선택된 블록의 테두리를 제거
            if (selectedBlock != null && selectedBlock != clickedBlock)
            {
                RemoveOutline(selectedBlock);
            }

            // 클릭된 블록을 새로운 선택으로 설정하고 테두리 추가
            selectedBlock = clickedBlock;
            AddOutline(selectedBlock);
        }
    }

    private void AddOutline(GameObject block)
    {
        if (outlineMaterial == null)
        {
            Debug.LogWarning("Outline material 을 찾을 수 없습니다.");
            return;
        }

        // 블록의 Renderer에서 기존 Materials를 가져오기
        var renderer = block.GetComponent<Renderer>();
        var materials = renderer.materials;

        // 기존 Materials 배열에 outlineMaterial을 추가
        Material[] newMaterials = new Material[materials.Length + 1];
        materials.CopyTo(newMaterials, 0);
        newMaterials[newMaterials.Length - 1] = outlineMaterial;

        renderer.materials = newMaterials;
    }

    private void RemoveOutline(GameObject block)
    {
        var renderer = block.GetComponent<Renderer>();
        var materials = renderer.materials;

        // Outline Material 유무 확인 및 제거
        int index = System.Array.FindIndex(materials, m => m == outlineMaterial);
        if (index != -1)
        {
            Material[] newMaterials = new Material[materials.Length - 1];
            for (int i = 0, j = 0; i < materials.Length; i++)
            {
                if (i != index)
                {
                    newMaterials[j++] = materials[i];
                }
            }
            renderer.materials = newMaterials;
        }
    }
}
