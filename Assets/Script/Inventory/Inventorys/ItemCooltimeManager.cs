using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemCooltimeManager : Singleton<ItemCooltimeManager>
{
    private Dictionary<int, float> mCooltimes; //�����ִ� ��Ÿ�ӵ��� �������ִ� ��ųʸ�
    private List<int> mCooltimeList; //���� �����ִ� ��Ÿ�ӵ��� ������ �ڵ带 ��� ����Ʈ

    private float mTempCooltime; //�ӽ� ����� ����

    private void Start()
    {
        mCooltimes = new Dictionary<int, float>();
        mCooltimeList = new List<int>();
    }

    private void Update()
    {
        //���� ����Ʈ�� ����ִ� ��� ��ҵ��� ���鼭 ��Ÿ���� Ȯ��
        for (int i = mCooltimeList.Count - 1; i >= 0; --i)
        {
            //�� �����Ӹ��� ��Ÿ�� ����
            mTempCooltime = mCooltimes[mCooltimeList[i]] = mCooltimes[mCooltimeList[i]] - Time.deltaTime;

            //��Ÿ���� �����ٸ� ����Ʈ���� ��� ����
            if (mTempCooltime < 0) { mCooltimeList.RemoveAt(i); }
        }
    }

    /// <summary>
    /// ��Ÿ�� ��⿭�� �������ڵ尡 itemID�� ����� ��Ÿ���� �����Ѵ�
    /// </summary>
    /// <param name="itemID">������ �ڵ�</param>
    /// <param name="originCooltime">�ش� �������� ������ ��Ÿ��</param>
    public void AddCooltimeQueue(int itemID, float originCooltime)
    {
        mCooltimes.TryAdd(itemID, originCooltime);

        mCooltimes[itemID] = originCooltime;
        mCooltimeList.Add(itemID);
    }

    /// <summary>
    /// �ش� �������� ���� ��Ÿ�� �ð��� �����´�
    /// </summary>
    /// <param name="itemID">Ȯ���� ��Ÿ���� ������</param>
    /// <returns></returns>
    public float GetCurrentCooltime(int itemID)
    {
        float cooltime;
        bool isSuccess = mCooltimes.TryGetValue(itemID, out cooltime);

        if (isSuccess) { return cooltime; }
        else { return 0; }
    }
}