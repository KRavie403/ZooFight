using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScenario : MonoBehaviour
{
    [SerializeField]
    private UserInfo user;

    private void Awake()
    {
        user.GetUserInfoFromBackend();
    }

    private void Start()
    {
        BackendGameData.Inst.GameDataLoad();
    }
}
