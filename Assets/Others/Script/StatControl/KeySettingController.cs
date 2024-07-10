using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class KeySettingController : MonoBehaviour
{
    private KeyCode mOriginKeyCode;
    [SerializeField] private string mKeyBindingName;

    //키 설정 버튼
    [SerializeField] private Image mKeyButtonImage; //현재 할당된 키와 그 키를 수정할 수 있게 하는 버튼의 이미지
    private Coroutine mKeyButtonColorCor; //키 수정 버튼의 색상 변경을 수행하는 코루틴을 담는 변수

    //버튼 텍스트
    [SerializeField] private TextMeshProUGUI mKeyButtonText; //버튼의 하위 자식의 텍스트필드

    private void OnEnable()
    {
        mOriginKeyCode = KeyManager.Instance.GetKeyCode(mKeyBindingName);
        mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();
    }

    public void BTN_ModifyKey()
    {
        mKeyButtonText.text = "< >";

        StartCoroutine(CorAssignKey());
    }

    private IEnumerator CorAssignKey()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        // 기존의 코루틴 제거        
                        if (mKeyButtonColorCor != null) { StopCoroutine(mKeyButtonColorCor); }

                        // 키 설정을 할 수 있는경우?
                        if (KeyManager.Instance.CheckKey(kcode, mOriginKeyCode))
                        {
                            // 키 지정
                            KeyManager.Instance.AssignKey(kcode, mKeyBindingName);
                            mOriginKeyCode = kcode;

                            // 키 레이블을 변경
                            mKeyButtonText.text = ((char)kcode).ToString().ToUpper();

                            // 녹색으로 설정 완료됨을 연출
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.green));
                        }
                        else
                        {
                            // 키 레이블을 변경
                            mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();

                            // 빨간색으로 설정 완료됨을 연출
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.red));
                        }
                    }
                }

                yield break;
            }

            yield return null;
        }
    }

    private IEnumerator CorChangeButtonColor(Color targetColor, float colorSpeed = 2.0f)
    {
        float progress = 0;

        //targetColor로 변경
        while (true)
        {
            mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, targetColor, progress);
            progress += colorSpeed * Time.deltaTime;

            //progress가 1이면 > 보간 완료
            if (progress > 1)
            {
                progress = 0;

                //targetColor에서 다시 돌아오기
                while (true)
                {
                    mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, Color.white, progress);
                    progress += colorSpeed * Time.deltaTime;

                    //색상 전환 완료
                    if (progress > 1)
                    {
                        yield break;
                    }

                    yield return null;
                }
            }

            yield return null;
        }
    }
}