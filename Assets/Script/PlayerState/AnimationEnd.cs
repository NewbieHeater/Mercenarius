using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : MonoBehaviour
{
    private static AnimationEnd instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static AnimationEnd Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public bool attackAnimationEnded = false;

    public void atkStart()
    {
        attackAnimationEnded = false;
    }

    private void atkEnd()
    {
        attackAnimationEnded = true;
    }

}