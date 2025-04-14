using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource;

    public AudioSource btnsource;

    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume; //인수를 받아서 오디오 소스의 볼륨을 조절
    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume; 
    }

    public void OnSfx()
    {
        btnsource.Play(); //오디오를 재생하는 함수
    }

}
