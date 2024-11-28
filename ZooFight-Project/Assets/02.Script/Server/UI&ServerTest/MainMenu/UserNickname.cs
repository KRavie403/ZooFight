using UnityEngine;
using TMPro;

public class UserNickname : MonoBehaviour
{
    public static UserNickname instance;
    [SerializeField]
    private TextMeshProUGUI textNickname;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public void UpdateNickname()
    {
        // 닉네임 출력
        textNickname.text = UserInfo.Data.nickname;
    }
}
