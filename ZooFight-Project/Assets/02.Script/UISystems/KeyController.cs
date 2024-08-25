using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public Button[] keyButtons;
    public KeySettingDecoder InputSettingDecoder;

    private KeyAction currentKeyAction;
    private bool isListeningForKey = false; // 키 입력 대기 중인지 여부

    private void Start()
    {
        for (int i = 0; i < keyButtons.Length; i++)
        {
            KeyAction action = (KeyAction)i;
            keyButtons[i].GetComponentInChildren<Text>().text = KeySetting.keys[action].ToString();
            keyButtons[i].onClick.AddListener(() => OnKeyButtonClick(action));
        }

        // 저장된 키 설정을 불러오기
        LoadSavedKeys();
    }

    private void Update()
    {
        if (isListeningForKey)
        {
            // 모든 키코드를 검사하여 어떤 키가 눌렸는지 확인
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    // 중복된 키가 아닌지 확인
                    if (IsKeyCodeAlreadyAssigned(keyCode))
                    {
                        Debug.LogWarning($"{keyCode} 키는 이미 할당된 키입니다.");
                        return;
                    }

                    // 키 설정을 변경하고 UI 텍스트 업데이트
                    KeySetting.keys[currentKeyAction] = keyCode;
                    keyButtons[(int)currentKeyAction].GetComponentInChildren<Text>().text = keyCode.ToString();

                    // 변경된 키 저장
                    SaveKeySetting(currentKeyAction, keyCode);

                    isListeningForKey = false;
                    break;
                }
            }
        }
    }

    private void OnKeyButtonClick(KeyAction action)
    {
        currentKeyAction = action;
        isListeningForKey = true;
        Debug.Log($"{action}을 설정하기 위해 아무 키나 눌러주세요.");
    }

    private void SaveKeySetting(KeyAction action, KeyCode keyCode)
    {
        if (InputSettingDecoder != null)
        {
            InputSettingDecoder.SavedKeyCodes[(int)action] = (int)keyCode;
        }
    }

    private bool IsKeyCodeAlreadyAssigned(KeyCode keyCode)
    {
        // 현재 설정된 키들 중에서 이미 사용 중인 키코드가 있는지 확인
        foreach (var entry in KeySetting.keys)
        {
            if (entry.Value == keyCode)
            {
                return true; // 중복된 키코드가 존재
            }
        }
        return false; // 중복된 키코드가 없음
    }

    public void LoadSavedKeys()
    {
        if (InputSettingDecoder != null)
        {
            //InputSettingDecoder.LoadSaveKeys(); // 저장된 키 설정을 로드
            InputSettingDecoder.SavedCodeDecode(InputSettingDecoder.SavedKeyCodes);

            // 저장된 키 값을 UI에 반영
            for (int i = 0; i < keyButtons.Length; i++)
            {
                KeyAction action = (KeyAction)i;
                KeyCode keyCode = (KeyCode)InputSettingDecoder.SavedKeyCodes[i];
                KeySetting.keys[action] = keyCode;
                keyButtons[i].GetComponentInChildren<Text>().text = keyCode.ToString();
            }
        }
#if DEBUG
        else
        {
            Debug.LogWarning("InputSettingDecoder가 할당되지 않음");
        }
#endif
    }
}
