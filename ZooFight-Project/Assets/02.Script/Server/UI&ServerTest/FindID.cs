using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;
using System.Text;

public class FindID : LoginBase
{
    [SerializeField]
    private Image imageEmail;
    [SerializeField]
    private TMP_InputField inputFieldEmail;
    [SerializeField]
    private Button btnFindID;

    public void OnClickFindID()
    {
        ResetUI(imageEmail);

        if (isFieldDataEmpty(imageEmail, inputFieldEmail.text, "���� �ּ�")) return;

        if (!inputFieldEmail.text.Contains("@"))
        {
            GuideForIncorrectlyEnteredData(imageEmail, "���� ������ �߸��Ǿ����ϴ�.");
            return;
        }

        btnFindID.interactable = false;
        SetMessage("���� �߼� ���Դϴ�.");
    }

    private void FindCustomID()
    {
        Backend.BMember.FindCustomID(inputFieldEmail.text, callback =>
        {
            btnFindID.interactable = true;

            if (callback.IsSuccess())
            {
                SetMessage($"{inputFieldEmail.text}�ּҷ� ������ �߼��Ͽ����ϴ�.");
            }
            else
            {
                string message = string.Empty;

                switch (int.Parse(callback.GetStatusCode()))
                {
                    case 404:
                        message = "�ش� �̸����� ����ϴ� ����ڰ� �����ϴ�.";
                        break;
                    case 429:
                        message = "24�ð� �̳��� 5ȸ �̻� ���̵� / ��й�ȣ ã�⸦ �õ��߽��ϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
                }

                if (message.Contains("�̸���"))
                {
                    GuideForIncorrectlyEnteredData(imageEmail, message);
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
        ResetUI(imageEmail);
    }
}
