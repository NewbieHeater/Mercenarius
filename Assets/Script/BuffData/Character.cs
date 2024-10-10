using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float AttackBuff { get; set; } = 0f;
    public float HpBuff { get; set; } = 0f;
    public float MovementSpeedBuff = 0f;
    public float DefenseBuff = 0f;

    private Dictionary<BuffType, IBuff> activeBuffs = new Dictionary<BuffType, IBuff>();
    //public Transform buffIconParent; // Buff UI 아이콘을 배치할 부모 오브젝트
    //public GameObject buffIconPrefab; // Buff UI 아이콘 프리팹
    private Dictionary<BuffType, Image> activeBuffIcons = new Dictionary<BuffType, Image>();

    public void ApplyBuff(IBuff buff)
    {
        //아직 손봐야함
        if (activeBuffs.TryGetValue(buff.BuffType, out IBuff existingBuff))
        {
            // 중첩되는 경우 기존 버프의 효과를 증가
            existingBuff.StackBuff(buff.EffectValue);
        }
        else
        {
            buff.ApplyEffect(this);
            activeBuffs[buff.BuffType] = buff;
            StartCoroutine(RemoveBuffAfterDuration(buff));

            // UI에 버프 아이콘 추가
            //AddBuffIcon(buff);
        }
    }

    private IEnumerator RemoveBuffAfterDuration(IBuff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        buff.RemoveEffect(this);
        activeBuffs.Remove(buff.BuffType);

        // UI에서 버프 아이콘 제거
        //RemoveBuffIcon(buff.BuffType);
    }

    //private void AddBuffIcon(IBuff buff)
    //{
    //    GameObject iconObject = Instantiate(buffIconPrefab, buffIconParent);
    //    Image iconImage = iconObject.GetComponent<Image>();
    //    // 여기에 버프 아이콘 설정 로직 추가

    //    activeBuffIcons[buff.BuffType] = iconImage;
    //}

    //private void RemoveBuffIcon(BuffType buffType)
    //{
    //    if (activeBuffIcons.ContainsKey(buffType))
    //    {
    //        Destroy(activeBuffIcons[buffType].gameObject);
    //        activeBuffIcons.Remove(buffType);
    //    }
    //}

    public void RemoveAllBuffs()
    {
        foreach (var buff in activeBuffs.Values)
        {
            buff.RemoveEffect(this);
        }
        activeBuffs.Clear();

        // UI에서 모든 버프 아이콘 제거
        //foreach (var icon in activeBuffIcons.Values)
        //{
        //    Destroy(icon.gameObject);
        //}
        //activeBuffIcons.Clear();
    }
}
