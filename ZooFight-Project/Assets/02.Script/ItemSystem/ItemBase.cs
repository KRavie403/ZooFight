using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ItemBase : ItemProperty , IItems , IEffect
{

    // ������ ��� ��ü
    [SerializeField] protected PlayerController myPlayer = null;
    // ������ ���� ����
    public IEnumerator ItemAction;
    // ������ ���� ���� Ȯ��
    public bool isItemUseEnd = false;

    // ���� ���� �ʿ�� �޾Ƶδ� ĭ
    protected Vector3 dir = Vector3.zero;

    EffectCode IEffect.EffectCode => effectCode;

    

    void IItems.ItemUse()
    {

    }

    protected virtual void Awake()
    {

    }


    protected virtual void Start()
    {

    }


    protected virtual void Update()
    {

    }

    #region ������ ��������
    #region �������� 

    // ��� ���� �� ��������
    public virtual void Initate(List<float> Values, PlayerController player)
    {
        if (Values == null) return;
        if (Values.Count > 5) return;

        Value1 = Values[0];
        Value2 = Values[1];
        Value3 = Values[2];
        Value4 = Values[3];
        Value5 = Values[4];

        myPlayer = player;
        isItemUseEnd = false;
        Debug.Log(Values);
    }

    public void SetTarget(List<GameObject> Target)
    {
        Targets = Target;
    }

    public void SetDir(Vector3 Dir)
    {
        dir = Dir;
    }

    #endregion
    #region ���� ����
    // ��ü���� ���� �����Ҷ� ���
    public List<float> GetValues()
    {
        List<float> Values = new List<float>
        {
            Value1,
            Value2,
            Value3,
            Value4,
            Value5
        };
        return Values;
    }
    public EffectCode GetEffectCode () { return effectCode; }
    public SoundCode GetSoundCode () { return soundCode; }

    #endregion
    #endregion


    #region ������ ��� ����

    // ������ �ߵ��� ����
    public virtual void ItemUse()
    {
        if (myPlayer == null) return;
        


    }

    // Ÿ�������� �ִ� �������� Ÿ�ݽ� ����
    public virtual void ItemHitAction()
    {

    }

    // �������� ������ ������ ����
    public virtual void ItemEnd()
    {
        // ���� ���� ����
        isItemUseEnd = true;

        // ������ ����
        myPlayer.curItems = null;
        myPlayer = null;
        ItemAction = null;
        dir = Vector3.zero;
        GetComponent<HitScanner>().Reset();


    }
    
    public virtual IEnumerator ItemActions()
    {
        bool A = false;

        // ��� ������
        while (!A)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            { 
                A = true;
            }

            Debug.Log("AA");
            //A = true;
            yield return null;
        }

    }



    #endregion
}
