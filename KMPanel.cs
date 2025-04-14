using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KMPanel : MonoBehaviour
{
    public GameObject KeyPanel;

    void Start()
    {
        if (KeyPanel != null)
        {
            KeyPanel.SetActive(false);
        }
    }

    public void OnClickKeyManager()
    {
        Debug.Log("키설정");

        if (KeyPanel != null)
        {
            KeyPanel.SetActive(true);
        }
    }

    public void OnClickCloseKeyManager()
    {
        if (KeyPanel != null)
        {
            KeyPanel.SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
