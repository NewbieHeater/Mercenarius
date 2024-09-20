using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float AttackBuff { get; set; } = 1f;
    public float HpBuff { get; set; } = 1f;
    public float DefenseBuff { get; set; } = 1f;
    public float MovementSpeedBuff { get; set; } = 1f;
    
    private Dictionary<BuffType, Buff> activeBuffs = new Dictionary<BuffType, Buff>();
    public Transform buffIconParent; // Buff UI 아이콘을 배치할 부모 오브젝트
    public GameObject buffIconPrefab; // Buff UI 아이콘 프리팹

    private Dictionary<BuffType, Image> activeBuffIcons = new Dictionary<BuffType, Image>();

    public void ApplyBuff(BuffData buffData)
    {
        if (activeBuffs.ContainsKey(buffData.buffType))
        {
            Buff existingBuff = activeBuffs[buffData.buffType];
            if (buffData.isStackable)
            {
                existingBuff.StackBuff(buffData.effectValue);
            }
            else
            {
                existingBuff.RemoveEffect(this);
                Buff newBuff = new Buff(buffData);
                newBuff.ApplyEffect(this);
                activeBuffs[buffData.buffType] = newBuff;
                StartCoroutine(RemoveBuffAfterDuration(newBuff));
            }
        }
        else
        {
            Buff newBuff = new Buff(buffData);
            newBuff.ApplyEffect(this);
            activeBuffs[buffData.buffType] = newBuff;
            StartCoroutine(RemoveBuffAfterDuration(newBuff));
            AddBuffIcon(buffData);
        }
    }

    private IEnumerator RemoveBuffAfterDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        buff.RemoveEffect(this);
        activeBuffs.Remove(buff.BuffType);
        RemoveBuffIcon(buff.BuffType);
    }

    public void RemoveAllBuffs()
    {
        foreach (var buff in activeBuffs.Values)
        {
            buff.RemoveEffect(this);
        }
        activeBuffs.Clear();
    }

    private void AddBuffIcon(BuffData buffData)
    {
        GameObject iconObject = Instantiate(buffIconPrefab, buffIconParent);
        Image iconImage = iconObject.GetComponent<Image>();
        iconImage.sprite = buffData.buffImage;

        activeBuffIcons[buffData.buffType] = iconImage;
    }

    private void RemoveBuffIcon(BuffType buffType)
    {
        if (activeBuffIcons.ContainsKey(buffType))
        {
            Destroy(activeBuffIcons[buffType].gameObject);
            activeBuffIcons.Remove(buffType);
        }
    }
}
