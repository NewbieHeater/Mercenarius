using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class MainMenu : MonoBehaviour
{
    public GameObject OptionPanel;

    void Start()
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(false); 
        }
    }

    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
    }

    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }

    public void OnClickOption()
    {
        Debug.Log("옵션");

        if (OptionPanel != null)
        {
            OptionPanel.SetActive(true); 
        }
    }

    public void ClosePanel()
    {
        if (OptionPanel != null)
        {
            OptionPanel.SetActive(false); 
        }
    }

    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
