using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����۸� : ���׹̳ʵ帵ũ
/// Value 1 ���׹̳� ȸ����
/// Value 2 ���׹̳� ȸ���ð� - �̻�� ���ɼ� ����
/// 
/// </summary>

public class Item_StaminaDrink : Items
{


    protected override void Awake()
    {
        base.Awake();

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    public override void ItemUse(PlayerController player)
    {
        base.ItemUse(player);
        base.ItemAction = DrinkAction();
    }


    // ��ǵ��۵��� ȸ�� & �ִϸ��̼� & ����Ʈ ��µ���
    public IEnumerator DrinkAction()
    {
        // ȸ��


        // �ִϸ��̼�

        // ����Ʈ


        while (true)
        {
            yield return null;
        }
    }


}
