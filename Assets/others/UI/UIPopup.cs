using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupCanvas; // �˾� â�� ĵ����
    [SerializeField] private Animator popupAnimator; // �˾� â�� �ִϸ�����

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    public void OnCloseAnimationFinished()
    {
        // �ִϸ��̼� �Ϸ� �� �˾� ĵ������ ��Ȱ��ȭ�մϴ�.
        UIUtilities.SetUIActive(popupCanvas, false);
    }

    // �˾��� ���� �� ȣ��˴ϴ�.
    public void Open()
    {
        if (popupCanvas != null)
        {
            // �˾� ĵ������ Ȱ��ȭ�մϴ�.
            UIUtilities.SetUIActive(popupCanvas, true);

            // �˾� �ִϸ��̼� ��� (�ִϸ��̼� �̺�Ʈ�� OnCloseAnimationFinished ȣ��)
            if (popupAnimator != null)
            {
                popupAnimator.SetTrigger("Open");
            }
        }
    }

    // �˾��� ���� �� ȣ��˴ϴ�.
    public void Close()
    {
        if (popupCanvas != null)
        {
            // �˾� �ִϸ��̼� ��� (�ִϸ��̼� �̺�Ʈ�� OnCloseAnimationFinished ȣ��)
            if (popupAnimator != null)
            {
                popupAnimator.SetTrigger("Close");
            }
            else
            {
                // �ִϸ��̼��� ������� �ʴ� ��� ��� �˾� ĵ������ ��Ȱ��ȭ�մϴ�.
                UIUtilities.SetUIActive(popupCanvas, false);
            }
        }
    }

    // �˾� ������ � ������ ������ �� ȣ��� �޼������ �߰��� �� �ֽ��ϴ�.
    public void OnButtonClicked()
    {
        // �˾� �� ��ư�� Ŭ���Ǿ��� �� ������ ������ ���⿡ �ۼ��մϴ�.
    }
}