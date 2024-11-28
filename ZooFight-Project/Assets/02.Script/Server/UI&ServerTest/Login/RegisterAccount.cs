using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using Unity.VisualScripting;
using static BackEnd.SendQueue;

public class RegisterAccount : LoginBase
{
    [SerializeField]
    private Image imageID;
    [SerializeField]
    private TMP_InputField inputFieldID;
    [SerializeField]
    private Image imagePW;
    [SerializeField]
    private TMP_InputField inputFieldPW;
    [SerializeField]
    private Image imageConfirmPW;
    [SerializeField]
    private TMP_InputField inputFieldConfirmPW;
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;
    [SerializeField]
    private Image imageNickname;
    [SerializeField]
    private TMP_InputField inputFieldNickName;

    //[SerializeField]
    //private Button btnNicknameAccept;
    [SerializeField]
    private Button btnRegisterAccount;

    //private bool nickNameAccept=false;

    public void OnClickNicknameAccept()
    {
        ResetUI(imageNickname);

        if (isFieldDataEmpty(imageNickname, inputFieldNickName.text, "닉네임")) return;
        //btnNicknameAccept.interactable = false;
        SetMessage("닉네임 확인 중 . . . ");

        NicknameAccept();
    }

    private void NicknameAccept()
    {
        Backend.BMember.UpdateNickname(inputFieldNickName.text, callback =>
        {
            //btnNicknameAccept.interactable = true;

            if (callback.IsSuccess())
            {
                //nickNameAccept = true;
                SetMessage("사용가능한 닉네임입니다.");
                return;
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message = "닉네임이 비어있거나 20자 이상이거나 앞/뒤 공백이 있습니다.";
                        break;
                    case 409:
                        message = "이미 존재하는 닉네임입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }
                GuideForIncorrectlyEnteredData(imageNickname, message);
            }
        });
    }

    public void OnClickRegisterAccount()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail, imageNickname);

        if (isFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (isFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;
        if (isFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "비밀번호 확인")) return;
        if (isFieldDataEmpty(imageEmail, inputFieldEmail.text, "이메일")) return;
        if (isFieldDataEmpty(imageNickname, inputFieldNickName.text, "닉네임")) return;

        //if (!nickNameAccept)
        //{
        //    SetMessage("닉네임 확인 버튼을 눌러주세요.");
        //    return;
        //}

        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "비밀번호가 일치하지 않습니다.");
            return;
        }

        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "메일 형식이 잘못되었습니다.(ex. adress@xx.xx");
            return;
        }

        btnRegisterAccount.interactable = false;
        SetMessage("계정 생성중입니다. . .");

        CustomSignUp();
    }

    private void CustomSignUp()
    {
        Backend.BMember.CustomSignUp(inputFieldID.text, inputFieldPW.text, callback =>
        {
            btnRegisterAccount.interactable = true;

            if (callback.IsSuccess())
            {
                Backend.BMember.UpdateCustomEmail(inputFieldEmail.text, callback =>
                {
                    if (callback.IsSuccess())
                    {
                        SetMessage($"계정 생성 성공. {inputFieldNickName.text}님 환영합니다.");
                        Backend.BMember.UpdateNickname(inputFieldNickName.text);

                        // 계정 생성 성공 시 해당 계정의 게임 정보 생성
                        BackendGameData.Inst.GameDataInsert();
                    }
                });
            }
            else
            {
                string message = string.Empty;

                 switch (int.Parse(callback.GetStatusCode()) )
                {
                    case 409:
                        message = "이미 존재하는 아이디입니다.";
                        break;
                    case 403:
                    case 401:
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("아이디"))
                {
                    GuideForIncorrectlyEnteredData(imageID, message);
                }
                else
                {
                    SetMessage(message);
                }
            }
        });
    }

    public void InputFieldClear()
    {
        ResetUI(imageID, imagePW, imageConfirmPW, imageEmail, imageNickname);
    }
}
