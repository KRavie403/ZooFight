using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    private static Gamemanager inst;
    public static Gamemanager Inst => inst;

    public PlayerController currentPlayer;

    public void Awake()
    {
        if (inst == null)
        {
            inst = FindObjectOfType<Gamemanager>();     // ���� ���� �� �ڱ� �ڽ��� ����
            DontDestroyOnLoad(this.gameObject);         // �� ��ȯ�� ������ ���� �ʰ� ����

        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
