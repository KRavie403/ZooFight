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

        if (isFieldDataEmpty(imageNickname, inputFieldNickName.text, "�г���")) return;
        //btnNicknameAccept.interactable = false;
        SetMessage("�г��� Ȯ�� �� . . . ");

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
                SetMessage("��밡���� �г����Դϴ�.");
                return;
            }
            else
            {
                string message = string.Empty;
                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 400:
                        message = "�г����� ����ְų� 20�� �̻��̰ų� ��/�� ������ �ֽ��ϴ�.";
                        break;
                    case 409:
                        message = "�̹� �����ϴ� �г����Դϴ�.";
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

        if (isFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (isFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;
        if (isFieldDataEmpty(imageConfirmPW, inputFieldConfirmPW.text, "��й�ȣ Ȯ��")) return;
        if (isFieldDataEmpty(imageEmail, inputFieldEmail.text, "�̸���")) return;
        if (isFieldDataEmpty(imageNickname, inputFieldNickName.text, "�г���")) return;

        //if (!nickNameAccept)
        //{
        //    SetMessage("�г��� Ȯ�� ��ư�� �����ּ���.");
        //    return;
        //}

        if (!inputFieldPW.text.Equals(inputFieldConfirmPW.text))
        {
            GuideForIncorrectlyEnteredData(imageConfirmPW, "��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.(ex. adress@xx.xx");
            return;
        }

        btnRegisterAccount.interactable = false;
        SetMessage("���� �������Դϴ�. . .");

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
                        SetMessage($"���� ���� ����. {inputFieldNickName.text}�� ȯ���մϴ�.");
                        Backend.BMember.UpdateNickname(inputFieldNickName.text);


                    }
                });
            }
            else
            {
                string message = string.Empty;

                 switch (int.Parse(callback.GetStatusCode()) )
                {
                    case 409:
                        message = "�̹� �����ϴ� ���̵��Դϴ�.";
                        break;
                    case 403:
                    case 401:
                    case 400:
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("���̵�"))
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
