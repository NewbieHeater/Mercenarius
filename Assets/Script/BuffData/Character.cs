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
    //public Transform buffIconParent; // Buff UI �������� ��ġ�� �θ� ������Ʈ
    //public GameObject buffIconPrefab; // Buff UI ������ ������
    private Dictionary<BuffType, Image> activeBuffIcons = new Dictionary<BuffType, Image>();

    public void ApplyBuff(IBuff buff)
    {
        //���� �պ�����
        if (activeBuffs.TryGetValue(buff.BuffType, out IBuff existingBuff))
        {
            // ��ø�Ǵ� ��� ���� ������ ȿ���� ����
            existingBuff.StackBuff(buff.EffectValue);
        }
        else
        {
            buff.ApplyEffect(this);
            activeBuffs[buff.BuffType] = buff;
            StartCoroutine(RemoveBuffAfterDuration(buff));

            // UI�� ���� ������ �߰�
            //AddBuffIcon(buff);
        }
    }

    private IEnumerator RemoveBuffAfterDuration(IBuff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        buff.RemoveEffect(this);
        activeBuffs.Remove(buff.BuffType);

        // UI���� ���� ������ ����
        //RemoveBuffIcon(buff.BuffType);
    }

    //private void AddBuffIcon(IBuff buff)
    //{
    //    GameObject iconObject = Instantiate(buffIconPrefab, buffIconParent);
    //    Image iconImage = iconObject.GetComponent<Image>();
    //    // ���⿡ ���� ������ ���� ���� �߰�

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

        // UI���� ��� ���� ������ ����
        //foreach (var icon in activeBuffIcons.Values)
        //{
        //    Destroy(icon.gameObject);
        //}
        //activeBuffIcons.Clear();
    }
}
