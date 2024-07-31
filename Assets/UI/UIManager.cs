using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    Debug.LogError("UIManager�� ���� �������� �ʽ��ϴ�.");
                }
            }
            return instance;
        }
    }
    
    /*
    private Stack<UIPopup> openPopups = new Stack<UIPanel>();
    private Queue<UIPopup> pendingPopups = new Queue<UIPanel>(); // ����� �˾��� ���� ť

    private void Update()
    {
        // �ڷΰ��� Ű�� ������ ���� �ֱٿ� ���� �˾��� �ݽ��ϴ�.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseLastOpenedPopup();
        }
    }

    // �˾��� ���ϴ�.
    public void OpenPopup(UIPanel popup)
    {
        if (popup != null)
        {
            // ���ο� �˾��� ���ϴ�.
            UIUtilities.SetUIActive(popup.gameObject, true);
            openPopups.Push(popup);
        }
    }

    // �˾��� �ݽ��ϴ�.
    public void ClosePopup(UIPanel popup)
    {
        if (popup != null && openPopups.Contains(popup))
        {
            // �˾��� �ݽ��ϴ�.
            UIUtilities.SetUIActive(popup.gameObject, false);
            openPopups.Pop();

            // �˾��� ���� �� ����� �˾��� �ִٸ� ���ϴ�.
            if (pendingPopups.Count > 0)
            {
                OpenPopup(pendingPopups.Dequeue());
            }
        }
    }

    // ���� �ֱٿ� ���� �˾��� �ݽ��ϴ�.
    public void CloseLastOpenedPopup()
    {
        if (openPopups.Count > 0)
        {
            ClosePopup(openPopups.Peek());
        }
    }

    // ��� ���� �˾��� �ݽ��ϴ�.
    public void CloseAllOpenPopups()
    {
        while (openPopups.Count > 0)
        {
            ClosePopup(openPopups.Peek());
        }
    }

    // ����� �˾��� �߰��մϴ�.
    public void ReservePopup(UIPanel popup)
    {
        if (popup != null)
        {
            pendingPopups.Enqueue(popup);
        }
    }
    */
}