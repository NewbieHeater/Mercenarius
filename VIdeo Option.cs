using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VIdeoOption : MonoBehaviour
{
    FullScreenMode screenMode;
    public Dropdown resolutionDropdowm;
    public Toggle fullscreenBtn;
    List<Resolution> resolutions = new List<Resolution>();
    int resolutionNum;
    // Start is called before the first frame update
    void Start()
    {
        InitUI();
    }

    // Update is called once per frame
    void InitUI()
    {
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if(Screen.resolutions[i].refreshRate == 60)
               resolutions.Add(Screen.resolutions[i]);
        }

        resolutionDropdowm.options.Clear();

        int optionNum = 0;
        foreach (Resolution item in resolutions)
        {
            Dropdown.OptionData option = new Dropdown.OptionData();
            option.text = item.width + "x" + item.height + " " + item.refreshRate + "hz";
            resolutionDropdowm.options.Add(option);

            if (item.width == Screen.width && item.height == Screen.height)
                resolutionDropdowm.value = optionNum;
            optionNum++;
        }
        resolutionDropdowm.RefreshShownValue(); //옵션지가 변경되므로 새로고침 함수를 실행

        fullscreenBtn.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }

    public void DropboxOptionChange(int x)
    {
        resolutionNum = x; 
    }

    public void FullScreenBtn(bool isFull)
    {
        screenMode = isFull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed; //isFull이 참이면 전체화면 거짓이면 창
    }

    public void OnClickBtn()
    {
        Screen.SetResolution(resolutions[resolutionNum].width,
            resolutions[resolutionNum].height,
            screenMode);
    }
}
