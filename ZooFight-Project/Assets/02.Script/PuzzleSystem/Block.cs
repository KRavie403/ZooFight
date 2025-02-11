using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockNum; // 블록 번호
    public int type;     // 블록 타입
    public Vector3 position; // 초기 위치

    // 초기화 함수
    public void Initialize(int blockNum, int type, Vector3 position)
    {
        this.blockNum = blockNum;
        this.type = type;
        this.position = position;

        // 블록의 실제 위치를 설정
        transform.position = position;
    }


    private void SendPositionToServer()
    {
        Debug.Log($"블록 이동 전송: 번호={blockNum}, 위치={position}");
        // 뒤끝 매치로 전송
    }
}
