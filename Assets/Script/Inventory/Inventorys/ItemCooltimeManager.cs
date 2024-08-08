using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCooltimeManager : Singleton<ItemCooltimeManager>
{
    private Dictionary<int, float> mCooltimes; //돌고있는 쿨타임들을 가지고있는 딕셔너리
    private List<int> mCooltimeList; //현재 돌고있는 쿨타임들의 아이템 코드를 담는 리스트

    private float mTempCooltime; //임시 저장용 변수

    private void Start()
    {
        mCooltimes = new Dictionary<int, float>();
        mCooltimeList = new List<int>();
    }

    private void Update()
    {
        //현재 리스트에 들어있는 모든 요소들을 돌면서 쿨타임을 확인
        for (int i = mCooltimeList.Count - 1; i >= 0; --i)
        {
            //매 프레임마다 쿨타임 갱신
            mTempCooltime = mCooltimes[mCooltimeList[i]] = mCooltimes[mCooltimeList[i]] - Time.deltaTime;

            //쿨타임이 끝났다면 리스트에서 요소 제거
            if (mTempCooltime < 0) { mCooltimeList.RemoveAt(i); }
        }
    }

    /// <summary>
    /// 쿨타임 대기열에 아이템코드가 itemID인 대상의 쿨타임을 시작한다
    /// </summary>
    /// <param name="itemID">아이템 코드</param>
    /// <param name="originCooltime">해당 아이템의 오리진 쿨타임</param>
    public void AddCooltimeQueue(int itemID, float originCooltime)
    {
        mCooltimes.TryAdd(itemID, originCooltime);

        mCooltimes[itemID] = originCooltime;
        mCooltimeList.Add(itemID);
    }

    /// <summary>
    /// 해당 아이템의 남은 쿨타임 시간을 가져온다
    /// </summary>
    /// <param name="itemID">확인할 쿨타임의 아이템</param>
    /// <returns></returns>
    public float GetCurrentCooltime(int itemID)
    {
        float cooltime;
        bool isSuccess = mCooltimes.TryGetValue(itemID, out cooltime);

        if (isSuccess) { return cooltime; }
        else { return 0; }
    }
}